using Godot;
using System;

public class MobSpawner : Node
{
    #region Field Declaration
    // Children.
    private Position2D[] spawnPoints;
    private Timer spawnTimer;


    // Enemy.
    [Export] private PackedScene enemyScene;
    private Native[] enemy;
    [Export] private int amountEnemys;
    private int enemycounter;
    private Navigation2D map;

    #endregion

    public override void _Ready()
    {
        // Brothers
        PlayerController player = GetParent().GetChild<PlayerController>(1);
        map = GetParent().GetChild<Navigation2D>(0);

        // Children
        spawnPoints = new Position2D[4];
        for (int i = 0; i < 4; i ++)
        {
            spawnPoints[i] = GetChild<Position2D>(i);
        }
        spawnTimer = GetChild<Timer>(4);


        // Enemy Creation & config.
        enemy = new Native[amountEnemys];
        enemycounter = 0;

        for (int i = 0; i < amountEnemys; i ++)
        {
            enemy[i] = enemyScene.Instance<Native>();
            AddChild(enemy[i]);
            enemy[i].SetActivation(false);
            enemy[i].GlobalPosition = spawnPoints[0].GlobalPosition;
        }

        player.Connect("SharePosition", this, "OnGettingTarget");
        enemy[0].SetActivation(true);
    }

    void OnGettingTarget(Vector2 playerPosition)
    {
        for (int i = 0; i < amountEnemys; i ++)
        {
            if (enemy[i].IsActive())
            {
                enemy[i].FollowPath(map.GetSimplePath(enemy[i].GlobalPosition, playerPosition));
            }
        }
    }

    private void OnSpawnTimeOut()
    {

    }
}
