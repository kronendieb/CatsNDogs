using Godot;
using System;

public partial class Cop : CharacterBody3D
{
	[Export]
	public float Speed {get;set;} = 5.0f;
	[Export]
	public float AgroRange {get;set;} = 5.0f;
	public const float JumpVelocity = 4.5f;
	[Export]
	public Node3D Target {get; set;}

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		LookAt(Target.Position);
		Vector3 direction = -GlobalTransform.Basis.Z;
		if (direction != Vector3.Zero && Position.DistanceSquaredTo(Target.Position) < AgroRange * AgroRange)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
