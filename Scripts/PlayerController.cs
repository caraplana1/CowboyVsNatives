using Godot;
using System;

public class PlayerController : Sprite
{

    #region Field Declaration.
    // Movement
    private Area2D area;
    private Vector2 direction;
    [Export]private float speed;


    private AnimationPlayer animationPlayer;
    private Rect2 viewport;
    [Export]private Sprite Bullet;

    #endregion

    public override void _Ready()
    {
      viewport = GetViewportRect();
      area = GetChild<Area2D>(0);
      animationPlayer = GetChild<AnimationPlayer>(1);
    }


    public override void _Process(float delta)
    {
        InputManage();
  
        Translate(direction * speed * delta);

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

}
