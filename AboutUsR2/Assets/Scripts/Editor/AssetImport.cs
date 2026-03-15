using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetImport : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        TextureImporter importer = (TextureImporter)assetImporter;

        Debug.Log(importer.assetPath);
        if (importer.assetPath.Contains("Resources/Sprite"))
        {
            // 设置纹理类型为 Sprite (2D and UI)
            importer.textureType = TextureImporterType.Sprite;
            // 禁用 Mipmap
            importer.mipmapEnabled = false;
            // 设置过滤模式为双线性
            importer.filterMode = FilterMode.Bilinear;
            // 设置最大纹理大小为 1024
            importer.maxTextureSize = 4096;
            // 设置纹理压缩方式为压缩
            importer.textureCompression = TextureImporterCompression.Uncompressed;

        }
        else if(importer.assetPath.Contains("Resources/Texture"))
        {
            // 设置纹理类型为 Sprite (2D and UI)
            importer.textureType = TextureImporterType.Default;
            // 禁用 Mipmap
            importer.mipmapEnabled = false;
            // 设置过滤模式为双线性
            importer.filterMode = FilterMode.Bilinear;
            // 设置最大纹理大小为 1024
            importer.maxTextureSize = 4096;
            // 设置纹理压缩方式为压缩
            importer.textureCompression = TextureImporterCompression.Uncompressed;

        }


        /*// 设置各平台的覆盖设置
        TextureImporterPlatformSettings androidSettings = new TextureImporterPlatformSettings();
        androidSettings.overridden = true;
        androidSettings.name = "Android"; // 设置平台为 Android
        androidSettings.format = TextureImporterFormat.ASTC_6x6; // 设置 Android 平台压缩格式为 ASTC 6x6
        importer.SetPlatformTextureSettings(androidSettings);

        TextureImporterPlatformSettings iOSSettings = new TextureImporterPlatformSettings();
        iOSSettings.overridden = true;
        iOSSettings.name = "iPhone"; // 设置平台为 iPhone
        iOSSettings.format = TextureImporterFormat.ASTC_6x6; // 设置 iOS 平台压缩格式为 ASTC 6x6
        importer.SetPlatformTextureSettings(iOSSettings);*/

        //Debug.Log($"Preprocessed texture: {assetPath}");
    }
}
