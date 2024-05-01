using Godot;
using System;

public partial class hellknight : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
		// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	public override void _Ready(){
		AnimatedSprite3D _animated_sprite = GetNode<AnimatedSprite3D>("./AnimatedSprite3D");
		_animated_sprite.Play("idle");
	}
	public override void _PhysicsProcess(double delta)
	{
		AnimatedSprite3D _animated_sprite = GetNode<AnimatedSprite3D>("./AnimatedSprite3D");
		Node player = GetNode("../Player");
		Vector3 playerpos = (Vector3)player.Get("global_position");
		LookAt(playerpos);
		if(!this.IsOnFloor()){
			this.Velocity.Y.Equals(Velocity.Y - gravity * delta);
		}
		if(this.GlobalPosition.DistanceTo(playerpos) > 5.0){
			_animated_sprite.Play("run_forward");
			Velocity = this.GlobalPosition.DirectionTo(playerpos) * 8;
		} else{
			_animated_sprite.Play("idle");
		}

		MoveAndSlide();
	}
};
