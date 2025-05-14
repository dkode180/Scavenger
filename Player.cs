using Godot;
using System;
using System.Diagnostics;
//TODO Crear la sombra de los objetos
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
	private int porcentaje;
	private Random r;



	public override void _PhysicsProcess(double delta)
	{
		r = new Random();
		spriteDelantero = GetNode<Sprite2D>("SpriteDelantero");
		spriteTrasero = GetNode<Sprite2D>("SpriteTrasero");
		labelPorcentaje = GetNode<Label>("/root/Game/LabelPorcentaje");
		int.TryParse(labelPorcentaje.Text, out porcentaje);


		Vector2 velocity = Velocity;
		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 direccion = (mousePosition - GlobalPosition).Normalized();
		Rotation = direccion.Angle();

		// spriteTrasero.Position = spriteDelantero.Position - direccion * 10;


		// Handle Movement.
		if (Input.IsActionPressed("move"))
		{
			// Aceleramos en dirección al mouse
			velocity += direccion * Acceleration * (float)delta;

			// Limitamos la velocidad a la máxima permitida, velocity.Length pilla el modulo del vector y lo compara con MaxSpeed. Si es mayor, lo capa a MaxSpeed
			if (velocity.Length() > MaxSpeed)
			{
				velocity = velocity.Normalized() * MaxSpeed;
			}
		}
		else
		{
			// Desacelerar cuando no se mueve
			velocity = velocity.MoveToward(Vector2.Zero, Deceleration * (float)delta);
		}

		if (Input.IsActionJustPressed("shoot"))
		{
			int n;
			Debug.WriteLine("Porcentaje: " + porcentaje);
			// if (r.Next(1, 100) == porcentaje)
			n = r.Next(1, 100);
			if (n > porcentaje)
			{
				foreach (Node node in GetTree().GetNodesInGroup("enemies"))
				{
					node.QueueFree();
				}
				GetTree().ReloadCurrentScene(); // Reinicia la partida
				
			}
			else
			{
				porcentaje--;
				labelPorcentaje.Text = porcentaje.ToString();

				// Crear la bala
				Bullet bullet = BulletScene.Instantiate<Bullet>();
				bullet.GlobalPosition = GlobalPosition;
				bullet.SetDirection((GetGlobalMousePosition() - GlobalPosition).Normalized());

				// Agregarla al árbol de nodos
				GetTree().CurrentScene.AddChild(bullet);
			}
			Debug.WriteLine("Numero aleatorio: " + n);


		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
