using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ScoreKeeper : Node3D
{
	[Export]
	private float score = 0.0f;
	[Export]
	private float scoreMultiplier = 10.0f;
	[Export]
	private float time = 300.0f;
	[Export]
	private Timer timer;
	[Export]
	private Label timerLabel;
	[Export]
	private Label scoreLabel;
	[Export]
	private Label gameOverLabel;
	[Export]
	private Label tagsLabel;
	[Export]
	private Node3D player;
	[Export]
	private Godot.Collections.Array<Node3D> graffities;
	[Export]
	private Godot.Collections.Array<Node3D> cops;
	private float numGraffiti {get; set;}
	[Export]
	private AudioStreamPlayer musicPlayer {get;set;}
	[Export]
	private AudioStreamPlayer graffitiPlayer {get;set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Enabled);
		timer.WaitTime = time;
		timer.Start();
		timer.Timeout += () => OnTimerTimeout();
		numGraffiti = graffities.Count();
		gameOverLabel.Text = "";
		tagsLabel.Text = "Tags left: " + graffities.Count;
		musicPlayer.Play();
		
		foreach (Graffiti G in graffities){
			G.player = player;
			G.SignalGraffiti += () => AddScore(G);
		}

		foreach (Cop C in cops){
			C.Arrest += () => GameOver();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timerLabel.Text = "Timer: " + (int)timer.TimeLeft;
		scoreLabel.Text = "Score: " + score;
		tagsLabel.Text = "Tags left: " + numGraffiti;
		if(numGraffiti <= 0){
			gameOverLabel.Text = "Game Over!";
		}
	}

	public void AddScore(Node3D node){
		score += (int)scoreMultiplier * (int)timer.TimeLeft;
		graffitiPlayer.Play();
		numGraffiti -= 1;
	}

	public void GameOver(){
		var globalScore = GetNode<Score>("/root/Score");
		globalScore.SetScore(score);
		GetTree().ChangeSceneToFile("res://Scenes/GameOver.tscn");
	}

	public void OnTimerTimeout(){
		timer.Stop();
	}
}
