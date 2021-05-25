using Godot;
using System;

public class Main : Node
{
    #region Field Declaration

    // Bullets.
    [Export]private PackedScene bulletScene;
    private RigidBody2D[] bullet;
    [Export]private int amountBullets = 11; 
    private int bulletCounter;

    #endregion

    public override void _Ready()
    {
        // Bullets Creation;
        bullet = new RigidBody2D[amountBullets];
        bulletCounter = 0;
        for (int i = 0; i < amountBullets; i++)
        {
            bullet[i] = (RigidBody2D)bulletScene.Instance();
            AddChild(bullet[i]);
            bullet[i].Visible = false;
        }

        GetChild(1).Connect("Shooting", this, "OnShootingBullet");
    }

    private void OnShootingBullet(Vector2 position,float rotation)
    {
        bullet[bulletCounter].Position = position;
        bullet[bulletCounter].Rotation = rotation;
        bullet[bulletCounter].Visible = true;

        if (bulletCounter >= amountBullets - 1)
        {
            bulletCounter = 0;
        }
        else
        {
            bulletCounter++;
        }
    }
}
