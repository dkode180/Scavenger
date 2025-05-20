using Godot;
using System;
using System.Diagnostics;

public partial class Orb : Area2D
{
	public static bool flag = true;
	public static int orbesRecogidos = 0;
	private AudioStreamPlayer2D sonido;
	private AudioStreamPlayer2D s;

	public static bool orbesCompletados = false;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		AddToGroup("orbs");
		sonido = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
	}

	public override void _Process(double delta)
	{
		
	}

	private void OnBodyEntered(Node body)
	{
		if (body is Player)
		{
			if (orbesRecogidos == 9)
			{
				
				s = GetNode<AudioStreamPlayer2D>("/root/Game/SonidoWin");
				s.Play();

				Player.disparar = false;
				orbesCompletados = true;

				foreach (Node node in GetTree().GetNodesInGroup("enemies"))
				{
					node.QueueFree();
				}

				var spawnComponent = GetTree().Root.FindChild("spawnComponent", true, false) as Node;
				if (spawnComponent != null)
				{
					var timer = spawnComponent.GetNode<Timer>("TimerSpawn");
					timer.Stop();
				}
				else
				{
					Debug.WriteLine("No pilla el timer");
				}
			}
			else
			{
				SpawnComponent.transparency = 0.5f;
				Player.porcentaje = 100;
				orbesRecogidos++;
				flag = true;
			}

			((Player)body).ReproducirSonidoOrb();
			QueueFree(); // Elimina el orb
		}
	}
}
