using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataM2_A2_Tab_Product : DataParent
{
    [SerializeField]
    public string tabTitle = string.Empty;

    [SerializeField]
    [Header("头像")]
    public Texture2D head;
    [Header("分类")]
    public string classify;
    [SerializeField]
    [Header("温区")]
    public float warmArea;
    [SerializeField]
    [Header("冷量")]
    public float coolingCapacity;

    [Header("详情页")]
    [SerializeField]
    public Texture2D model;
    [SerializeField]
    public Texture2D feature;
    [SerializeField]
    public Texture2D parameter;
    [SerializeField]
    public Texture2D size;
    [SerializeField]
    public Texture2D application;
    [SerializeField]
    public Texture2D suit;

}
