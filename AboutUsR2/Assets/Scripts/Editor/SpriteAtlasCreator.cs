using UnityEditor;
using UnityEditor.U2D; // 需要引用U2D命名空间
using UnityEngine;
using UnityEngine.U2D;
using System.IO; // 用于文件操作
using System.Collections.Generic; // 如果需要使用列表

public class SpriteAtlasCreator : EditorWindow
{
    // 添加一个菜单项
    [MenuItem("Assets/Create Atlas")]
    static void CreateSpriteAtlas()
    {

        // 创建SpriteAtlas实例
        SpriteAtlas spriteAtlas = new SpriteAtlas();

        // 1. 配置打包设置 (PackingSettings)
        SpriteAtlasPackingSettings packingSettings = new SpriteAtlasPackingSettings()
        {
            blockOffset = 1,       // 块偏移，通常为1
            enableRotation = false, // 是否允许旋转精灵以优化空间。如果图集包含UI元素，建议禁用:cite[2]:cite[7]
            enableTightPacking = false, // 是否启用紧密打包（基于精灵轮廓而非矩形）。启用可以优化空间，但可能在某些情况下导致问题:cite[2]:cite[7]
            padding = 4 // 精灵之间的像素间隔，防止重叠。常用2,4,8:cite[2]:cite[7]
        };
        spriteAtlas.SetPackingSettings(packingSettings);

        // 2. 配置纹理设置 (TextureSettings)
        SpriteAtlasTextureSettings textureSettings = new SpriteAtlasTextureSettings()
        {
            readable = false,     // 是否启用读写。通常禁用以避免内存翻倍:cite[1]:cite[6]
            generateMipMaps = false, // 是否生成Mip Maps。UI元素通常禁用以节省内存:cite[2]:cite[7]
            sRGB = true,          // 是否使用sRGB（Gamma空间）存储颜色
            filterMode = FilterMode.Bilinear // 过滤模式：Point（像素风），Bilinear（常用），Trilinear:cite[2]
        };
        spriteAtlas.SetTextureSettings(textureSettings);

        // 3. 配置目标平台设置 (PlatformSettings)
        TextureImporterPlatformSettings platformSettings = new TextureImporterPlatformSettings()
        {
            name = "Standalone", // 平台名称，如 "Android", "iPhone", "WebGL"
            overridden = true,   // 是否覆盖此平台的默认设置
            maxTextureSize = 2048, // 图集最大尺寸（512, 1024, 2048, 4096）
            format = TextureImporterFormat.ARGB32, // 压缩格式，Automatic通常即可
            //compressionQuality = 50, // 压缩质量 (0-100)
            // 可以根据需要设置其他属性，如 crunchedCompression
        };
        spriteAtlas.SetPlatformSettings(platformSettings);


        if (0 < Selection.assetGUIDs.Length)
        {
            string folder = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
            if (AssetDatabase.IsValidFolder(folder))
            {
                List<Object> assetsToAdd = new List<Object>();
                var guids = AssetDatabase.FindAssets("t:sprite", new string[] { folder });
                foreach (var guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                    assetsToAdd.Add(sprite);
                    Debug.LogWarning(path);
                }
                spriteAtlas.Add(assetsToAdd.ToArray());


                // 指定图集保存的路径和名称
                string atlasPath = Path.Combine(folder, Path.GetFileNameWithoutExtension(folder) + ".spriteatlas");
                AssetDatabase.DeleteAsset(atlasPath);
                AssetDatabase.CreateAsset(spriteAtlas, atlasPath);
                AssetDatabase.SaveAssets(); // 保存资产
                AssetDatabase.Refresh();    // 刷新数据库

                Debug.Log($"Sprite Atlas created successfully at: {atlasPath}");

            }
        }



        /**/
    }


    static void FindChildFolder(string folder, List<string> folders)
    {
        if(!folders.Contains(folder))
        {
            folders.Add(folder);
        }
        foreach(var child in Directory.GetDirectories(folder))
        {
            FindChildFolder(child, folders);
        }

    }

}