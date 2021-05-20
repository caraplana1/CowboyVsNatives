using Godot;
using System;

public class Main : Node
{
    #region Field Declaration

    // Bullets.
    private PackedScene bulletScene;
    private Sprite[] bullet;
    [Export]private int amountBullets; 
    private int bulletCounter;

    #endregion

    public override void _Ready()
    {
        // Bullets Creation;
        bulletScene = (PackedScene) ResourceLoader.Load("res://Prefabs/Bullet.tscn");
        bullet = new Sprite[amountBullets];
        bulletCounter = 0;
        for (int i = 0; i < amountBullets; i++)
        {
            bullet[i] = (Sprite)bulletScene.Instance();
            AddChild(bullet[i]);
            // bullet[i].Position = new Vector2(50, 50);
            bullet[i].Visible = false;
        }

        GetChild(0).Connect("Shooting", this, "OnShootingBullet");
    }

    private void OnShootingBullet(Vector2 position,float rotation)
    {
        GD.Print(position);
        bullet[bulletCounter].Position = position.Rotated(rotation);
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
