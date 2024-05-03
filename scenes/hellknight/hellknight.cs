using Godot;
using System;

public partial class hellknight : CharacterBody3D
{


	public const float Speed = 1.0f;
	public int health = 20;
	public const float JumpVelocity = 4.5f;
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	private AnimatedSprite3D _animated_sprite;


	public override void _Ready(){
		_animated_sprite = GetNode<AnimatedSprite3D>("./AnimatedSprite3D");
		_animated_sprite.Play("idle");
	}
	public override void _PhysicsProcess(double delta)
	{

		MoveAndSlide();
	}
};
