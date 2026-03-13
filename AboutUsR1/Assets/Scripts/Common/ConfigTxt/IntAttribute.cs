using System;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class IntAttribute : Attribute
{
    public string Note { get; }
    public int Value { get; }
    public IntAttribute(string not, int value)
    {
        Note = not;
        Value = value;
    }
}
