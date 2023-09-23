using Godot;
using System;

public partial class Graffiti : Node3D
{

	[Export]
	private Area3D area {get; set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		area = GetChild<Area3D>(0);
		area.BodyEntered += (node) => Entered(node);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Entered(Node3D node){
		GD.PrintErr(node.Name);
	}
}
