using Godot;
using System;
using Godot.Collections;

public class SignalButton : Button
{

    [Export]
    protected Dictionary<string, int> Requirements;
    
    [Export]
    protected String SignalName;
    
    [Export]
    protected PackedScene BuildingPrefab;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //SignalManger.Instance().
    }

    public override void _Pressed()
    {
        base._Pressed();
        SignalManger.SendSignal( SignalName, BuildingPrefab);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
