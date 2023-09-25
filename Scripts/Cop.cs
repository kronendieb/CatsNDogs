using Godot;
using System;

public partial class Cop : CharacterBody3D
{
	[Export]
	public Node3D world {get;set;}
	[Export]
	public float Speed {get;set;} = 5.0f;
	[Export]
	public float AgroRange {get;set;} = 6.0f;
	[Export]
	public float ShootingRange {get;set;} = 4.0f;
	public const float JumpVelocity = 4.5f;
	[Export]
	public Node3D Target {get; set;}

	private AnimatedSprite3D sprite {get;set;}

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready()
    {
		world = GetOwner<Node3D>();
		sprite = GetChild<AnimatedSprite3D>(1);
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		LookAt(Target.Position);

		float distance = (GlobalPosition - Target.GlobalPosition).LengthSquared();
		if(distance < AgroRange * AgroRange){
			if(distance < ShootingRange * ShootingRange){
				if(distance < 2){
					EmitSignal("Arrest");
				}
				sprite.Play("Shooting");
			}
			else{
				sprite.Play("Alert");
			}
		}else{
			sprite.Play("Idle");
		}

		Vector3 direction = -GlobalTransform.Basis.Z;
		if (direction != Vector3.Zero && distance < AgroRange * AgroRange)
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

	[Signal]
	public delegate void ArrestEventHandler();

/*
	public void Shoot(){
		GD.Print("Hello");
		Node3D instance = (Node3D)bulletPrefab.Instantiate();
		instance.Position = Position;
		instance.LookAtFromPosition(instance.Position, Target.Position);
		world.AddChild(instance);
		canShoot = false;
	}

	public void OnTimerTimeout(){
		GD.Print("Goodbye");
		canShoot = true;
	}
	*/
}
