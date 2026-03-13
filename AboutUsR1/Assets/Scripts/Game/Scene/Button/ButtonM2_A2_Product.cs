using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonM2_A2_Product : ButtonData
{
    public MenuM2_A2 menu => parentMenu as MenuM2_A2;
    public DataM2_A2_Tab_Product DataProduct { get; set; }

    public override void OnClick()
    {
        base.OnClick();
        menu.SetupProductView(this);
    }


}
