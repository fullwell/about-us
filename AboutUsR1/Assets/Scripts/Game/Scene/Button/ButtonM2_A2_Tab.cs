using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonM2_A2_Tab : ButtonData
{
    public MenuM2_A2 menu => parentMenu as MenuM2_A2;
    public DataM2_A2_Tab DataTab { get; set; } = null;

    public override void OnClick()
    {
        base.OnClick();
        menu.SetupTabView(this);
    }

}
