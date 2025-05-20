using Godot;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;

public partial class SpawnComponent : Node2D
{

	private float time;


	//Variables para colores
	private float t = 0f; // Interpolador entre 0 y 1
	private Color startColor = new Color(1f, 0f, 1f); // Morado
	private Color endColor = new Color(1f, 1f, 1f);   // Blanco
	[Export]
	public float ColorChangeSpeed = 0.05f; // Cuánto tarda en cambiar, ajústalo si va muy lento o muy rápido

	[Export]
	public PackedScene EnemyScene;

	[Export]
	public PackedScene OrbScene;
	public static float transparency=0.5f;
	private int con = 0;

	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{

		time = (float)GetNode<Timer>("TimerSpawn").WaitTime;

		// Conectar la señal timeout al método spawn
		GetNode<Timer>("TimerSpawn").Timeout += spawn;

	}



	// public Vector2 getSpawnPoint()
	// {
	// 	Random r = new Random();
	// 	var screen_size = GetViewport().GetVisibleRect().Size;
	// 	var randomSide = r.Next(0, 4);
	// 	var randomPosition = Vector2.Zero;
	// 	switch (randomSide)
	// 	{
	// 		case 0:
	// 			randomPosition.X = r.Next(0, (int)screen_size.X);
	// 			break;
	// 		case 1:
	// 			randomPosition.Y = r.Next(0, (int)screen_size.Y);
	// 			randomPosition.X = screen_size.X;
	// 			break;
	// 		case 2:
	// 			randomPosition.X = r.Next(0, (int)screen_size.X);
	// 			randomPosition.Y = screen_size.Y;
	// 			break;
	// 		case 3:
	// 			randomPosition.Y = r.Next(0, (int)screen_size.Y);
	// 			break;
	// 	}
	// 	return randomPosition;

	// }
	public Vector2 GetRandomPointInAnnulus(Vector2 center, float innerRadius, float outerRadius)
	{
		Random r = new Random();

		// Ángulo entre 0 y 2pi recuerda float
		float angle = (float)(r.NextDouble() * MathF.PI * 2);

		// Radio en el area del anillo
		//  r² = inner² + rand*(outer² - inner²)
		//Le metes raiz porque si no aparecen muy cerca del inner
		float distance = MathF.Sqrt(
			innerRadius * innerRadius +               // inner²
			(outerRadius * outerRadius - innerRadius * innerRadius) *
			(float)r.NextDouble()                     // rand de 0 a 1
		);

		// Pasamos de (r, θ) a (x, y)
		float x = center.X + distance * MathF.Cos(angle);
		float y = center.Y + distance * MathF.Sin(angle);

		return new Vector2(x, y);
	}

	public Vector2 GetRandomPointInCircle(Vector2 center, float radius)
	{
		Random r = new Random();

		// angulo aleatorio entre 0 y 2pi
		float angle = (float)(r.NextDouble() * Math.PI * 2);

		// Distancia radial aleatoria entre 0 y radius (usamos raiz cuadrada para distribucion uniforme)
		float distance = radius * Mathf.Sqrt((float)r.NextDouble());

		// Convertimos a coordenadas cartesianas que si no se vuelve loco
		float x = center.X + distance * Mathf.Cos(angle);
		float y = center.Y + distance * Mathf.Sin(angle);

		return new Vector2(x, y);
	}
	public void spawn()
	{
		// UpdateSpriteColors();
		con++;
		// Debug.WriteLine("Tiempo " + time);
		// Debug.WriteLine("Con: " + con);
		if (EnemyScene != null)
		{
			// Instancia un nuevo enemigo desde la escena exportada
			Node2D newEnemy = (Node2D)EnemyScene.Instantiate();

			// Posición inicial aleatoria
			newEnemy.Position = GetRandomPointInAnnulus(new Vector2(0, 0), 300, 400);


			// Añade el enemigo al árbol de nodos
			GetTree().Root.AddChild(newEnemy);
		}
		else
		{
			// GD.PrintErr("EnemyScene no está asignado.");
		}
		if (OrbScene != null)
		{
			if (Orb.flag == true)
			{
				// Debug.WriteLine("Entra en la comparacion de timer");


				Node2D newOrb = (Node2D)OrbScene.Instantiate();
				newOrb.Position = GetRandomPointInCircle(new Vector2(0, 0), 235);
				Orb.flag = false;

				GetTree().Root.AddChild(newOrb);//Lo añades al arbol de nodos que si no solo lo creas en memoria
				con = 0;
			}
		}
		else
		{
			// Debug.WriteLine("No esta asignado");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Cambio de color muy lento: ciclo cada 60 segundos
		float hue = ((float)Time.GetTicksMsec() % 60000) / 60000.0f;

		// Oscurecer el color
		float saturation = 1.0f;
		float value = 0.65f;

		Color hsvColor = Color.FromHsv(hue, saturation, value,0.8f);
		Color hsvColorLabels = Color.FromHsv(hue, saturation, value,transparency);


		// PLAYER
		var player = GetTree().Root.FindChild("Player", true, false) as Node;
		var playerSprite = player?.GetNodeOrNull<Sprite2D>("SpriteTrasero");
		if (playerSprite != null)
			playerSprite.Modulate = hsvColor;

		// ENEMIES
		foreach (Node node in GetTree().GetNodesInGroup("enemies"))
		{
			if (node is CharacterBody2D enemy)
			{
				var sprite = enemy.GetNodeOrNull<Sprite2D>("SpriteTrasero");
				if (sprite != null)
					sprite.Modulate = hsvColor;
			}
		}
		//OBRS
		foreach (Node node in GetTree().GetNodesInGroup("orbs"))
		{
			if (node is Area2D orb)
			{
				var sprite = orb.GetNodeOrNull<Sprite2D>("SpriteTrasero");
				if (sprite != null)
					sprite.Modulate = hsvColor;
			}
		}

		// sprites del nodo Game
		var gameNode = GetTree().Root.FindChild("Game", true, false);
		if (gameNode != null)
		{
			foreach (Node child in gameNode.GetChildren())
			{
				if (child.Name != "WhiteBg" && child.Name != "BoundaryCircle" && child is Sprite2D sprite)
				{
					sprite.Modulate = hsvColor;
				}
				if (child is Label label && child.Name == "LabelPorcentaje")
				{
					label.Modulate = hsvColorLabels;
				}
				if ( child is Label label2 && child.Name=="LabelOrbes")
				{
					label2.Modulate=hsvColor;
				}
			}
		}
	}


}