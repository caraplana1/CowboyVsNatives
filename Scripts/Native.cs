using Godot;
using System.Collections.Generic;
using System.Linq;

public class Native : RigidBody2D
{
    #region  Field Declaration

    [Export]private float speed;
    private bool isActive;

    // Children
    private Sprite sprite;
    private CollisionShape2D collision;
    private AnimationPlayer animationPlayer;
    
    // Navigation
    private Vector2[] path;
    private Navigation2D map;

    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // children
        sprite = GetChild<Sprite>(0);
        collision = GetChild<CollisionShape2D>(1);
        animationPlayer = GetChild<AnimationPlayer>(2);

        map = GetParent().GetParent().GetChild<Navigation2D>(0);
        SetActivation(false);
    }

    public override void _Process(float delta)
    {
        if(path != null)
        {
            MoveAlongPath(speed * delta);
        }
    }


    void MoveAlongPath(float distance)
    {
        Vector2 startPoint = Position;
        float distanceToNextPoint;
        List<Vector2> aux;

        for (int i = 0; i < path.Length; i ++)
        {
            // GD.Print(path.Length);
            distanceToNextPoint = startPoint.DistanceTo(path[0]);
            if (distance <= distanceToNextPoint && distance >= 0)
            {
                Position = startPoint.LinearInterpolate(path[0], distance / distanceToNextPoint);
            }
            else if (distance < 0)
            {
                Position = path[0];
            }

            distance -= distanceToNextPoint;
            startPoint = path[0];
            aux = path.ToList<Vector2>();
            aux.RemoveAt(0);
            path = aux.ToArray<Vector2>();
            // GD.Print(path.Length);
        }
    }

    public void GetPlayerPosition(Vector2 playerPosition)
    {   
        if (isActive)
        {
            // Gets the path and correct the with the current object.
            path = map.GetSimplePath(GlobalPosition, playerPosition);
            /*
            for (int i = 0; i < path.Length; i++)
            {
                path[i] -= GlobalPosition;
            }
            GetNode<Line2D>("Line2D").Points = path; // Show the enemy path.
            */
        }
    }

    public void SetActivation(bool activationMode)
    {
        Visible = activationMode;
        collision.Disabled = !activationMode;
        Sleeping = !activationMode;
        SetProcess(activationMode);
        isActive = activationMode;
        if (activationMode)
        {
            animationPlayer.Play("Walking");
        }
        else
        {
            animationPlayer.Stop(true);
        }
    }

}
