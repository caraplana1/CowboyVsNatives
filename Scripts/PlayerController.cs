using Godot;
using System;

public class PlayerController : Sprite
{

    #region Field Declaration.
    // Movement
    private Vector2 direction;
    [Export]private float speed;

    private Rect2 viewport;

// Bullet
    private PackedScene bulletPackage;
    private Sprite[] bullet;
    [Export]private int amountBullets;
    private int bulletCounter;

    // Children
    private Area2D area;
    private AnimationPlayer animationPlayer;
    private Position2D[] pistolPosition;
    

    #endregion

    public override void _Ready()
    {
      pistolPosition = new Position2D[4];
      viewport = GetViewportRect();
      area = GetChild<Area2D>(0);
      animationPlayer = GetChild<AnimationPlayer>(1);
      for (int i = 0; i < 4; i++)
      {
        pistolPosition[i] = GetChild<Position2D>(i+2);
      }
      bulletPackage = (PackedScene)ResourceLoader.Load("res://Prefabs/Bullet.tscn");

      bullet = new Sprite[amountBullets];

      for (int j = 0; j < amountBullets; j++)
      {
        bullet[j] = (Sprite)bulletPackage.Instance();
        AddChild(bullet[j]);
      }
      bulletCounter = 0;
    }


    public override void _Process(float delta)
    {
        InputManage();
  
        Translate(direction * speed * delta);

        FireInput();

        Animation();          
    }

  void Animation()
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
      if (Input.IsActionPressed("fire_up"))
      {
          bullet[bulletCounter].Position = pistolPosition[0].Position;
          if (bulletCounter < amountBullets)
          {
            bulletCounter++;
          }
          else
          {
            bulletCounter = 0;
          }
      }
      if (Input.IsActionPressed("fire_down"))
      {
        bullet[bulletCounter].Position = pistolPosition[1].Position;
        if (bulletCounter < amountBullets)
          {
            bulletCounter++;
          }
          else
          {
            bulletCounter = 0;
          }
      }
      if (Input.IsActionPressed("fire_left"))
      {
          bullet[bulletCounter].Position = pistolPosition[2].Position;
          if (bulletCounter < amountBullets)
          {
            bulletCounter++;
          }
          else
          {
            bulletCounter = 0;
          }
      }
      if (Input.IsActionPressed("fire_right"))
      {
          bullet[bulletCounter].Position = pistolPosition[3].Position;
          if (bulletCounter < amountBullets)
          {
            bulletCounter++;
          }
          else
          {
            bulletCounter = 0;
          }
      }
  }

}
