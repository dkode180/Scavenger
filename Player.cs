using Godot;
using System;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
	[Export]
	public PackedScene BulletScene;

	public const float Speed = 280.0f;
	public const float Deceleration = 80.0f;
	public const float MaxSpeed = 200.0f;
	public const float Acceleration = 400.0f;

	private Sprite2D spriteDelantero;
	private Sprite2D spriteTrasero;
	private Label labelPorcentaje;
	private AudioStreamPlayer2D sonidoDisparo;
	private Label labelOrbes;
	public static int porcentaje = 100;
	private Random r = new();

	public override void _Ready()
	{
		sonidoDisparo = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer");
		spriteDelantero = GetNode<Sprite2D>("SpriteDelantero");
		spriteTrasero = GetNode<Sprite2D>("SpriteTrasero");
		labelPorcentaje = GetNode<Label>("/root/Game/LabelPorcentaje");
		labelOrbes = GetNode<Label>("/root/Game/LabelOrbes");
		ActualizarPorcentajeLabel();
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
		labelPorcentaje.Text = porcentaje.ToString();
		labelOrbes.Text = Orb.orbesRecogidos.ToString();
		// Debug.WriteLine("POSICION: "+this.Position);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 direccion = (mousePosition - GlobalPosition).Normalized();
		Rotation = direccion.Angle();

		if (Input.IsActionPressed("move"))
		{
			velocity += direccion * Acceleration * (float)delta;
			if (velocity.Length() > MaxSpeed)
				velocity = velocity.Normalized() * MaxSpeed;
		}
		else
		{
			velocity = velocity.MoveToward(Vector2.Zero, Deceleration * (float)delta);
		}

		if (Input.IsActionJustPressed("shoot"))
		{
			// sonidoDisparo.Play(); NO VA?
			Debug.WriteLine("Transparencia: " + SpawnComponent.transparency);
			Debug.WriteLine("Porcentaje: " + porcentaje);
			int n = r.Next(1, 100);
			SpawnComponent.transparency += 0.1f;
			if (n > porcentaje)
			{
				foreach (Node node in GetTree().GetNodesInGroup("enemies"))
				{
					node.QueueFree();
				}
				foreach (Node node in GetTree().GetNodesInGroup("orbs"))
				{
					node.QueueFree();
				}

				GetTree().ReloadCurrentScene();
				porcentaje = 100;
				Orb.flag = true;
				Orb.orbesRecogidos = 0;
				SpawnComponent.transparency = 0.5f;
			}
			else
			{
				porcentaje = Math.Max(0, porcentaje - 1);
				ActualizarPorcentajeLabel();

				Bullet bullet = BulletScene.Instantiate<Bullet>();
				bullet.GlobalPosition = GlobalPosition;
				bullet.SetDirection((GetGlobalMousePosition() - GlobalPosition).Normalized());

				GetTree().CurrentScene.AddChild(bullet);
			}

			// Debug.WriteLine("Numero aleatorio: " + n);

		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void ActualizarPorcentajeLabel()
	{
		if (labelPorcentaje != null)
		{
			labelPorcentaje.Text = porcentaje.ToString();
		}



	}
}
