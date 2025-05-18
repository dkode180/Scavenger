using Godot;
using System;


public partial class Orb : Area2D
{
	public static bool flag=true;
	public static int orbesRecogidos = 0;
	private AudioStreamPlayer2D sonido;

	public override void _Ready()
	{
		// Conecta la señal a un método local
		BodyEntered += OnBodyEntered;
		AddToGroup("orbs");
		sonido = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");

	}

	private void OnBodyEntered(Node body)
	{
		if (body is Player)
		{

			GD.Print("¡Orb recogido!");
			Player.porcentaje = 100;
			orbesRecogidos++;
			flag = true;
			sonido.Play();//No suena?
			QueueFree(); // Elimina el orb
		
			
		}
	}
}
