using Godot;
using System;


public partial class hellknight : CharacterBody3D
{


	public const float Speed = 7.5f;
	public int health = 20;
	public const float JumpVelocity = 4.5f;

	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	private Vector3 _movementTargetPosition = new Vector3(-3.0f, 0.0f, 2.0f);

	private AnimatedSprite3D _animated_sprite;
	private NavigationAgent3D _navigationAgent;
	public CharacterBody3D player;
	private Vector3 lastPathPosition;


	public Vector3 MovementTarget
	{
		get {return _navigationAgent.TargetPosition;}
		set {_navigationAgent.TargetPosition = value;}
	}


	public void movement(float angle_to_vel){

		if(angle_to_vel < Mathf.Pi / 6 & angle_to_vel > -Mathf.Pi / 6) {
			_animated_sprite.Play("run_forward");

		} else if(angle_to_vel < Mathf.Pi / 3 & angle_to_vel > Mathf.Pi / 6) {

			_animated_sprite.Play("run_forward_right");
			GD.Print(angle_to_vel);
			GD.Print("true fr_right");

		} else if(angle_to_vel > -Mathf.Pi / 3 & angle_to_vel < -Mathf.Pi / 6) {

			_animated_sprite.Play("run_forward_left");
			GD.Print(angle_to_vel);
			GD.Print("true fr_left");

		} else if (angle_to_vel < 2 * Mathf.Pi / 3 & angle_to_vel > Mathf.Pi / 3){

			GD.Print(angle_to_vel);
			_animated_sprite.Play("run_left");
			GD.Print("true left");

		} else if (angle_to_vel > -2 * Mathf.Pi / 3 & angle_to_vel < -Mathf.Pi / 3){
			GD.Print(angle_to_vel);
			_animated_sprite.Play("run_right");
			GD.Print("true right");

		}
	}


	public override void _Ready(){
		player = GetNode<CharacterBody3D>("../Player");

		_navigationAgent = GetNode<NavigationAgent3D>("./NavigationAgent3D");
		_animated_sprite = GetNode<AnimatedSprite3D>("./AnimatedSprite3D");

		_animated_sprite.Play("idle");


		Callable.From(ActorSetup).CallDeferred();
	}


	public override void _PhysicsProcess(double delta)
	{

		LookAt(player.GlobalPosition);
		this.MovementTarget = player.GlobalPosition;
		if (_navigationAgent.IsNavigationFinished()){
			_animated_sprite.Play("idle");
			return;
		}
	
		Vector3 currentAgentPosition = GlobalTransform.Origin;
		Vector3 nextPathPosition = _navigationAgent.GetNextPathPosition();

		Velocity = currentAgentPosition.DirectionTo(nextPathPosition) * Speed;
		float angle_to_velocity = this.GetRealVelocity().SignedAngleTo(Velocity, player.GlobalPosition);

		movement(angle_to_velocity);
		

		MoveAndSlide();
	}


	private async void ActorSetup()
	{
		await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

		MovementTarget = _movementTargetPosition;
		
	}

};
