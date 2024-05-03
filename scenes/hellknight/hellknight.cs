using Godot;
using System;

public partial class hellknight : CharacterBody3D
{


	public const float Speed = 1.0f;
	public int health = 20;
	public const float JumpVelocity = 4.5f;

	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	private Vector3 _movementTargetPosition = new Vector3(-3.0f, 0.0f, 2.0f);

	private AnimatedSprite3D _animated_sprite;
	private NavigationAgent3D _navigationAgent;
	public CharacterBody3D player;


	public Vector3 MovementTarget
	{
		get {return _navigationAgent.TargetPosition;}
		set {_navigationAgent.TargetPosition = value;}
	}


	public override void _Ready(){
		
		_navigationAgent = GetNode<NavigationAgent3D>("./NavigationAgent3D");
		_animated_sprite = GetNode<AnimatedSprite3D>("./AnimatedSprite3D");

		_animated_sprite.Play("idle");

		_navigationAgent.PathDesiredDistance = 0.5f;
		_navigationAgent.TargetDesiredDistance = 0.5f;

		Callable.From(ActorSetup).CallDeferred();
	}


	public override void _PhysicsProcess(double delta)
	{
		if (_navigationAgent.IsNavigationFinished()){
			return;
		}
		Vector3 currentAgentPosition = GlobalTransform.Origin;
		Vector3 nextPathPosition = _navigationAgent.GetNextPathPosition();

		Velocity = currentAgentPosition.DirectionTo(nextPathPosition) * Speed;

		MoveAndSlide();
	}


	private async void ActorSetup()
	{
		await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

		MovementTarget = _movementTargetPosition;
		
	}

};
