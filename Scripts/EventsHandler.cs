    using System.Dynamic;
    using System.Runtime.Remoting.Messaging;
    using Godot;
    public class EventsHandler : Node
    {
        public event SignalManger.Signal Signal;

        public void Invoke(params object[] vars)
        {
            Signal.Invoke(vars);
        }
    }
