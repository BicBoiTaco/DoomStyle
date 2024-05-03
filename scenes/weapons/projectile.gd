extends Area3D


var direction := Vector3.FORWARD
var damage = 5
@export_range(0.5, 100.0) var speed = 30

func _physics_process(delta):
	position += direction * speed * delta

# Bullet hits body such as CharacterBody3D
func _on_body_entered(body):
	# If is in group "enemy"
	if body.is_in_group("enemy"):
		enemy_hit(body)

# Bullet despawns after 5 seconds
func _on_timer_timeout():
	self.queue_free()

# Bullet hits Area3D node
func _on_area_entered(area):
	# If is in group "enemy"
	if area.is_in_group("enemy"):
		enemy_hit(area)
	
func enemy_hit(enemybody):
	# Refers to "health" variable on target entity and subtracts projectile 
	# damage variable
	enemybody.health -= damage
	# Prints target health value
	print(enemybody.health)
	# Prints prompt letting you know someone was hit
	print("I HIT SOMEONE!")
	# Bullet is removed
	self.queue_free()
	# If enemy health is zero or below
	if enemybody.health <= 0:
		# Delete enemy
		enemybody.queue_free()
	
