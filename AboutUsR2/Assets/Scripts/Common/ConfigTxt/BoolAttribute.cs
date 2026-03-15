using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolAttribute : Attribute
{
    public string Note { get; }
    public bool Value { get; }
    public BoolAttribute(string not, bool value)
    {
        Note = not;
        Value = value;
    }
}
