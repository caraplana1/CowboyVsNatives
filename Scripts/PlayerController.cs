using Godot;
using System;

public class PlayerController : Sprite
{

	#region Field Declaration.

	// Movement
	private Vector2 direction;
	[Export]private float speed;

	private Rect2 viewport;

	// Children
	private Area2D area;
	private AnimationPlayer animationPlayer;
	private Position2D[] pistolPosition;

	private bool readyToShoot;
	[Signal] private delegate void Shooting(Vector2 position, float degrees);
	private const float phi = (float)Math.PI / 180;

	#endregion

	public override void _Ready()
	{
	  // Getting Children.
	  pistolPosition = new Position2D[4];
	  viewport = GetViewportRect();
	  area = GetChild<Area2D>(0);
	  animationPlayer = GetChild<AnimationPlayer>(1);
	  for (int i = 0; i < 4; i++)
	  {
		pistolPosition[i] = GetChild<Position2D>(i+2);
	  }
	}

	public override void _Process(float delta)
	{
		InputManage();
  
		Translate(direction * speed * delta);

		Animation();

		FireInput();          
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
				Frame = 0;
			}
		}
	}

	void InputManage()
	{
		direction = Vector2.Zero;

		if (Input.IsActionPressed("move_up") && Position.y > 0)
		{
			direction.y -= 1;
		}
		if (Input.IsActionPressed("move_down") && Position.y < viewport.Size.y)
		{
			direction.y += 1;
		}
		if (Input.IsActionPressed("move_left") && Position.x > 0)
		{
			direction.x -= 1;
		}
		if (Input.IsActionPressed("move_right") && Position.x < viewport.Size.x)
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
			Frame = 4;
			EmitSignal("Shooting", pistolPosition[0].GlobalPosition, 0);
		}
		if (Input.IsActionPressed("fire_down") && readyToShoot)
		{
            readyToShoot = false;
			Frame = 3;
			EmitSignal("Shooting", pistolPosition[1].GlobalPosition, 180 * phi);
		}
		if (Input.IsActionPressed("fire_left") && readyToShoot)
		{
            readyToShoot = false;
			Frame = 6;
			EmitSignal("Shooting", pistolPosition[2].GlobalPosition, -90 * phi);
		}
		if (Input.IsActionPressed("fire_right") && readyToShoot)
		{
            readyToShoot = false;
			Frame = 5;
			EmitSignal("Shooting", pistolPosition[3].GlobalPosition, 90 * phi);
		}
	}

    void ReadyToShoot()
    {
        GetChild<Timer>(6).Start();
        readyToShoot = true;
    }

}
