using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	public const float Speed = 80.0f;
	public const float Deceleration = 80.0f;
	public const float MaxSpeed = 100.0f;
	public const float Acceleration = 400.0f;
	public const float JumpVelocity = -400.0f;
	private CharacterBody2D player;

	public override void _PhysicsProcess(double delta)
	{
		 player = GetTree().Root.FindChild("Player", true, false) as CharacterBody2D;
		Vector2 velocity = Velocity;

		Vector2 playerPosition = player.GlobalPosition;
		Vector2 direccion = (playerPosition - GlobalPosition).Normalized();
		Rotation = direccion.Angle();

			// Aceleramos en dirección al mouse
			velocity += direccion * Acceleration * (float)delta;

			// Limitamos la velocidad a la máxima permitida, velocity.Length pilla el modulo del vector y lo compara con MaxSpeed. Si es mayor, lo capa a MaxSpeed
			if (velocity.Length() > MaxSpeed)
			{
				velocity = velocity.Normalized() * MaxSpeed;
			}
		Velocity = velocity;
		MoveAndSlide();
	}
}
