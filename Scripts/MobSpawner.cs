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
    [Export] int minPerTurn, maxPerTurn;

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

        // If min number > max number then their are the same else min keep their value.
        minSpawTime = minSpawTime > maxSpawTime ? maxSpawTime : minSpawTime;
        minPerTurn = minPerTurn > maxPerTurn ? maxPerTurn : minPerTurn;

    }

    private void StartNewGame()
    {
        ChangeSpawnTime();
    }

    private void GameOver()
    {
        DisableAllEnemies();
    }
    
    private void DisableAllEnemies()
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
        // Spawn a random amount of enemy, between minPerTurn and maxPerTurn,
        // and then move them to random positions each.
        Random rand = new Random();

        for (int i = 0; i < rand.Next(minPerTurn, maxPerTurn); i ++)
        {
            enemy[enemycounter].Position = spawnPoints[rand.Next(4)].GlobalPosition;
            enemy[enemycounter].SetActivation(true);

            enemycounter = enemycounter == amountEnemys - 1 ? 0 : ++enemycounter ;
        }
    }

    void ChangeSpawnTime()
    {
        // Changes the timer in a number between the minSpawTime and maxSpawTime.
        Random rand = new Random();
        float randNumber;

        randNumber = (float) rand.NextDouble() * (maxSpawTime - minSpawTime) + minSpawTime;
        spawnTimer.WaitTime = randNumber;
        spawnTimer.Start();
    }
    #endregion
}
