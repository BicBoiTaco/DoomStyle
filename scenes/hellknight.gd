extends CharacterBody3D
const SPEED = 5.0
const JUMP_VELOCITY = 4.5
# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/3d/default_gravity")
@onready var player := get_node("../Player")
@onready var _animated_sprite = $AnimatedSprite3D


func faceplayer():
	var playerpos = player.playerpos
	look_at(playerpos)

func runforward():
	pass
	
func _ready():
	_animated_sprite.play("idle")
	
func _physics_process(_delta):
	# Add the gravity.
	faceplayer()
	if global_position.distance_to(player.playerpos) > 5.0:
		_animated_sprite.play("run_forward")
		velocity = global_position.direction_to(player.playerpos) * 7
	else:
		_animated_sprite.play("idle")
		
	move_and_slide()




