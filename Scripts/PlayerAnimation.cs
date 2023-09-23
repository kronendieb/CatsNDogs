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
		if(Input.IsActionPressed("Forwards")){
			if(Input.IsActionPressed("Right")){
				Play("NorthEast");
			}else if(Input.IsActionPressed("Left")){
				Play("NorthWest");
			}else{
				Play("North");
			}
		}
		else if(Input.IsActionPressed("Backwards")){
			if(Input.IsActionPressed("Left")){
				Play("SouthWest");
			}else{
				Play("SouthEast");
			}
		}
		else if (Input.IsActionPressed("Right")){
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
