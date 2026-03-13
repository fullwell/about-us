using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Expend 
{
    public static Transform FindName(this Transform tran, string name)
    {
        return childFind(tran);

        Transform childFind(Transform tran1)
        {
            var t = tran1.Find(name);
            if (null != t)
                return t;
            for(int i = 0; i < tran1.childCount; i++)
            {
                t = childFind(tran1.GetChild(i));
                if (null != t)
                    return t;
            }
            return null;
        }
    }
}
