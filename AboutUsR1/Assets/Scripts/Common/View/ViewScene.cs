using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewScene : View
{
    public int sceneId;
    public bool sceneActive = false;
    public override void Show()
    {
        base.Show();
        sceneActive = true;
    }
    public override void Hide()
    {
        base.Hide();
        sceneActive = false;
    }
}
