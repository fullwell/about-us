using System;
using UnityEngine;

public class View : MonoBehaviour
{
    #region UI 组件
    private bool isUIBinded=false;
    public void BindUI()
    {
        if (isUIBinded)
        { return; }
        isUIBinded = true;

        bindUIView();
        bindButtons();
    }
    private void bindUIView()
    {
        var fields = this.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        foreach (var field in fields)
        {
            var attributes = field.GetCustomAttributes(typeof(ViewNameAttribute), true);
            if (attributes.Length > 0)
            {
                var viewNameAttribute = (ViewNameAttribute)attributes[0];
                var childName = viewNameAttribute.ViewName;
                Transform childTransform = transform.Find(childName);

                if (childTransform != null)
                {
                    if (field.FieldType == typeof(GameObject))
                    {
                        field.SetValue(this, childTransform.gameObject);
                        continue;
                    }
                    if (field.FieldType == typeof(Transform))
                    {
                        field.SetValue(this, childTransform.transform);
                        continue;
                    }
                    if (field.FieldType == typeof(RectTransform))
                    {
                        field.SetValue(this, childTransform.GetComponent<RectTransform>());
                        continue;
                    }

                    var component = childTransform.GetComponent(field.FieldType);
                    if (component != null)
                    {
                        field.SetValue(this, component);
                    }
                    else
                    {
                        Debug.LogError($"Component of type '{field.FieldType}' not found on the child object '{childName}'.");
                    }
                }
                else
                {
                    Debug.LogError($"Child object with name '{childName}' not found.");
                }
            }
        }
    }
    #endregion
    #region 按钮
    private void bindButtons()
    {
        var fields = this.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        foreach (var field in fields)
        {
            var attributes = field.GetCustomAttributes(typeof(ButtonBindAttribute), true);
            if (attributes.Length > 0)
            {
                var buttonBindAttribute = (ButtonBindAttribute)attributes[0];
                var childPath = buttonBindAttribute.ChildPath;
                var methodName = buttonBindAttribute.MethodName;

                Transform buttonTransform = transform.Find(childPath);
                if (buttonTransform != null)
                {
                    var buttonComponent = buttonTransform.GetComponent<UnityEngine.UI.Button>();
                    if (buttonComponent != null)
                    {
                        // 绑定按钮点击事件到指定函数
                        buttonComponent.onClick.AddListener(() => { InvokeMethodByName(methodName); });
                        field.SetValue(this, buttonComponent);
                    }
                    else
                    {
                        Debug.LogError($"Button component not found on the child object at path '{childPath}'.");
                    }
                }
                else
                {
                    Debug.LogError($"Child object with path '{childPath}' not found.");
                }
            }
        }
    }
    private void InvokeMethodByName(string methodName)
    {
        // 使用反射调用特定名称的函数
        this.Invoke(methodName, 0);
    }
    #endregion

    protected Action unAction;
    public virtual void Create() { }
    public virtual void Show() { }
    public virtual void Hide() { }


}
