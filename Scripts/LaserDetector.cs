using Godot;

public partial class LaserDetector : Node2D
{
	[Export] public float LaserLength = 500f;
	[Export] public Color LaserColorNormal = Colors.Green;
	[Export] public Color LaserColorAlert = Colors.Red;
	[Export] public NodePath PlayerPath;
	[Export] public NodePath ParticleSystemPath;
	[Export] public NodePath RedTintPath;
	
	private RayCast2D _rayCast;
	private Line2D _laserBeam;
	private Node2D _player;
	private bool _isAlarmActive = false;
	private Timer _alarmTimer;
	private GpuParticles2D _particleSystem;
	private ColorRect _redTint;
	
	public override void _Ready()
	{
		SetupRaycast();
		SetupVisuals();
		// TODO: Get player reference
		_player = GetNodeOrNull<Node2D>(PlayerPath);
		_particleSystem = GetNodeOrNull<GpuParticles2D>(ParticleSystemPath);
		// TODO: Setup alarm timer
		_alarmTimer = new Timer
		{
			WaitTime = 1.5f,
			OneShot = true
		};
		AddChild(_alarmTimer);
		_alarmTimer.Timeout += ResetAlarm;
		
		_redTint = GetNodeOrNull<ColorRect>(RedTintPath);
		_redTint.Visible = false;
	}
	
	private void SetupRaycast()
	{
		// TODO: Create and configure RayCast2D
		_rayCast = new RayCast2D();
		AddChild(_rayCast);
		// TODO: Set target position
		_rayCast.TargetPosition = new Vector2(LaserLength, 0);
		_rayCast.Enabled = true;
		// TODO: Set collision mask to detect player
		_rayCast.CollisionMask = 1;
	}
	
	private void SetupVisuals()
	{
		// TODO: Create Line2D for laser visualization
		_laserBeam = new Line2D();
		AddChild(_laserBeam);
		
		// TODO: Set width and color
		_laserBeam.Width = 3f;
		_laserBeam.DefaultColor = LaserColorNormal;
		// TODO: Add points for the line
		_laserBeam.AddPoint(Vector2.Zero);
		_laserBeam.AddPoint(new Vector2(LaserLength, 0));
	}
	
	public override void _PhysicsProcess(double delta)
	{
		// TODO: Force raycast update
		_rayCast.ForceRaycastUpdate();
		// TODO: Check if raycast is colliding
		if (_rayCast.IsColliding())
		{
			// TODO: Get collision point
			var collider = _rayCast.GetCollider();
			// TODO: Update laser beam visualization
			// TODO: Check if hit object is player
			if (collider == _player && !_isAlarmActive)
			{
				// TODO: Trigger alarm if player detected
				TriggerAlarm();
			}
		}
		UpdateLaserBeam();
	}
	
	private void UpdateLaserBeam()
	{
		// TODO: Update Line2D points based on raycast
		if (_rayCast.IsColliding())
		{
			// Show up to collision point if hitting something
			Vector2 hitPoint = ToLocal(_rayCast.GetCollisionPoint());
			_laserBeam.SetPointPosition(1, hitPoint);
		} else {
			// Show full length if no collision
			_laserBeam.SetPointPosition(1, new Vector2(LaserLength, 0));
		}
	}
	
	private void TriggerAlarm()
	{
		// TODO: Change laser color
		_laserBeam.DefaultColor = LaserColorAlert;
		// TODO: Emit signal or call alarm function
		_isAlarmActive = true;
		// TODO: Add visual feedback (flashing, particles, etc.)
		_particleSystem.Emitting = true;
		
		_redTint.Visible = true;
		
		GD.Print("ALARM! Player detected!");
		_alarmTimer.Start();
	}
	
	private void ResetAlarm()
	{
		// TODO: Reset laser to normal color
		_laserBeam.DefaultColor = LaserColorNormal;
		// TODO: Reset alarm state
		_isAlarmActive = false;
		
		_particleSystem.Emitting = false;
	
		_redTint.Visible = false;
	}
}
