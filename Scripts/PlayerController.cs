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
	private Timer ShootTimer;

	private bool readyToShoot;
	[Signal] private delegate void Shooting(Vector2 position, float degrees);
	[Signal] private delegate void SharePosition(Vector2 _position);

	private const float phi = (float) Math.PI / 180;

	#endregion

	public override void _Ready()
	{
	  // Getting Children.
	  pistolPosition = new Position2D[4];
	  viewport = GetViewportRect();
	  sprite = GetChild<Sprite>(0);
	  animationPlayer = GetChild<AnimationPlayer>(1);
	  ShootTimer = GetChild<Timer>(6);

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
		ApplyCentralImpulse(direction * speed);
	}

	#region Methods

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

		if (Input.IsActionPressed("move_up") && Position.y > 80)
		{
			direction.y -= 1;
		}
		if (Input.IsActionPressed("move_down") && Position.y < viewport.Size.y - 80)
		{
			direction.y += 1;
		}
		if (Input.IsActionPressed("move_left") && Position.x > 80)
		{
			direction.x -= 1;
		}
		if (Input.IsActionPressed("move_right") && Position.x < viewport.Size.x - 80)
		{
			direction.x += 1;
		}

		direction = direction.Normalized();
	}

	void FireInput()
	{
		if (Input.IsActionPressed("fire_up") && readyToShoot)
		{

			Shoot(pistolPosition[0].GlobalPosition, 0, 4);

		}
		else if (Input.IsActionPressed("fire_down") && readyToShoot)
		{

			Shoot(pistolPosition[1].GlobalPosition, 180 * phi, 3);

		}
		else if (Input.IsActionPressed("fire_left") && readyToShoot)
		{

			Shoot(pistolPosition[2].GlobalPosition, -90 * phi, 6);

		}
		else if (Input.IsActionPressed("fire_right") && readyToShoot)
		{

			Shoot(pistolPosition[3].GlobalPosition, 90 * phi, 5);

		}
	}

	void Shoot(Vector2 _position, float angle, int frame)
	{
		// Funtion to set the correct frame, start the timer to shoot and emmit the signal.
		sprite.Frame = frame;

		readyToShoot = false;
		ShootTimer.Start();
		EmitSignal("Shooting", _position, angle);
	}

	void ReadyToShoot()
	{
		readyToShoot = true;
	}

	void OnEmitPositionTimer()
	{
		EmitSignal("SharePosition", GlobalPosition);
	}

	#endregion
}
