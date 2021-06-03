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
    private Vector2 direction = Vector2.Zero;

    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // children
        sprite = GetChild<Sprite>(0);
        collision = GetChild<CollisionShape2D>(1);
        animationPlayer = GetChild<AnimationPlayer>(2);

        map = GetParent().GetParent().GetChild<Navigation2D>(0);

        SetPhysicsProcess(false);
    }

    public override void _Process(float delta)
    {
        if (direction != Vector2.Zero)
        {
            animationPlayer.Play("Walking");
        }
    }

    public override void _PhysicsProcess(float delta)
    {
       ApplyCentralImpulse(direction * speed);
    }

    public void OnChase(Vector2 playerPosition)
    {   
        if (isActive)
        {
            path = map.GetSimplePath(GlobalPosition, playerPosition);
            
            for (int i = 0; i < path.Length; i++)
            {
                path[i] -= GlobalPosition;
            }

            // GetNode<Line2D>("Line2D").Points = path; // Show the enemy path.
            
        }
    }

    public void SetActivation(bool activationMode)
    {
        Visible = activationMode;
        collision.Disabled = !activationMode;
        Sleeping = !activationMode;
        SetPhysicsProcess(activationMode);
        isActive = activationMode;
    }

}
