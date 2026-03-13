using System;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class ButtonBindAttribute : Attribute
{
    public string ChildPath { get; }
    public string MethodName { get; }

    public ButtonBindAttribute(string childPath, string methodName)
    {
        ChildPath = childPath;
        MethodName = methodName;
    }
}