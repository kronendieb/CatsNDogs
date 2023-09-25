using Godot;
using System;

public partial class Score : Node
{
	public float score {get;set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetScore(float s){
		score = s;
	}

	public float GetScore(){
		return score;
	}
}
