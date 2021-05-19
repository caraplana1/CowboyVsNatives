using Godot;
using System;

public class Bullet : Sprite
{
    [Export]private float speed;

    public override void _Process(float delta)
    {
        Translate(Vector2.Up * speed * delta);
    }

    private void OnArea2DEntered(Area2D area)
    {
        if (area.Name != "Player")
        {
            this.QueueFree();
        }
    }
}
