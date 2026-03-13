using System;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class StringAttribute : Attribute
{
    public string Note { get; }
    public string Value { get; }
    public StringAttribute(string not, string value)
    {
        Note = not;
        Value = value;
    }
}
