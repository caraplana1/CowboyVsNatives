using Godot;
using System;

public class PlayerController : Sprite
{
    // Movement
    private Area2D area;
    private Vector2 direction;
    [Export]private float speed;


    private Rect2 viewport;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      area = GetChild<Area2D>(0);
      viewport = GetViewportRect();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
      #region Input Getter

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
      #endregion
  
      Translate(direction * speed * delta);
            
  }

}
