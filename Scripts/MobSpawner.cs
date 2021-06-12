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

    // Spaw Logic.
    [Export] float minSpawTime , maxSpawTime; 
    [Export] int minperturn, maxperturn;

    #endregion

    public override void _Ready()
    {
        // Brothers
        PlayerController player = GetParent().GetChild<PlayerController>(1);

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
            player.Connect("SharePosition", enemy[i], "GetPlayerPosition");
        }

        player.Connect("GameOver", this, "DisableEnemies");
        GetParent<Main>().Connect("NewGame", this, "StartNewGame");

        if (minSpawTime > maxSpawTime){ minSpawTime = maxSpawTime; }
        if (minperturn > maxperturn){ minperturn = maxperturn; }

    }

    private void StartNewGame()
    {
        ChangeSpawnTime();
    }
    private void DisableEnemies()
    {
        for (int i = 0; i < enemy.Length; i ++)
        {
            if(enemy[i].IsActive())
            {
                enemy[i].SetActivation(false);
            }
        }

        spawnTimer.Stop();
    }

    #region  Spawn Logic
    private void OnSpawnTimeOut()
    {
        Spawn();
        ChangeSpawnTime();
    }

    void Spawn()
    {
        Random rand = new Random();

        for (int i = 0; i < rand.Next(minperturn, maxperturn); i ++)
        {
            enemy[enemycounter].Position = spawnPoints[rand.Next(4)].GlobalPosition;
            enemy[enemycounter].SetActivation(true);

            if (enemycounter == amountEnemys - 1)
            {
                enemycounter = 0;
            }
            else
            {
                enemycounter ++;
            }

        }
    }

    void ChangeSpawnTime()
    {
        Random rand = new Random();
        spawnTimer.WaitTime = (float) rand.NextDouble() * (maxSpawTime - minSpawTime) + minSpawTime;
        spawnTimer.Start();
    }
    #endregion
}
