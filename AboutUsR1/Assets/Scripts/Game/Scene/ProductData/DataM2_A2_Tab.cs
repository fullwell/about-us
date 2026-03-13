using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataM2_A2_Tab : DataParent
{
    [SerializeField]
    public string tabTitle = string.Empty;
    [SerializeField]
    public List<DataM2_A2_Tab_Product> products = new List<DataM2_A2_Tab_Product>();

}
