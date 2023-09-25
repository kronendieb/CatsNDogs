using Godot;
using System;


public partial class Splash : Node2D
{
	[Export]
	private AnimatedSprite2D sprite {get;set;}
	[Export]
	private AudioStreamPlayer2D music {get;set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Enabled);
		sprite.Play("default");
		music.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionPressed("Jump")){
			GetTree().ChangeSceneToFile("res://Scenes/Level1.tscn");
		}
	}
}
