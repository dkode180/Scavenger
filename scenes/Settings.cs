using Godot;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

public partial class Settings : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var checkMute = GetNode<CheckBox>("/root/settings/MarginContainer/VBoxContainer/Mute");
		var volumen = GetNode<HSlider>("/root/settings/MarginContainer/VBoxContainer/Volume");
		var selectResolutions = GetNode<OptionButton>("/root/settings/MarginContainer/VBoxContainer/Resolutions");
		var backButton = GetNode<Button>("/root/settings/MarginContainer/VBoxContainer/Button");
		volumen.ValueChanged += volumenCambiado;
		checkMute.Toggled += muteToggled;
		selectResolutions.ItemSelected += itemSelected;
		backButton.Pressed += backPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void backPressed()
	{
		Debug.WriteLine("ENTRA");
		GetTree().ChangeSceneToFile("res://Scenes/main_menu.tscn");
	}
	private void volumenCambiado(double value)
	{
		AudioServer.SetBusVolumeDb(0, (float)value);
	}
	private void muteToggled(bool buttonPressed)
	{
		AudioServer.SetBusMute(0, buttonPressed);
	}
	private void itemSelected(long index)
	{
		Debug.WriteLine("Entra");
		switch (index)
		{
			case 0:
				DisplayServer.WindowSetSize(new Vector2I(1920, 1080));
				break;
			case 1:
				DisplayServer.WindowSetSize(new Vector2I(1600, 900));
				break;
			case 2:
				DisplayServer.WindowSetSize(new Vector2I(1280, 720));
				break;

		}
	}
}
