// NOTE: Shader automatically converted from Godot Engine 4.3.stable.mono's StandardMaterial3D.

shader_type spatial;
render_mode blend_mix, depth_draw_opaque, cull_back, diffuse_burley, specular_schlick_ggx;

uniform vec4 albedo : source_color;
uniform sampler2D texture_albedo : source_color, filter_linear_mipmap, repeat_enable;
uniform float alpha_scissor_threshold : hint_range(0.0, 1.0, 0.001);
uniform float point_size : hint_range(0.1, 128.0, 0.1);

uniform float roughness : hint_range(0.0, 1.0);
uniform sampler2D texture_metallic : hint_default_white, filter_linear_mipmap, repeat_enable;
uniform vec4 metallic_texture_channel;
uniform sampler2D texture_roughness : hint_roughness_r, filter_linear_mipmap, repeat_enable;

uniform float specular : hint_range(0.0, 1.0, 0.01);
uniform float metallic : hint_range(0.0, 1.0, 0.01);

uniform sampler2D texture_normal : hint_roughness_normal, filter_linear_mipmap, repeat_enable;
uniform float normal_scale : hint_range(-16.0, 16.0);

uniform vec3 uv1_scale;
uniform vec3 uv1_offset;
uniform vec3 uv2_scale;
uniform vec3 uv2_offset;

group_uniforms Wind;
global uniform vec3 palm_disturb_pos;
uniform float disturb_radius;
uniform float disturb_wind_speed;
uniform float disturb_noise_intensity;
global uniform vec3 wind_direction;
global uniform float wind_intensity;
global uniform float wind_speed;
global uniform float wind_noise_intensity;
uniform sampler2D wind_mask_texture : source_color, filter_linear_mipmap, repeat_enable;
uniform sampler2D wind_noise_texture : source_color, filter_linear_mipmap, repeat_enable;
/*uniform float wind_strength : hint_range(0.0, 5.0)= 0.5;
uniform float wind_speed = 1.0;
uniform float wind_scale : hint_range(1.0, 20.0) = 20.0;
uniform float wind_density = 10.0;*/
group_uniforms;

void vertex() {
	UV = UV * uv1_scale.xy + uv1_offset.xy;
	
	vec3 world_pos = (MODEL_MATRIX * vec4(VERTEX, 1.0)).xyz;
	
	float mask = texture(wind_mask_texture, UV).r;
	
	mat3 inverse_model_matrix = inverse(mat3(MODEL_MATRIX));
	vec3 local_wind_direction = normalize(inverse_model_matrix * wind_direction);
	
	float wind_uv_scale = 1.0;
	float scroll_speed = wind_speed;
	if(length(world_pos - palm_disturb_pos) < disturb_radius){
		scroll_speed = disturb_wind_speed;
	}
	vec2 wind_uv = vec2(
		VERTEX.x * wind_uv_scale + TIME * scroll_speed,
		VERTEX.z * wind_uv_scale
	);
	float wind_noise = texture(wind_noise_texture, wind_uv).r;
	wind_noise = 2.0 * wind_noise - 1.0;
	float noise_intensity = wind_noise_intensity;
	if(length(world_pos - palm_disturb_pos) < disturb_radius){
		noise_intensity = disturb_noise_intensity;
	}
	wind_noise = wind_noise * noise_intensity;
	
	if(UV.y != 1.0){
		VERTEX += local_wind_direction * wind_intensity * mask;
		VERTEX.y += wind_noise * mask;
	}
	//VERTEX += local_wind_direction * wind_intensity * mask;
	//VERTEX.y += wind_noise * mask;
	//VERTEX.y += wind_noise * mask * wind_intensity;
}

void fragment() {
	vec2 base_uv = UV;

	vec4 albedo_tex = texture(texture_albedo, base_uv);
	ALBEDO = albedo.rgb * albedo_tex.rgb;

	float metallic_tex = dot(texture(texture_metallic, base_uv), metallic_texture_channel);
	METALLIC = metallic_tex * metallic;
	SPECULAR = specular;

	vec4 roughness_texture_channel = vec4(1.0, 0.0, 0.0, 0.0);
	float roughness_tex = dot(texture(texture_roughness, base_uv), roughness_texture_channel);
	ROUGHNESS = roughness_tex * roughness;

	// Normal Map: Enabled
	NORMAL_MAP = texture(texture_normal, base_uv).rgb;
	NORMAL_MAP_DEPTH = normal_scale;
	ALPHA *= albedo.a * albedo_tex.a;
	ALPHA_SCISSOR_THRESHOLD = alpha_scissor_threshold;
}
