using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.U2D;
using UnityEngine.U2D;

public class EasyEditor : MonoBehaviour
{
    [MenuItem("Assets/Create RawImage", false, 50)]
    static void CreateRawImage()
    {
        // 或者获取选中的资源的GUID
        string[] selectedGUIDs = Selection.assetGUIDs;
        foreach (string guid in selectedGUIDs)
        {
            // 将GUID转换为资源路径
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            // 通过资源路径加载资源对象
            Texture asset = AssetDatabase.LoadAssetAtPath<Texture>(assetPath);
            if (null != asset)
            {
                var o = new GameObject(asset.name);
                o.transform.parent = GameObject.Find("Canvas").transform;
                o.transform.localPosition = Vector3.zero;
                o.transform.localScale = Vector3.one;
                var raw = o.AddComponent<RawImage>();
                raw.texture = asset;
                raw.SetNativeSize();
            }
        }
    }

    [MenuItem("Assets/Create Image", false, 51)]
    static void CreateImage()
    {
        // 或者获取选中的资源的GUID
        string[] selectedGUIDs = Selection.assetGUIDs;
        foreach (string guid in selectedGUIDs)
        {
            // 将GUID转换为资源路径
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            // 通过资源路径加载资源对象
            Sprite asset = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            if (null != asset)
            {
                var o = new GameObject(asset.name);
                o.transform.parent = GameObject.Find("Canvas").transform;
                o.transform.localPosition = Vector3.zero;
                o.transform.localScale = Vector3.one;
                var raw = o.AddComponent<Image>();
                raw.sprite = asset;
                raw.SetNativeSize();
            }

        }
    }

    [MenuItem("My Tools/Action _F1")]
    static void F1()
    {
        bool flag = false;
        bool active = false;
        foreach(var obj in Selection.objects)
        {
            if(obj is GameObject o)
            {
                if (!flag)
                {
                    flag = true;
                    active = !o.activeSelf;
                }
                o.SetActive(active);
            }
        }

    }

}
