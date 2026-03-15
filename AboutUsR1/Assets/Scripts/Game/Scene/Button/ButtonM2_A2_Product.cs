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
        MenuM3_Detail m3Detail = Main.MenuParents.Find(v => v.gameObject.name == "M3-Detail") as MenuM3_Detail;
        m3Detail.DataProduct = DataProduct; ;
        m3Detail.CurrentDetailType = MenuM3_DetailType.none;
        m3Detail.gameObject.SetActive(true);
        m3Detail.Show();
    }


}
