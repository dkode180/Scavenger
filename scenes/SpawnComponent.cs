using Godot;
using Microsoft.VisualBasic;
using System;

public partial class SpawnComponent : Node2D
{

	private float time;
	// private Node2D enemy;

	[Export]
	public PackedScene EnemyScene;

	// Called when the node enters the scene tree for the first time.
	
	public override void _Ready()
	{
		time = (float)GetNode<Timer>("TimerSpawn").WaitTime;
		// enemy = GetNode<Node2D>("Enemy");
		
	}
	public Vector2 getSpawnPoint()
	{
		Random r = new Random();
		var screen_size = GetViewport().GetVisibleRect().Size;
		var randomSide = r.Next(0, 4);
		var randomPosition = Vector2.Zero;
		switch (randomSide)
		{
			case 0:
				randomPosition.X = r.Next(0, (int)screen_size.X);
				break;
			case 1:
				randomPosition.Y = r.Next(0, (int)screen_size.Y); 
				randomPosition.X=screen_size.X;
				break;
			case 2:
				randomPosition.X = r.Next(0, (int)screen_size.X);
				randomPosition.Y=screen_size.Y;
				break;
			case 3:
				randomPosition.Y = r.Next(0, (int)screen_size.Y);
				break;
		}
		return randomPosition;

	}

	// public void spawn(){
	// 	var newEnemy= new Enemy
	// }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
