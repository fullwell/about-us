using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private static Dictionary<string, Action<object>> actions = new Dictionary<string, Action<object>>();

    public static Action Sub(string eventName, Action<object> eventAction)
    {
        if(actions.ContainsKey(eventName)) 
        {
            actions[eventName] += eventAction;
        }
        else
        {
            actions.Add(eventName, eventAction);
        }
        return () => 
        {
            actions[eventName] -= eventAction;
        };
    }

    public static void Pub(string eventName, object eventParam)
    {
        if(actions.ContainsKey(eventName)) 
        {
            actions[eventName]?.Invoke(eventParam);
        }
    }
    public static void Pub(string eventName)
    {
        if (actions.ContainsKey(eventName))
        {
            actions[eventName]?.Invoke(null);
        }
    }

}
