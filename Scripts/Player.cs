using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export]
	private float Speed {get; set;}= 5.0f;
	[Export]
	private float Acceleration {get;set;} = 4.0f;
	[Export]
	private float Deceleration {get; set;} = 5.0f;
	[Export]
	public float JumpVelocity {get;set;} = 4.5f;
	[Export]
	public float maxJumps {get;set;} = 2.0f;
	private float currentJumps {get;set;}

	private Vector2 inputDir = Vector2.Zero;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	private bool landed = false;
    public override void _Ready(){
		currentJumps = maxJumps;
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;
		if(Input.IsActionJustPressed("Quit")){
			GetTree().Quit();
		}

		// Add the gravity.
		if (!IsOnFloor()){
			velocity.Y -= gravity * (float)delta;
			landed = false;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("Jump") && currentJumps > 0){
			currentJumps -= 1;
			velocity.Y = JumpVelocity;
			EmitSignal("SignalJump");
		}

		if(!landed && IsOnFloor()){
			currentJumps = maxJumps;
			EmitSignal("SignalLanded");
			landed = true;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		inputDir = Input.GetVector("Left", "Right", "Forwards", "Backwards").Normalized();
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction.LengthSquared() > 1){
			direction /= direction.Length();
		}

		Vector3 xVelocity = velocity;
		xVelocity.Y = 0;

		Vector3 TargetSpeed = direction * Speed;
		float CurrentAcceleration;
		if(direction.Dot(xVelocity) > 0){
			CurrentAcceleration = Acceleration;
		}
		else{
			CurrentAcceleration = Deceleration;
		}
		xVelocity = xVelocity.Lerp(TargetSpeed, CurrentAcceleration * (float)delta);

		velocity.X = xVelocity.X;
		velocity.Z = xVelocity.Z;

		Velocity = velocity;
		MoveAndSlide();
	}

	[Signal]
	public delegate void SignalJumpEventHandler();
	[Signal]
	public delegate void SignalLandedEventHandler();


}
