using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

public class BuildSignalReactor : Area2D
{
    
    private Node2D node;
    private Node2D activeFloor;
    
    public override void _Ready()
    {
        SignalManger.Subscribe("build_button_pressed", OnSignalReceive);
    }

    private void OnSignalReceive(params object[] vars)
    {
        if (vars.Length > 0 && vars[0] is PackedScene prefab) 
        {
            //var prefab = GD.Load<PackedScene>(vars[0].ToString());
            if (prefab.Instance() is Node2D instance)
            {
                node = instance;
                GetTree().GetRoot().AddChild(node);
                Monitoring = true;
                Connect("body_entered", this, nameof(OnBodyEnter));
                Connect("body_exited", this, nameof(OnBodyExit));
                GD.Print("connected");
            }
        } 
        
        
    }

    private void OnBodyEnter(PhysicsBody2D body)
    {
        if (body.IsInGroup("floor"))
        {
            activeFloor = body;
        } 
    }

    private void OnBodyExit(PhysicsBody2D body)
    {
        if (body == activeFloor)
        {
            activeFloor = null;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        if (node == null)
        {
            return;
        }

        if (activeFloor == null)
        {
            node.Position = Position;
        }
        else
        {
            var space = GetWorld2d().DirectSpaceState;
            var result = space.IntersectRay(node.Position + new Vector2(0,10).Rotated(node.Rotation+Mathf.Deg2Rad(90)), Position);
            if (result.Count != 0)
            {
                Vector2 pos = (Vector2)result["position"];
                GD.Print(result["position"]);
                GD.Print(result["collider"]);
                node.LookAt(Position);
                node.Position = pos ;
            }
            else
            {
                GD.Print("no collision");
            }
        }

                  
    }

    public override void _Input(InputEvent @event)
    {
        if (node == null)
        {
            return;
        }
        
        base._Input(@event);
        //if ( followMouse )
        {
            
            if (@event is InputEventMouseMotion mouseMotion)
            {
               //GetGlobalMousePosition()
               
               Position = GetGlobalMousePosition();//mouseMotion.Position;
               if (activeFloor != null)
               {
                   node.LookAt(activeFloor.Position);
               }
            }

            if (@event is InputEventMouseButton mouseButton)
            {
                //followMouse = false;
                Place();
            }
        }
    }

    private void Place()
    {
        Disconnect("body_entered", this, nameof(OnBodyEnter));
        Disconnect("body_exited", this, nameof(OnBodyExit));
        node = null;
    }
}
