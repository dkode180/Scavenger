using Godot;
using System;
using System.Diagnostics;

public partial class Settings : Control
{
	private HSlider volumen;
	private CheckBox checkMute;
	private OptionButton selectResolutions;
	private Button backButton;

	public override void _Ready()
	{
		// Rutas relativas (más robustas que usar /root)
		checkMute = GetNode<CheckBox>("MarginContainer/VBoxContainer/Mute");
		volumen = GetNode<HSlider>("MarginContainer/VBoxContainer/Volume");
		selectResolutions = GetNode<OptionButton>("MarginContainer/VBoxContainer/Resolutions");
		backButton = GetNode<Button>("MarginContainer/VBoxContainer/Button");

		// Conectar señales
		volumen.ValueChanged += volumenCambiado;
		checkMute.Toggled += muteToggled;
		selectResolutions.ItemSelected += itemSelected;
		backButton.Pressed += backPressed;

		// Inicializar volumen desde AudioServer (decibelios a 0–100)
		float currentDb = AudioServer.GetBusVolumeDb(0);
		float value = Mathf.InverseLerp(-80f, 0f, currentDb) * 100f;
		volumen.Value = value;
	}

	private void volumenCambiado(double value)
	{
		// Convertir de 0–100 (slider) a -80–0 dB (volumen real)
		float volumeDb = Mathf.Lerp(-80f, 0f, (float)value / 100f);
		AudioServer.SetBusVolumeDb(0, volumeDb);
	}

	private void muteToggled(bool buttonPressed)
	{
		AudioServer.SetBusMute(0, buttonPressed);
	}

	private void itemSelected(long index)
	{
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

	private void backPressed()
	{
	
		GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
	}
}
