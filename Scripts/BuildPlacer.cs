using Godot;

public class BuildPlacer : Area2D
{
    private Node2D node;
    private bool initied = false;
    public void Init(Node2D node)
    {
        this.node = node;
        Connect("body_entered", this, nameof(OnBodyEnter));
        initied = true;
    }

    public override void _Ready()
    {
    }

    protected void onMouseMove()
    {
        
    }

    protected void OnBodyEnter(PhysicsBody2D body)
    {
        if (body.IsInGroup("floor"))
        {
            node.LookAt(body.Position);
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
                node.Position = mouseMotion.Position;
            }

            if (@event is InputEventMouseButton mouseButton)
            {
                //followMouse = false;
                QueueFree();
            }
        }
    }
}
