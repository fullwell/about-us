using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChangeMenu : ButtonData
{
    public MenuParent nextShowMenu;
    public List<MenuParent> unHideMenu = new List<MenuParent>();

    public override void OnClick()
    {
        base.OnClick();
        Main.MenuParents.ForEach((m) =>
        {
            if(nextShowMenu == m)
            {
                if (!m.gameObject.activeSelf)
                {
                    m.Show();
                    m.gameObject.SetActive(true);
                }
            }
            else
            {
                if(m.gameObject.activeSelf)
                {
                    if (!unHideMenu.Contains(m))
                    {
                        m.gameObject.SetActive(false);
                        m.Hide();
                    }
                }
            }
        });
    }

}
