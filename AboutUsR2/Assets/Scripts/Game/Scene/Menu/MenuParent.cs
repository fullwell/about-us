using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuParent : MonoBehaviour
{
    protected List<ButtonData> buttons = new List<ButtonData>();
    public virtual void Init()
    {
        buttons.AddRange(transform.GetComponentsInChildren<ButtonData>(true));
        buttons.ForEach((button) =>
        {
            button.Init();
        });
    }

    public virtual void Show()
    {
        Debug.Log("Show() =>Menu : " + gameObject.name);

    }

    public virtual void Hide()
    {
        Debug.Log("Hide() =>Menu : " + gameObject.name);

    }







}
