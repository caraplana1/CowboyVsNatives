using Godot;
using System;

public class Bullet : Sprite
{
    [Export]private float speed;

    public override void _Process(float delta)
    {
        Translate(Vector2.Up.Rotated(Rotation) * speed * delta);
    }

    private void OnBody2DEntered()
    {
        Visible = !Visible;
    }

}
