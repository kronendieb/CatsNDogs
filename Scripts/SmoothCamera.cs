using Godot;
using System;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;

public partial class SmoothCamera : Node3D
{

	[Export]
	private Node3D Target{get; set;}
	[Export]
	private Node3D camera {get;set;}
	private Vector3 direction {get;set;}
	private float distance {get;set;}
	[Export]
	private float maxDistance {get;set;} = 5.0f;
	[Export]
	private float minDistance {get;set;} = 1.0f;
	[Export]
	private float SmoothSpeed {get; set;} = 0.125f;
	[Export]
	public Vector3 Offset{get; set;}

	public PhysicsDirectSpaceState3D physics {get;set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		direction = camera.Position.Normalized();
		distance = maxDistance;
		
		physics = GetWorld3D().DirectSpaceState;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta){
		camera.LookAt(Position);
		Vector3 smoothed = Position.Lerp(Target.Position, (float)delta * SmoothSpeed);
		Position = smoothed;

		var query = PhysicsRayQueryParameters3D.Create(GlobalPosition, GlobalPosition + (direction * maxDistance));
		CollisionObject3D getRid = (CollisionObject3D)Target;
		query.Exclude = new Godot.Collections.Array<Rid>{getRid.GetRid()};
		var result = physics.IntersectRay(query);
		if(result != null){
			if(result.ContainsKey("position")){
				var hit = (Vector3)result["position"];
				distance = Mathf.Clamp((hit - GlobalPosition).Length() * 0.6f, minDistance, maxDistance);
			}else{
				distance = maxDistance;
			}
		}
		Vector3 desiredPosition = direction * distance;
		camera.Position = camera.Position.Lerp(desiredPosition, (float)delta * SmoothSpeed);

	}
}
