using Godot;
using System;

public class Bullet : Area2D
{
	#region  Field Declaration

	// Speed is set in the editor and aux is the valu that multiply the vector
	// aux is a copy of speed but change, speed doesn't
	[Export]private float speed;
	private float aux;

	[Signal]private delegate void NativeKilled();

	#endregion

	public override void _Ready()
	{
		aux = speed;
	}

	public override void _Process(float delta)
	{
		Translate(Vector2.Up.Rotated(Rotation) * aux * delta);
	}

	private void OnBodyEntered(Node body)
	{
		aux = 0;
		Visible = false;

		if (body.IsInGroup("Enemy"))
		{
			EmitSignal("NativeKilled");
		}
	}

	public void RestoreSpeed()
	{
		aux = speed;
	}

}
