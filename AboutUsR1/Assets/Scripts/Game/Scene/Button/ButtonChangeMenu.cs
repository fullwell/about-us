using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChangeMenu : ButtonData
{
    public MenuParent nextShowMenu;

    public override void OnClick()
    {
        base.OnClick();
        Main.MenuParents.ForEach((m) =>
        {
            if(nextShowMenu == m)
            {
                if (!m.gameObject.activeSelf)
                {
                    m.gameObject.SetActive(true);
                    m.Show();
                }
            }
            else
            {
                if(m.gameObject.activeSelf)
                {
                    m.gameObject.SetActive(false);
                    m.Hide();
                }
            }
        });
    }

}
