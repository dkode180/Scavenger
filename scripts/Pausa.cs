using Godot;
using System;
using System.Diagnostics;

public partial class Pausa : CanvasLayer
{
	private ColorRect colorRect;
	private Label labelPausa;
	private Label labelMenu;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		colorRect = GetNode<ColorRect>("ColorRect");
		labelPausa = GetNode<Label>("Label");
		labelMenu = GetNode<Label>("Label2");

		ProcessMode = ProcessModeEnum.Always; // Esto mantiene vivo el nodo aunque el juego esté pausado
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pausa"))
		{
			Debug.WriteLine("A");
			GetTree().Paused = !GetTree().Paused;
			colorRect.Visible = !colorRect.Visible;
			labelPausa.Visible = !labelPausa.Visible;
			labelMenu.Visible = !labelMenu.Visible;



		}
		// Esto debe estar fuera del if de pausa
		if (GetTree().Paused && Input.IsActionJustPressed("Back"))
		{
			GetTree().Paused = false; // Despausar antes de cambiar escena
			GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
		}

	}
}
