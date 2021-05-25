using Godot;
using System;

public class Bullet : RigidBody2D
{
	[Export]private float speed;

	public override void _PhysicsProcess(float delta)
	{
		LinearVelocity = Vector2.Up.Rotated(Rotation) * speed;
	}

	private void OnBodyEntered(Node body)
	{
		Visible = false;
	}
}
