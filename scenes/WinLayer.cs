using Godot;
using System;

public partial class WinLayer : CanvasLayer
{
	private ColorRect colorRect;
	private Label labelWin;
	private Label label3;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		colorRect = GetNode<ColorRect>("ColorRect");
		labelWin = GetNode<Label>("Label");
		label3 = GetNode<Label>("Label3");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Orb.orbesCompletados)
		{
			colorRect.Visible = true;
			labelWin.Visible = true;
			label3.Visible = true;
			if (Input.IsActionJustPressed("Back"))
		{
			GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
		}
		}
		else
		{
			colorRect.Visible = false;
			labelWin.Visible = false;
			label3.Visible = false;
		}

		

	}
}
