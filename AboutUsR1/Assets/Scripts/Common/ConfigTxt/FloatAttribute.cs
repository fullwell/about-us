using System;
using System.Reflection;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class FloatAttribute : Attribute
{
    public string Note { get; }
    public float Value { get; }
    public FloatAttribute(string not, float value)
    {
        Note = not;
        Value = value;
    }
}
