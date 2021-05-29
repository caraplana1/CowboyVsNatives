using Godot;
using System;

public class MobSpawner : Node
{
    #region Field Declaration

    // Enemys
    [Export]private PackedScene enemyScene;
    private Native[] enemy;
    [Export]private int amountEnemy = 0;
    private int enemyCounter;

    private Position2D[] spawnPositions;
    private Timer spawnTimer;
    
    private RandomNumberGenerator indx;

    #endregion

    public override void _Ready()
    {
        //indx.Randomize();

        // Enemy Creation.
        enemy = new Native[amountEnemy];
        spawnPositions = new Position2D[4];
        enemyCounter = 0;

        for (int i = 0; i < amountEnemy; i ++)
        {
            enemy[i] = enemyScene.Instance<Native>();
            AddChild(enemy[i]);
            enemy[i].SetActivation(false);
        }

        // Getting Children.
        for (int j = 0; j < 4 ; j++)
        {
            spawnPositions[j] = GetChild<Position2D>(j);
        }

        spawnTimer = GetChild<Timer>(4);
    }

    private void OnSpawnTimeOut()
    {
        enemy[enemyCounter].GlobalPosition = spawnPositions[indx.RandiRange(0,3)].GlobalPosition;
        enemy[enemyCounter].SetActivation(true);

        if (enemyCounter == amountEnemy - 1)
        {
            enemyCounter = 0;
        }
        else
        {
            enemyCounter ++;
        }
    }
}
