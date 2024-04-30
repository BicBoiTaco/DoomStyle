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



func _ready():
	_animated_sprite.play("idle")
	
func _physics_process(delta):
	# Add the gravity.
	faceplayer()
	move_and_slide()



