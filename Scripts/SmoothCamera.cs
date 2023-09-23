using Godot;
using System;

public partial class SmoothCamera : Node3D
{

	[Export]
	private Node3D Target{get; set;}
	[Export]
	private float SmoothSpeed {get; set;} = 0.125f;
	[Export]
	public Vector3 Offset{get; set;}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta){

		Vector3 smoothed = Position.Lerp(Target.Position, (float)delta * SmoothSpeed);
		Position = smoothed;
	}
}
