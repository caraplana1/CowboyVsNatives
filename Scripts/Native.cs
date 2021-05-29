using Godot;
using System;

public class Native : RigidBody2D
{
    #region  Field Declaration

    [Export]private float speed;

    // Children
    private CollisionShape2D collision;
    
    // Navegation
    private Vector2[] path;
    private Navigation2D map;

    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        collision = GetChild<CollisionShape2D>(1);

        SetPhysicsProcess(false);
        GetParent().GetParent().GetChild(1).Connect("SharePosition", this, "OnChase");
        map = GetParent().GetParent().GetChild<Navigation2D>(0);
    }

    public override void _PhysicsProcess(float delta)
    {
        MoveAlongPath(speed);
    }

    private void MoveAlongPath(float enemySpeed)
    {
        Vector2 startPosition = GlobalPosition; 
        float nextPoint;


        for (int i = 0; i < path.Length; i++)
        {
            nextPoint = startPosition.DistanceTo(path[i]);
        }
    }

    private void OnChase(Vector2 playerPosition)
    {
        path = map.GetSimplePath(this.GlobalPosition, playerPosition);
        SetPhysicsProcess(true);
    }

    public void SetActivation(bool activationMode)
    {
        Visible = activationMode;
        collision.Disabled = !activationMode;
        SetPhysicsProcess(activationMode);
    }

}
