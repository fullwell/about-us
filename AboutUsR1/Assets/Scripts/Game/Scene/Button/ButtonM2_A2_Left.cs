using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonM2_A2_Left : ButtonData
{
    [SerializeField]
    public List<DataM2_A2_Tab> dataTabs = new List<DataM2_A2_Tab>();

    public MenuM2_A2 menu => parentMenu as MenuM2_A2;

    public override void Init()
    {
        base.Init();
    }
    public override void OnClick()
    {
        base.OnClick();
        menu.InitView(this);
    }


}
