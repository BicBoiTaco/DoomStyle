extends Area3D


var direction := Vector3.FORWARD
var damage = 5
@export_range(0.5, 100.0) var speed = 30
@onready var explosion = get_node("./ShapeCast3D")

# Bullet flies
func _physics_process(delta):
	position += direction * speed * delta
	
	


# Bullet explodes after 5 seconds
func _on_timer_timeout():
	pass

	

	





