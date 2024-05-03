extends CanvasLayer

@export var projectile: PackedScene
@onready var playercamera: Camera3D = $"../neck/fpscamera"

# Called when the node enters the scene tree for the first time.
func _ready():
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if Input.is_action_just_pressed("left_click"):
		var shot = projectile.instantiate()
		add_child(shot)
		shot.global_position = playercamera.global_position
		shot.direction = -playercamera.global_transform.basis.z
