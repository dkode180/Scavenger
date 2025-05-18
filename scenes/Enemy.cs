using Godot;
using System;
using System.Diagnostics;

public partial class Enemy : CharacterBody2D
{
	public const float Speed = 180.0f;
	public const float Deceleration = 80.0f;
	public const float MaxSpeed = 150.0f;
	public const float Acceleration = 400.0f;
	private Label labelOrb;

	private CharacterBody2D player;
	public override void _Ready()
	{
		var hitbox = GetNode<Area2D>("Hitbox");//Error en bodyentered?
		labelOrb = GetNode<Label>("/root/Game/LabelOrbes");
		hitbox.BodyEntered += OnBodyEntered;
		AddToGroup("enemies");
	}
	private void OnBodyEntered(Node body)
	{
		GD.Print("Algo entró en el hitbox: ", body.Name);
		if (body is Player)
		{

			((Player)body).QueueFree();
			foreach (Node node in GetTree().GetNodesInGroup("enemies"))//Solo borra los enemies
			{
				node.QueueFree();
			}
			foreach (Node node in GetTree().GetNodesInGroup("orbs"))//Solo borra los orbes
			{
				node.QueueFree();
			}

			GetTree().ReloadCurrentScene(); // Reinicia la partida pero no sirve con el arraylist de los enemies ni de los orbes
			Player.porcentaje = 100;
			Orb.flag = true;
			Orb.orbesRecogidos = 0;
			SpawnComponent.transparency = 0.5f;


		}
	}

	public override void _PhysicsProcess(double delta)
	{



		player = GetTree().Root.FindChild("Player", true, false) as CharacterBody2D;
		Vector2 velocity = Velocity;

		Vector2 playerPosition = player.GlobalPosition;
		Vector2 direccion = (playerPosition - GlobalPosition).Normalized();
		Rotation = direccion.Angle();


		velocity += direccion * Acceleration * (float)delta;// Acelera en dirección al raton

		// Limitamos la velocidad a la máxima permitida, velocity.Length pilla el modulo del vector y lo compara con MaxSpeed. Si es mayor, lo capa a MaxSpeed
		if (velocity.Length() > MaxSpeed)
		{
			velocity = velocity.Normalized() * MaxSpeed;
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}
