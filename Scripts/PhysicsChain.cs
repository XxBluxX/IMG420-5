using Godot;
using System.Collections.Generic;

public partial class PhysicsChain : Node2D
{
	[Export] public int ChainSegments = 5;
	[Export] public float SegmentDistance = 30f;
	[Export] public PackedScene SegmentScene;
	
	private List<RigidBody2D> _segments = new List<RigidBody2D>();
	private List<Joint2D> _joints = new List<Joint2D>();
	
	public override void _Ready()
	{
		CreateChain();
	}
	
	private void CreateChain()
	{
		Vector2 startPos = GlobalPosition;
		
		// TODO: Create chain segments
		var staticAnchor = new StaticBody2D();
		AddChild(staticAnchor);
		staticAnchor.GlobalPosition = startPos;
		
		for (int i=0; i<ChainSegments; i++)
		{
			var segment = (RigidBody2D)SegmentScene.Instantiate();
			AddChild(segment);
			
			// TODO: Position them appropriately
			segment.GlobalPosition = startPos + new Vector2(0, i * SegmentDistance);
			_segments.Add(segment);
			
			// TODO: Connect them with joints
			// TODO: Configure joint properties (softness, bias, damping)
			// Hints:
			// - First segment should be StaticBody2D or pinned
			// - Use PinJoint2D.NodeA and NodeB to connect segments
			// - Set collision layers appropriately
			if (i == 0)
			{
				var joint = new PinJoint2D
				{
					Softness = 0.3f
				};
				AddChild(joint);
				joint.NodeA = joint.GetPathTo(staticAnchor);
				joint.NodeB = joint.GetPathTo(segment);
				joint.GlobalPosition = startPos;
				_joints.Add(joint);
			} else {
				var joint = new PinJoint2D
				{
					Softness = 0.3f
				};
				AddChild(joint);
				var prev = _segments[i-1];
				joint.NodeA = joint.GetPathTo(prev);
				joint.NodeB = joint.GetPathTo(segment);
				joint.GlobalPosition = (prev.GlobalPosition + segment.GlobalPosition) / 2.0f;
				_joints.Add(joint);
			}
		}
	}
	
	// TODO: Add method to apply force to chain
	public void ApplyForceToSegment(int segmentIndex, Vector2 force)
	{
		// Apply impulse or force to specific segment
		if (segmentIndex < 0 || segmentIndex >= _segments.Count)
		{
			return;
		}
		_segments[segmentIndex].ApplyCentralImpulse(force);
	}
}
