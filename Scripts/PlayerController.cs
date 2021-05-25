using Godot;
using System;

public class PlayerController : RigidBody2D
{

	#region Field Declaration.

	// Movement
	private Vector2 direction;
	[Export]private float speed = 3;


	private Rect2 viewport;

	// Children
	private AnimationPlayer animationPlayer;
	private Position2D[] pistolPosition;
	private Sprite sprite;

	private bool readyToShoot;
	[Signal] private delegate void Shooting(Vector2 position, float degrees);
	private const float phi = (float)Math.PI / 180;

	#endregion

	public override void _Ready()
	{
	  // Getting Children.
	  pistolPosition = new Position2D[4];
	  viewport = GetViewportRect();
	  sprite = GetChild<Sprite>(0);
	  animationPlayer = GetChild<AnimationPlayer>(1);
	  for (int i = 0; i < 4; i++)
	  {
		pistolPosition[i] = GetChild<Position2D>(i+2);
	  }
	}

	public override void _Process(float delta)
	{
		InputManage();

		Animation();

		FireInput();          
	}

	public override void _PhysicsProcess(float delta)
	{	
		LinearVelocity = direction * speed;
	}

	void Animation()
	{
		if (readyToShoot)
		{
			if (direction != Vector2.Zero)
			{
				animationPlayer.Play("Walk");
			}
			else
			{
				animationPlayer.Stop(true);
				sprite.Frame = 0;
			}
		}
	}

	void InputManage()
	{
		direction = Vector2.Zero;

		if (Input.IsActionPressed("move_up") && Position.y > 20)
		{
			direction.y -= 1;
		}
		if (Input.IsActionPressed("move_down") && Position.y < viewport.Size.y - 20)
		{
			direction.y += 1;
		}
		if (Input.IsActionPressed("move_left") && Position.x > 20)
		{
			direction.x -= 1;
		}
		if (Input.IsActionPressed("move_right") && Position.x < viewport.Size.x - 20)
		{
			direction.x += 1;
		}

		direction = direction.Normalized();
	}

	void FireInput()
	{
		if (Input.IsActionPressed("fire_up") && readyToShoot)
		{
			readyToShoot = false;
			sprite.Frame = 4;
			EmitSignal("Shooting", pistolPosition[0].GlobalPosition, 0);
		}
		else if (Input.IsActionPressed("fire_down") && readyToShoot)
		{
			readyToShoot = false;
			sprite.Frame = 3;
			EmitSignal("Shooting", pistolPosition[1].GlobalPosition, 180 * phi);
		}
		else if (Input.IsActionPressed("fire_left") && readyToShoot)
		{
			readyToShoot = false;
			sprite.Frame = 6;
			EmitSignal("Shooting", pistolPosition[2].GlobalPosition, -90 * phi);
		}
		else if (Input.IsActionPressed("fire_right") && readyToShoot)
		{
			readyToShoot = false;
			sprite.Frame = 5;
			EmitSignal("Shooting", pistolPosition[3].GlobalPosition, 90 * phi);
		}
	}

	void ReadyToShoot()
	{
		GetChild<Timer>(6).Start();
		readyToShoot = true;
	}

}
