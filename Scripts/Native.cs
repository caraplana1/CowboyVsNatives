using Godot;
using System;

public class Native : RigidBody2D
{
    #region  Field Declaration

    [Export]private float speed ;
    private Navigation2D map;
    private Vector2[] path;

    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetParent().GetParent().GetChild(1).Connect("SharePosition", this, "OnChase");
        map = GetParent().GetParent().GetChild<Navigation2D>(0);
    }

    void OnChase(Vector2 playerPosition)
    {
       GetChild<Line2D>(3).Points = map.GetSimplePath(GlobalPosition, playerPosition);
    }
}
