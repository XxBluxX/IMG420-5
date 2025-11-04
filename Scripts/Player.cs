using Godot;
using Godot.Collections;

public partial class Player : CharacterBody2D
{
	[Export] public float Speed = 200f;
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Vector2.Zero;
		// TODO: Implement basic movement (WASD or Arrow keys)
		if (Input.IsActionPressed("Right")) velocity.X += 1;
		if (Input.IsActionPressed("Left")) velocity.X -= 1;
		if (Input.IsActionPressed("Down")) velocity.Y += 1;
		if (Input.IsActionPressed("Up")) velocity.Y -= 1;
		// TODO: Use MoveAndSlide()
		velocity = velocity.Normalized() * Speed;
		// Get input and move
		Velocity = velocity;
		MoveAndSlide();
		
		PushNearbyRigidBodies();
	}
	
	private void PushNearbyRigidBodies()
	{
		PhysicsShapeQueryParameters2D query = new PhysicsShapeQueryParameters2D
		{
			Shape = GetNode<CollisionShape2D>("CollisionShape2D").Shape,
			Transform = GlobalTransform,
			CollideWithBodies = true
		};
		
		Array<Dictionary> results = GetWorld2D().DirectSpaceState.IntersectShape(query);
		
		foreach (Dictionary result in results)
		{
			if (result.TryGetValue("collider", out Variant colliderVariant))
			{
				RigidBody2D rb = colliderVariant.As<RigidBody2D>();
				if (rb != null)
				{
					rb.ApplyCentralImpulse(Velocity * 5f);
				}
			}
		}
	}
}
