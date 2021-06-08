using Godot;
using System;

public class Main : Node
{
    #region Field Declaration

    // Bullets.
    [Export]private PackedScene bulletScene;
    private Bullet[] bullet;
    [Export]private int amountBullets = 12; 
    private int bulletCounter;

    #endregion

    public override void _Ready()
    {
        // Bullets Creation;
        bullet = new Bullet[amountBullets];
        bulletCounter = 0;
        for (int i = 0; i < amountBullets; i++)
        {
            bullet[i] = (Bullet) bulletScene.Instance();
            AddChild(bullet[i]);
            bullet[i].SetActivation(false);
        }

        GetChild(1).Connect("Shooting", this, "OnShootingBullet");
    }

    private void OnShootingBullet(Vector2 position, float rotation)
    {
        bullet[bulletCounter].SetActivation(true);
        bullet[bulletCounter].Position = position;
        bullet[bulletCounter].Rotation = rotation;

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
