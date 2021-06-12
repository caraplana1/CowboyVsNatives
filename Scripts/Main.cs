using Godot;

public class Main : Node
{
    #region Field Declaration

    // Bullets.
    [Export]private PackedScene bulletScene;
    private Bullet[] bullet;
    [Export]private int amountBullets = 12; 
    private int bulletCounter;


    private PlayerController player;
    [Signal] private delegate void NewGame();

    #endregion

    public override void _Ready()
    {

        player = GetChild<PlayerController>(1);

        // Bullets Creation;
        bullet = new Bullet[amountBullets];
        bulletCounter = 0;
        for (int i = 0; i < amountBullets; i++)
        {
            bullet[i] = (Bullet) bulletScene.Instance();
            AddChild(bullet[i]);
            bullet[i].SetActivation(false);
        }

        player.Connect("Shooting", this, "OnShootingBullet");
        Connect("NewGame", player, "PlayerNewGame");
    }

    private void OnShootingBullet(Vector2 position, float rotation)
    {
        bullet[bulletCounter].SetActivation(true);
        bullet[bulletCounter].Position = position;
        bullet[bulletCounter].Rotation = rotation;

        if (bulletCounter == amountBullets - 1)
        {
            bulletCounter = 0;
        }
        else
        {
            bulletCounter++;
        }
    }
}
