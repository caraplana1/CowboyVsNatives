using Godot;

public class Main : Node
{
    #region Field Declaration

    // Bullets.
    [Export]private PackedScene bulletScene;
    private Bullet[] bullet;
    [Export]private int amountBullets = 12; 
    private int bulletCounter;

    // Sound
    private AudioStreamPlayer2D music;
    private AudioStreamPlayer2D gameOverSound;
    private Timer StopGameOverSoundTimer;

    [Signal] private delegate void NewGame();

    #endregion

    public override void _Ready()
    {
        #region Children Signal Connections.
        PlayerController player;
        MobSpawner spawner;
        UserInterface ui;

        player = GetChild<PlayerController>(1);
        spawner = GetChild<MobSpawner>(2);
        ui = GetChild<UserInterface>(3);
        music = GetChild<AudioStreamPlayer2D>(4);
        gameOverSound = GetChild<AudioStreamPlayer2D>(5);
        StopGameOverSoundTimer = GetChild<Timer>(6);

        player.Connect("Shooting", this, "OnShootingBullet");

        player.Connect("GameOver", ui, "GameOver");
        player.Connect("GameOver", this, "GameOver");
        player.Connect("GameOver", spawner, "GameOver");


        Connect("NewGame", player, "StartNewGame");
        Connect("NewGame", spawner, "StartNewGame");
        ui.Connect("ButtonNewGamePressed", this, "StartNewGame");

        #endregion

        player.SetActive(false);

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

        bulletCounter = bulletCounter == amountBullets-1 ? 0 : ++bulletCounter;
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
        music.Play();
        gameOverSound.Stop();
    }

    void GameOver()
    {
        gameOverSound.Play(0.45f);
        StopGameOverSoundTimer.Start();
        DeativateAllBullets();
        music.Stop();
    }

    void StopGameOverSound()
    {
        gameOverSound.Stop();
    }
}
