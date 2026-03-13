using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleRoot : MonoBehaviour
{
    [Header("打包后，强制Apply")]
    public bool apply = false;
    public int perfectWidth = 2090;
    public int perfectHeight = 1250;
    public void Apply()
    {
//#if !UNITY_EDITOR
//        apply = true;
//#endif
//        if (apply)
//        {
//            transform.localScale = new Vector3(
//                1f * D.Instance.targetScreenWidth / perfectWidth,
//                1f * D.Instance.targetScreenHeight / perfectHeight,
//                1f);
//        }
    }

}
