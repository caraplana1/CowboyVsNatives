using Godot;
using System.Collections.Generic;
using System.Linq;

public class Native : RigidBody2D
{
    #region  Field Declaration

    [Export]private float speed;
    private bool isActive;

    // Children
    private CollisionShape2D collision;
    
    // Navegation
    private Vector2[] path;
    private Vector2 direction = Vector2.Zero;

    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        collision = GetChild<CollisionShape2D>(1);

        SetPhysicsProcess(false);
    }

    public override void _PhysicsProcess(float delta)
    {
       ApplyCentralImpulse(direction * speed);
    }

    public void FollowPath(Vector2[] pathToPlayer)
    {
        GetNode<Line2D>("Line2D").Points = pathToPlayer;
        /*
        Vector2 startPosition;
        List<Vector2> aux;

        path = pathToPlayer;
        startPosition = GlobalPosition;
        
        for (int i = 0; i < path.Length; i ++)
        {
            if (startPosition.DistanceTo(path[0]) < speed)
            {
                direction = path[0].Normalized();
                GD.Print("Moving to " + direction);
            }
            else
            {
                startPosition = path[0];
                aux = path.ToList<Vector2>();
                aux.RemoveAt(0);
                path = aux.ToArray<Vector2>();
            }
        } 
        */
    }

    public void SetActivation(bool activationMode)
    {
        Visible = activationMode;
        collision.Disabled = !activationMode;
        Sleeping = !activationMode;
        SetPhysicsProcess(activationMode);
        isActive = activationMode;
    }

    public bool IsActive()
    {
        return isActive;
    }

}
