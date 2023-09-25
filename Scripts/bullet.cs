using Godot;
using System;

public partial class bullet : AnimatableBody3D
{
	[Export]
	private Node3D Target {get;set;}
	[Export]
	private float speed {get;set;}
	[Export]
	private Timer alive {get;set;}
	[Export]
	private float timeAlive {get;set;}
	private Vector3 velocity {get;set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		alive.WaitTime = timeAlive;
		alive.Start();
		alive.Timeout += () => OnTimerTimeout();
		velocity =  Transform.Basis.X * speed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		MoveAndCollide((float)delta * velocity);
	}

	public void OnTimerTimeout(){
		QueueFree();
	}
}
