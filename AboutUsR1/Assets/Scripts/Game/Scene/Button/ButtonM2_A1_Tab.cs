using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonM2_A1_Tab : ButtonData
{
    public MenuM2_A1 menu => parentMenu as MenuM2_A1;
    public DataM2_A1_Tab DataTab { get; set; } = null;

    public override void OnClick()
    {
        base.OnClick();
        menu.SetupTabView(this);
    }

}
