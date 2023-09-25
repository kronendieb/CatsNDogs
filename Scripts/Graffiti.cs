using Godot;
using System;
using System.Dynamic;
using Godot.Collections;

public partial class Graffiti : Node3D
{

	private bool isInside {get;set;} = false;
	private Area3D area {get; set;}
	[Export]
	public Node3D player {get; set;}
	[Export]
	private Label prompt {get; set;}
	[Export]
	private Array<Texture2D> textures {get;set;}
	private bool isActive {get; set;} = true;
	private Sprite3D sprite {get;set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		prompt.Visible = false;
		area = GetChild<Area3D>(0);
		sprite = area.GetChild<Sprite3D>(1);
		sprite.Texture = textures[0];
		area.BodyEntered += (node) => Entered(node);
		area.BodyExited += (node) => Exited(node);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(isActive && isInside && Input.IsActionJustPressed("UseAction")){
			isActive = false;
			sprite.Texture = textures[1];
			EmitSignal("SignalGraffiti");
		}
	}

	public void Entered(Node3D node){
		if (node.Name == player.Name && isActive){
			prompt.Visible = true;
			isInside = true;
		}
	}

	public void Exited(Node3D node){
		if(node.Name == player.Name){
			prompt.Visible = false;
			isInside = false;
		}
	}

	[Signal]
	public delegate void SignalGraffitiEventHandler();
}
