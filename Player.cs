using Godot;
using System;
using System.Diagnostics;
//TODO Crear la sombra de los objetos
public partial class Player : CharacterBody2D
{
	public const float Speed = 200.0f;
	public const float JumpVelocity = -300.0f;
	public const float Deceleration = 80.0f;

	private Sprite2D spriteDelantero;
	private Sprite2D spriteTrasero;
	private Label labelPorcentaje;
	private int porcentaje;


	public override void _PhysicsProcess(double delta)
	{
		spriteDelantero = GetNode<Sprite2D>("SpriteDelantero");
		spriteTrasero = GetNode<Sprite2D>("SpriteTrasero");
		labelPorcentaje=GetNode<Label>("/root/Game/LabelPorcentaje");
		int.TryParse(labelPorcentaje.Text, out porcentaje);


		Vector2 velocity = Velocity;
		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 direccion = (mousePosition - GlobalPosition).Normalized();
		Rotation = direccion.Angle();

		// spriteTrasero.Position = spriteDelantero.Position - direccion * 10;
		
	
		// Handle Movement.
		if (Input.IsActionPressed("move"))
		{
			velocity = direccion * Speed;
		}
		else
		{
			// Desacelerar suavemente cuando no se mueve
			velocity = velocity.MoveToward(Vector2.Zero, Deceleration * (float)delta);
		}

		if (Input.IsActionJustPressed("shoot"))
		{
			Debug.WriteLine("Pew!");
			porcentaje--;
			labelPorcentaje.Text=porcentaje.ToString();
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		// Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		// if (direction != Vector2.Zero)
		// {
		// 	velocity.X = direction.X * Speed;
		// }
		// else
		// {
		// 	velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		// }

		Velocity = velocity;
		MoveAndSlide();
	}
}
