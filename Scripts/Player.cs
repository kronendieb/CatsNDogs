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
	public const float JumpVelocity = 4.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;
		if(Input.IsActionJustPressed("Quit")){
			GetTree().Quit();
		}

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("Left", "Right", "Forwards", "Backwards");
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
}
