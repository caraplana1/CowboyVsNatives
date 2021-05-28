using Godot;
using System;

public class Mob_Spawner : Node
{
    // Enemys
    [Export]private PackedScene enemyScene = ResourceLoader.Load<PackedScene>("res://Prefabs/Native.tscn");
    private Native enemy;
    private Navigation2D map;

    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
