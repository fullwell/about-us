using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using Newtonsoft;
using Newtonsoft.Json;
using System.Linq;

public class D : MonoBehaviour
{
    public static D Instance;
    public static Color32 colorTab0 = new Color32(16, 147, 103, 255);
    public static Color32 colorTab1 = new Color32(94, 202, 166, 255);
    public static Color32 colorTab2 = new Color32(68, 195, 153, 255);
    public static Color32 colorTab3 = new Color32(133, 231, 199, 255);
    public static float volumeBg = 1f;
    public static float volumeEffect = 1f;

    void Awake()
    {
        Instance = this;
    }

}



