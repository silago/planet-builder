using Godot;
using System;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using Godot.Collections;

public class SignalManger : Node 
{
    //private static SignalManger _instance;
    //public static SignalManger Instance()
    //{
    //    return _instance ?? (_instance = new SignalManger());
    //}
    
    
    public delegate void Signal(params object[] vars);
    
    public static  Dictionary<string, EventsHandler> events = new Dictionary<string, EventsHandler>();
    
    public static void SendSignal(string name, params object[] vars)
    {
        if (events.ContainsKey(name) == true)
        {
            events[name].Invoke(vars);
        }
    }

    public static void Subscribe(string name, Signal s)
    {
        if (events.ContainsKey(name) == false)
        {
            events.Add(name, new EventsHandler());
        }
        events[name].Signal += s;
    }

    public static void Unsubscribe(string name, Signal s)
    {
        events[name].Signal -= s;
    }
}
