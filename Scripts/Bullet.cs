using Godot;

public class Bullet : Area2D
{
	#region  Field Declaration

	[Export]private float speed;
	private bool isActive;

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

	///<summary>
    ///Set the object disable from the tree.
    ///</summary>
	public void SetActivation(bool activationMode)
	{
		Visible = activationMode;
		SetProcess(activationMode);
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", !activationMode);
		isActive = activationMode;
	}

	public bool IsActive()
	{
		return isActive;
	}

}
