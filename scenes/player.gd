extends CharacterBody3D
@export var speed = 5.0
@export var sprintspeed = 10.0
@export var jump_velocity = 4.5
@export_range(0.001, 0.01) var mouse_sens = 0.01 
@onready var neck := $neck
@onready var fpscamera := $neck/fpscamera
### FPS Camera movement
func _unhandled_input(event):
	if event is InputEventMouseButton:
		Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	elif event.is_action_pressed("escape"):
		Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
	if Input.get_mouse_mode() == Input.MOUSE_MODE_CAPTURED:
		if event is InputEventMouseMotion:
			neck.rotate_y(-event.relative.x * mouse_sens)
			fpscamera.rotate_x(-event.relative.y * mouse_sens)
			fpscamera.rotation.x =clamp(fpscamera.rotation.x, deg_to_rad(-75), deg_to_rad(75))

# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/3d/default_gravity")

func _physics_process(delta):
	# Add the gravity.
	
	if not is_on_floor():
		velocity.y -= gravity * delta

	# Handle jump.
	if Input.is_action_just_pressed("jump") and is_on_floor():
		velocity.y = jump_velocity

	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var input_dir = Input.get_vector("back", "forward", "left", "right")
	var direction = (neck.transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	if direction:
		velocity.x = direction.x * speed
		velocity.z = direction.z * speed
	else:
		velocity.x = move_toward(velocity.x, 0, speed)
		velocity.z = move_toward(velocity.z, 0, speed)
		
	if Input.is_action_pressed("sprint"):
		speed = sprintspeed
	else:
		speed = 5
	move_and_slide()
