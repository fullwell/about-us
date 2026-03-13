using System;

// 自定义 Attribute 用于标记子节点名称
[System.AttributeUsage(System.AttributeTargets.Field)]
public class ViewNameAttribute : Attribute
{
    public string ViewName { get; }

    public ViewNameAttribute(string viewName)
    {
        ViewName = viewName;
    }
}