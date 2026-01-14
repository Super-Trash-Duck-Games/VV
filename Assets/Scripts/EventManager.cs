using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager 
{
    public delegate void Event(params object[] parameters);

    public static Dictionary<string, Event> events = new Dictionary<string, Event>();

    public static void Subscribe(string name, Event method)
    {
        if (events.ContainsKey(name))
            events[name] += method;
        else
            events.Add(name, method);
    }

    public static void UnSubsctribe(string name, Event method)
    {
        if (events.ContainsKey(name))
        {
            events[name] -= method;

            if (events[name] == null)
                events.Remove(name);
        }
    }

    public static void Trigger(string name, params object[] parameters)
    {
        if (events.ContainsKey(name))
        {
            events[name](parameters);
        }
    }
}
