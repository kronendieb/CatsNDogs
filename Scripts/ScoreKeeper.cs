using Godot;
using System;
using System.Collections.Generic;

public partial class ScoreKeeper : Node3D
{
	[Export]
	private float score = 0.0f;
	[Export]
	private float time = 300.0f;
	[Export]
	private Timer timer;
	[Export]
	private Label timerLabel;
	[Export]
	private Label scoreLabel;
	private List<Node3D> Graffities = new List<Node3D>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer.WaitTime = time;
		timer.Start();
		timer.Timeout += () => OnTimerTimeout();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timerLabel.Text = "Timer: " + (int)timer.TimeLeft;
		scoreLabel.Text = "Score: " + score;
	}

	public void OnTimerTimeout(){
		GD.Print("Timer");
		timer.Stop();
	}
}
