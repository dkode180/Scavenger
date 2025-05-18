using Godot;
using System;
using System.Diagnostics;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var jugarButton = GetNode<Button>("/root/main_menu/VBoxContainer/Jugar");
		var opcionesButton = GetNode<Button>("/root/main_menu/VBoxContainer/Opciones");
		var salirButton = GetNode<Button>("/root/main_menu/VBoxContainer/Salir");
		// var vbox = GetNode<VBoxContainer>("/root/main_menu/VBoxContainer");
		Debug.WriteLine("Botón Jugar encontrado: ", jugarButton != null);
		GD.Print("MainMenu Ready");

		// foreach (Node child in vbox.GetChildren())
		// {
		// 	GD.Print("Nodo hijo del VBox: ", child.Name);
		// }
		jugarButton.Pressed += OnJugarPressed;
		opcionesButton.Pressed += OnOpcionesPressed;
		salirButton.Pressed += OnSalirPressed;


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void OnJugarPressed()
	{

		Debug.WriteLine("Jugar presionado");
		GetTree().ChangeSceneToFile("res://Scenes/game.tscn");
	}
	private void OnOpcionesPressed()
	{
		Debug.WriteLine("Jugar presionado");
		GetTree().ChangeSceneToFile("res://Scenes/settings.tscn");

	}
	private void OnSalirPressed()
	{
		Debug.WriteLine("Jugar presionado");
		GetTree().Quit();
	}
}
