using Godot;

namespace PlanetBuilder.Scripts
{
    public static class NodeExtenstions
    {
        const string rootPrefox = "/root/";
        public static T Global<T>(this Node node) where T : Node
        {
            string name = nameof(T);
            if (node.GetNode(rootPrefox + name) is T globalElement)
            {
                return globalElement;
            }

            return null;
        } 
    }
}