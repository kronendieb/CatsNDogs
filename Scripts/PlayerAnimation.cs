using Godot;
using System;

public partial class PlayerAnimation : AnimatedSprite3D
{
	[Export]
	private Player parent {get;set;}
	private bool jumping {get;set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		jumping = false;
		parent.SignalJump += () => PlayJumpped();
		parent.SignalLanded += () => PlayLanded();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if(!jumping){
			FlipH = false;
			if(Input.IsActionPressed("Forwards")){
				if(Input.IsActionPressed("Right")){
					Play("NorthEast");
				}else if(Input.IsActionPressed("Left")){
					FlipH = true;
					Play("NorthEast");
				}else{
					Play("North");
				}
			}
			else if(Input.IsActionPressed("Backwards")){
				if(Input.IsActionPressed("Left")){
					FlipH = true;
					Play("SouthEast");
				}else if (Input.IsActionPressed("Right")){
					Play("SouthEast");
				}else{
					Play("South");
				}
			}
			else if (Input.IsActionPressed("Right")){
					Play("Right");
			}
			else if (Input.IsActionPressed("Left")){
				 	FlipH = true;
					Play("Right");
			}
			else{
				Play("Standing");
			}
		}else if(!IsPlaying()){
			if(Input.IsActionPressed("Left")){
				FlipH = true;
				Play("Jump");
			}else{
				FlipH = false;
				Play("Jump");
			}
		}
	}

	public void PlayJumpped(){
		if(Input.IsActionPressed("Left")){
			FlipH = true;
			Play("StartJump");
		}else{
			FlipH = false;
			Play("StartJump");
		}
		jumping = true;
	}

	public void PlayLanded(){
		if(Input.IsActionPressed("Left")){
			FlipH = true;
			Play("EndJump");
		}else{
			FlipH = false;
			Play("EndJump");
		}
		jumping = false;
	}
}
