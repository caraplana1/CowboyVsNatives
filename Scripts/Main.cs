using Godot;

public class Main : Node
{
    #region Field Declaration

    // Bullets.
    [Export]private PackedScene bulletScene;
    private Bullet[] bullet;
    [Export]private int amountBullets = 12; 
    private int bulletCounter;

    [Signal] private delegate void NewGame();

    #endregion

    public override void _Ready()
    {
        // Signal Connections.
        PlayerController player;
        MobSpawner spawner;
        UserInterface ui;

        player = GetChild<PlayerController>(1);
        spawner = GetChild<MobSpawner>(2);
        ui = GetChild<UserInterface>(3);

        player.Connect("Shooting", this, "OnShootingBullet");

        player.Connect("GameOver", ui, "GameOver");
        player.Connect("GameOver", this, "GameOver");
        player.Connect("GameOver", spawner, "GameOver");


        Connect("NewGame", player, "StartNewGame");
        Connect("NewGame", spawner, "StartNewGame");
        ui.Connect("ButtonNewGamePressed", this, "StartNewGame");

        // Bullets Creation;
        bullet = new Bullet[amountBullets];
        bulletCounter = 0;
        // Instace n quantity of bullets and disable all the bullets.
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

    private void DeativateAllBullets()
    {
        for (int i = 0; i < bullet.Length; i++)
        {
            if( bullet[i].IsActive())
            {
                bullet[i].SetActivation(false);
            }
        }
    }

    void StartNewGame()
    {
        EmitSignal("NewGame");
    }

    void GameOver()
    {
        DeativateAllBullets();
    }
}
