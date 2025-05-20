using Godot;
using System;

public partial class Game : Node2D
{


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Orb.orbesCompletados = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Orb.orbesCompletados  && Input.IsActionJustPressed("Back"))
		{
			Orb.flag = true;
			GetTree().Paused = false;
			GetTree().ChangeSceneToFile("res://Scenes/main_menu.tscn");
			Player.porcentaje = 100;
			Orb.orbesRecogidos = 0;
			
		}
	}
}
