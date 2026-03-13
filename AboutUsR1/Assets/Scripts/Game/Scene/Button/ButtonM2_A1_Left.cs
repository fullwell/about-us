using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonM2_A1_Left : ButtonData
{
    [SerializeField]
    public List<DataM2_A1_Tab> dataTabs = new List<DataM2_A1_Tab>();

    public MenuM2_A1 menu => parentMenu as MenuM2_A1;

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
