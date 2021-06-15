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

    private UserInterface ui;

    #endregion

    public override void _Ready()
    {

        player = GetChild<PlayerController>(1);
        ui = GetChild<UserInterface>(3);

        player.Connect("Shooting", this, "OnShootingBullet");
        player.Connect("GameOver", ui, "GameOver");
        Connect("NewGame", player, "PlayerNewGame");

        // Bullets Creation;
        bullet = new Bullet[amountBullets];
        bulletCounter = 0;
        for (int i = 0; i < amountBullets; i++)
        {
            bullet[i] = (Bullet) bulletScene.Instance();
            AddChild(bullet[i]);
            bullet[i].SetActivation(false);
            bullet[i].Connect("NativeKilled", ui, "IncreasePoints");
        }

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
