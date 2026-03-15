using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

namespace LTG.UnityEditor
{
    public partial class EasyEditor
    {
        public class FontsMaker : EditorWindow
        {
            [MenuItem("Tools/[EasyEditor]/创建字体(sprite)")]
            public static void Open()
            {
                var w = GetWindow<FontsMaker>("创建字体");
                w.fontPath = EditorPrefs.GetString("ee_lastFontSavePath");
            }

            private Texture2D tex;
            private Vector2Int CharXOffset = Vector2Int.zero;
            private string fontName;
            private string fontPath;
            private void OnGUI()
            {
                GUILayout.BeginVertical();

                GUILayout.BeginHorizontal();
                GUILayout.Label("字体贴图：");
                tex = (Texture2D)EditorGUILayout.ObjectField(tex, typeof(Texture2D), true);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("字体名称：");
                fontName = EditorGUILayout.TextField(fontName);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                CharXOffset = EditorGUILayout.Vector2IntField("字间距：(X分量)", CharXOffset);
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                if (GUILayout.Button(string.IsNullOrEmpty(fontPath) ? "选择路径" : fontPath))
                {
                    fontPath = EditorUtility.OpenFolderPanel("字体路径", Application.dataPath, "");
                    if (string.IsNullOrEmpty(fontPath))
                    {
                        Debug.Log("取消选择路径");
                    }
                    else
                    {
                        fontPath = fontPath.Replace(Application.dataPath, "") + "/";
                        EditorPrefs.SetString("ee_lastFontSavePath", fontPath);
                    }
                }
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                if (GUILayout.Button("创建"))
                {
                    Create();
                }
                GUILayout.EndHorizontal();


                GUILayout.EndVertical();
                GUILayout.Label("#1 贴图必须是multiple\n#2切割的sprite需要以ascii单字符命名\n#3再次生成的font保存目录不变才能保留引用");
            }

            private void Create()
            {

                string mFontPath;
                string mMatPath;
                mFontPath = "Assets/" + fontPath + fontName + ".fontsettings";
                mMatPath = "Assets/" + fontPath + fontName + ".mat";

                if (tex == null)
                {
                    Debug.LogWarning("创建失败，图片为空！");
                    return;
                }

                if (string.IsNullOrEmpty(fontPath))
                {
                    Debug.LogWarning("字体路径为空！");
                    return;
                }
                if (fontName == null)
                {
                    Debug.LogWarning("创建失败，字体名称为空！");
                    return;
                }

                string selectionPath = AssetDatabase.GetAssetPath(tex);
                string selectionExt = Path.GetExtension(selectionPath);
                if (selectionExt.Length == 0)
                {
                    Debug.LogError("创建失败！");
                    return;
                }

                string fontPathName = fontPath + fontName + ".fontsettings";
                string matPathName = fontPath + fontName + ".mat";

                string path = AssetDatabase.GetAssetPath(tex);
                List<Sprite> tmp = new List<Sprite>();
                foreach (var obj in AssetDatabase.LoadAllAssetsAtPath(path))
                {
                    Sprite sprite = obj as Sprite;
                    if (sprite != null)
                    {
                        tmp.Add(sprite);
                    }
                }
                var sprites = tmp.ToArray();
                float lineSpace = 0.1f;
                if (sprites != null && sprites.Length > 0)
                {
                    Font mFont = AssetDatabase.LoadAssetAtPath<Font>(mFontPath);
                    Material mat = AssetDatabase.LoadAssetAtPath<Material>(mMatPath);
                    var existFont = mFont != null;
                    var existMat = mat != null;
                    if (!existFont)
                    {
                        mFont = new Font();
                    }
                    if (!existMat)
                    {
                        mat = new Material(Shader.Find("GUI/Text Shader"));
                        mat.SetTexture("_MainTex", tex);
                    }
                    mFont.material = mat;
                    CharacterInfo[] characterInfo = new CharacterInfo[sprites.Length];
                    for (int i = 0; i < sprites.Length; i++)
                    {
                        if (sprites[i].rect.height > lineSpace)
                        {
                            lineSpace = sprites[i].rect.height;
                        }
                    }
                    for (int i = 0; i < sprites.Length; i++)
                    {
                        Sprite spr = sprites[i];
                        CharacterInfo info = new CharacterInfo();
                        try
                        {
                            //info.index = System.Convert.ToInt32(spr.name) + 48;
                            info.index = Char.Parse(spr.name);
                            char chr;
                            if (char.TryParse(spr.name, out chr))
                            {
                                info.index = chr;
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"创建失败，Sprite名称错误！{ex}");
                            return;
                        }
                        Rect rect = spr.rect;
                        float pivot = spr.pivot.y / rect.height - 0.5f;
                        if (pivot > 0)
                        {
                            pivot = -lineSpace / 2 - spr.pivot.y;
                        }
                        else if (pivot < 0)
                        {
                            pivot = -lineSpace / 2 + rect.height - spr.pivot.y;
                        }
                        else
                        {
                            pivot = -lineSpace / 2;
                        }
                        int offsetY = (int)(pivot + (lineSpace - rect.height) / 2);
                        info.uvBottomLeft = new Vector2((float)rect.x / tex.width, (float)(rect.y) / tex.height);
                        info.uvBottomRight = new Vector2((float)(rect.x + rect.width) / tex.width, (float)(rect.y) / tex.height);
                        info.uvTopLeft = new Vector2((float)rect.x / tex.width, (float)(rect.y + rect.height) / tex.height);
                        info.uvTopRight = new Vector2((float)(rect.x + rect.width) / tex.width, (float)(rect.y + rect.height) / tex.height);
                        info.minX = 0;
                        info.minY = -(int)rect.height - offsetY;
                        info.maxX = (int)rect.width;
                        info.maxY = -offsetY;
                        info.advance = (int)rect.width + CharXOffset.x;
                        characterInfo[i] = info;
                    }
                    if (!existFont)
                        AssetDatabase.CreateAsset(mat, "Assets" + matPathName);
                    if (!existMat)
                        AssetDatabase.CreateAsset(mFont, "Assets" + fontPathName);
                    mFont.characterInfo = characterInfo;
                    EditorUtility.SetDirty(mFont);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();//刷新资源
                    Debug.Log("创建字体成功");

                }
                else
                {
                    Debug.LogError("图集错误！");
                }
            }
        }
    }
}