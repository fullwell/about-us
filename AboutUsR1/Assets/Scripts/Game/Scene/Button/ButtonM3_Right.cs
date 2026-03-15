using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ButtonM3_Right : ButtonData
{
    public MenuM3_DetailType detailType;
    public MenuM3_Detail menu => parentMenu as MenuM3_Detail;
    public override void OnClick()
    {
        base.OnClick();
        menu.CurrentDetailType = detailType;
        menu.Show();
    }
}
