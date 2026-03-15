using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameTool
{
    public static int count = 0;
    private static Dictionary<PrimitiveType, List<GameObject>> primitiveObjs = new Dictionary<PrimitiveType, List<GameObject>>();
    //创建圆球
    public static GameObject CreatePrimitive(Vector3 pos, PrimitiveType type, object name, bool autoDestroy = true)
    {
        return null;
#if !UNITY_EDITOR
        return null;
#endif
        var o = GameObject.CreatePrimitive(type);
        o.transform.position = pos;
        o.transform.localScale = Vector3.one * 0.1f;
        o.name = name.ToString();

        if(!primitiveObjs.ContainsKey(type))
            primitiveObjs.Add(type, new List<GameObject>());
        primitiveObjs[type].Add(o);

        bool destroy = false;
        switch(type)
        {
            case PrimitiveType.Cube:
                MonoBehaviour.Destroy(o.GetComponent<BoxCollider>());
                if (primitiveObjs[type].Count > 20)
                    destroy = true;
                break;
            case PrimitiveType.Sphere:
                MonoBehaviour.Destroy(o.GetComponent<SphereCollider>());
                if (primitiveObjs[type].Count > 10)
                    destroy = true;
                break;
        }
        if (autoDestroy && destroy)
        {
            MonoBehaviour.Destroy(primitiveObjs[type][0]);
            primitiveObjs[type].RemoveAt(0);
        }
        return o;
    }


    //点到直线距离
    public static float DistancePointToLine(Vector3 point, Vector3 linePoint1, Vector3 linePoint2)
    {
        // 直线方向向量
        Vector3 lineDirection = linePoint2 - linePoint1;

        // 点到直线起点向量
        Vector3 pointToLineStart = point - linePoint1;

        // 计算叉积的模长
        float crossProductMagnitude = Vector3.Cross(lineDirection, pointToLineStart).magnitude;

        // 距离 = 叉积模长 / 直线方向模长
        return crossProductMagnitude / lineDirection.magnitude;
    }
}
