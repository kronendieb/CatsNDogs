using Godot;
using System;

public partial class PlayerAnimation : AnimatedSprite3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("Right")){
			Play("Right");
		}
		else if (Input.IsActionPressed("Left")){
			Play("Left");
		}
		else{
			Play("Standing");
		}
	}
}
