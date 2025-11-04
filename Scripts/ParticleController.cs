using Godot;

public partial class ParticleController : GpuParticles2D
{
	private ShaderMaterial _shaderMaterial;
	private float _time;
	
	public override void _Ready()
	{
		// TODO: Load and apply custom shader
		var shader = GD.Load<Shader>("res://Shaders/custom_particle.gdshader");
		_shaderMaterial = new ShaderMaterial {Shader = shader};
		ProcessMaterial = _shaderMaterial;
		
		// TODO: Configure particle properties (Amount, Lifetime, Speed, etc.)
		Amount = 200;
		Lifetime = 2.0f;
		SpeedScale = 1.0f;
		Emitting = true;
		
		// TODO: Set process material properties
		// Hint: Use a new ShaderMaterial with your custom shader
		_shaderMaterial.SetShaderParameter("wave_intensity", 0.15f);
		_shaderMaterial.SetShaderParameter("time_scale", 1.5f);
	}
	
	public override void _Process(double delta)
	{
		// TODO: Update shader parameters over time
		// Hint: Use shader parameters to create animated effects
		_time += (float)delta;
		_shaderMaterial.SetShaderParameter("time_scale", 1.5f + Mathf.Sin(_time) * 0.5f);
	}
}
