using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonData : MonoBehaviour
{
    private MenuParent _parentMenu;
    protected MenuParent parentMenu
    {
        get
        {
            if (null == _parentMenu)
            {
                _parentMenu = gameObject.GetComponentInParent<MenuParent>();
            }
            return _parentMenu;
        }
    }
    protected Button button;

    public virtual void Init()
    {
        if(!gameObject.TryGetComponent<Button>(out button))
        {
            button = gameObject.AddComponent<Button>();
            button.transition = Selectable.Transition.None;
        }
        button.onClick.AddListener(OnClick);
    }

    public virtual void OnClick() 
    {
        Debug.Log("点击按钮 " + gameObject.name);
    }

}
