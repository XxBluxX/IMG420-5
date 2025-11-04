shader_type canvas_item;

uniform float wave_intensity = 0.1;
uniform vec4 color_start : source_color = vec4(1.0, 0.5, 0.0, 1.0);
uniform vec4 color_end : source_color = vec4(1.0, 0.0, 0.5, 1.0);
uniform float time_scale = 1.0;

void fragment() {
	vec2 uv = UV;
	
	uv.x += sin(uv.y * 10.0 + TIME * time_scale) * wave_intensity;
	
	vec4 color = mix(color_start,color_end, uv.y);
	
	COLOR = color;
}