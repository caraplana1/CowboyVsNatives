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
        // distance Is the long of the enmey will move.
        float distanceToNextPoint;
        List<Vector2> aux;
        
        // As long as there are a path and enough distance to move.
        while(distance > 0 && path.Length > 0)
        {
            distanceToNextPoint = Position.DistanceTo(path[0]);

            if (distance <= distanceToNextPoint && distance >= 0)
            {
                // If the gap is bigger to the distance to move and there's distance to move.
                // Moves towards the next point.
                Position += Position.DirectionTo(path[0]) * distance;

                // Ajust the view of the enemy.
                sprite.FlipH = Position.DirectionTo(path[0]).x < 0 ? true : false;
            }
            else
            {
                // If the gap is smaller to the distance to move or there's distance to move.
                // Poll the head of the path.
                Position = path[0];
                aux = path.ToList<Vector2>();
                aux.RemoveAt(0);
                path = aux.ToArray<Vector2>();
            }

            // The distance to walk is reduced by the movement already performed
            distance -= distanceToNextPoint;
        }
    }

    public void GetPlayerPosition(Vector2 playerPosition)
    {   
        path = isActive ? map.GetSimplePath(GlobalPosition, playerPosition) : null;
    }

    public void SetActivation(bool activationMode)
    {
        Visible = activationMode;
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", !activationMode);
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

    public bool IsActive()
    {
        return isActive;
    }
}
