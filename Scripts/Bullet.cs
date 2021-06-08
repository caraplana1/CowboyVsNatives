using Godot;
using System;

public class Bullet : Area2D
{
	#region  Field Declaration

	// Speed is set in the editor and aux is the valu that multiply the vector
	// aux is a copy of speed but change, speed doesn't
	[Export]private float speed;

	[Signal]private delegate void NativeKilled();

	#endregion

	public override void _Process(float delta)
	{
		Translate(Vector2.Up.Rotated(Rotation) * speed * delta);
	}

	private void OnBodyEntered(Node body)
	{
		SetActivation(false);

		if (body.IsInGroup("Enemy"))
		{
			Native enemy = (Native)body;
			enemy.SetActivation(false);
			EmitSignal("NativeKilled");
		}
	}

	public void SetActivation(bool activationMode)
	{
		Visible = activationMode;
		SetProcess(activationMode);
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", !activationMode);
	}

}
