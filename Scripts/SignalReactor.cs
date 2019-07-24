using Godot;
using System;
using System.Runtime.Remoting.Messaging;

public class SignalReactor : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SignalManger.Subscribe("build_button_pressed", OnSignalReceive);
    }

    private void OnSignalReceive(params object[] vars)
    {
        GD.Print("signal recieved");

        var prefabPath = vars[0];
        var prefab  = GD.Load<PackedScene>(prefabPath.ToString());
        var item = prefab.Instance();
        GetParent().AddChild(item);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
