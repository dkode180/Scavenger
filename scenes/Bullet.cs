using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export]
	public float Speed = 800.0f;

	private Vector2 _direction;

	public void SetDirection(Vector2 dir)
	{
		_direction = dir.Normalized();
		Rotation = _direction.Angle();
	}

	public override void _Ready()
	{
		// Conectar la señal body_entered de manera C#-idiomática en Godot 4
		BodyEntered += _on_body_entered;
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += _direction * Speed * (float)delta;

		if (Position.Length() > 500)
		{
			QueueFree();
		}
	}

	private void _on_body_entered(Node body)
	{
		if (body is StaticBody2D || body is Enemy)
		{
			if(body is Enemy){
				((Enemy)body).QueueFree();
			}
			GD.Print("Colisión con StaticBody2D o enemigo");
			QueueFree();
		}
	}
}
