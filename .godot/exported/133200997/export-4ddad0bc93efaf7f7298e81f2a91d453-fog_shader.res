RSRC                    VisualShader            ��������                                            U      resource_local_to_scene    resource_name    output_port_for_preview    default_input_values    expanded_output_ports    linked_parent_graph_frame    parameter_name 
   qualifier    texture_type    color_default    texture_filter    texture_repeat    texture_source    script    source    texture 	   function    input_name    default_value_enabled    default_value    op_type 	   operator    code    graph_offset    mode    modes/blend    modes/depth_draw    modes/cull    modes/diffuse    modes/specular    flags/depth_prepass_alpha    flags/depth_test_disabled    flags/sss_mode_skin    flags/unshaded    flags/wireframe    flags/skip_vertex_transform    flags/world_vertex_coords    flags/ensure_correct_normals    flags/shadows_disabled    flags/ambient_light_disabled    flags/shadow_to_opacity    flags/vertex_lighting    flags/particle_trails    flags/alpha_to_coverage     flags/alpha_to_coverage_and_one    flags/debug_shadow_splits    flags/fog_disabled    nodes/vertex/0/position    nodes/vertex/connections    nodes/fragment/0/position    nodes/fragment/2/node    nodes/fragment/2/position    nodes/fragment/3/node    nodes/fragment/3/position    nodes/fragment/4/node    nodes/fragment/4/position    nodes/fragment/5/node    nodes/fragment/5/position    nodes/fragment/6/node    nodes/fragment/6/position    nodes/fragment/7/node    nodes/fragment/7/position    nodes/fragment/8/node    nodes/fragment/8/position    nodes/fragment/9/node    nodes/fragment/9/position    nodes/fragment/10/node    nodes/fragment/10/position    nodes/fragment/connections    nodes/light/0/position    nodes/light/connections    nodes/start/0/position    nodes/start/connections    nodes/process/0/position    nodes/process/connections    nodes/collide/0/position    nodes/collide/connections    nodes/start_custom/0/position    nodes/start_custom/connections     nodes/process_custom/0/position !   nodes/process_custom/connections    nodes/sky/0/position    nodes/sky/connections    nodes/fog/0/position    nodes/fog/connections     
   1   local://VisualShaderNodeTexture2DParameter_r05vg �
      &   local://VisualShaderNodeTexture_jw71o       %   local://VisualShaderNodeUVFunc_acscp g      $   local://VisualShaderNodeInput_d6lba �      ,   local://VisualShaderNodeVec2Parameter_8nj04 �      '   local://VisualShaderNodeVectorOp_jv05d 	      -   local://VisualShaderNodeColorParameter_gdekg ~      ,   local://VisualShaderNodeProximityFade_3fons �      &   local://VisualShaderNodeFloatOp_xh00q �      :   res://Assets/materials/shaders/fog_shader/fog_shader.tres %      #   VisualShaderNodeTexture2DParameter             fog_noise_01          VisualShaderNodeTexture                                      VisualShaderNodeUVFunc             VisualShaderNodeInput             time          VisualShaderNodeVec2Parameter          
   fog_speed          VisualShaderNodeVectorOp                    
                 
                                       VisualShaderNodeColorParameter          
   fog_color          VisualShaderNodeProximityFade             VisualShaderNodeFloatOp                      VisualShader          �  shader_type spatial;
render_mode blend_mix, depth_draw_always, cull_back, diffuse_lambert, specular_disabled, unshaded, shadows_disabled, ambient_light_disabled, fog_disabled;

uniform vec4 fog_color : source_color;
uniform vec2 fog_speed;
uniform sampler2D fog_noise_01;
uniform sampler2D depth_tex_frg_9 : hint_depth_texture;



void fragment() {
// ColorParameter:8
	vec4 n_out8p0 = fog_color;


// Vector2Parameter:6
	vec2 n_out6p0 = fog_speed;


// Input:5
	float n_out5p0 = TIME;


// VectorOp:7
	vec2 n_out7p0 = n_out6p0 * vec2(n_out5p0);


// UVFunc:4
	vec2 n_in4p1 = vec2(1.00000, 1.00000);
	vec2 n_out4p0 = n_out7p0 * n_in4p1 + UV;


	vec4 n_out3p0;
// Texture2D:3
	n_out3p0 = texture(fog_noise_01, n_out4p0);
	float n_out3p1 = n_out3p0.r;


	float n_out9p0;
// ProximityFade:9
	float n_in9p0 = 1.00000;
	{
		float __depth_tex = texture(depth_tex_frg_9, SCREEN_UV).r;
		vec4 __depth_world_pos = INV_PROJECTION_MATRIX * vec4(SCREEN_UV * 2.0 - 1.0, __depth_tex, 1.0);
		__depth_world_pos.xyz /= __depth_world_pos.w;
		n_out9p0 = clamp(1.0 - smoothstep(__depth_world_pos.z + n_in9p0, __depth_world_pos.z, VERTEX.z), 0.0, 1.0);
	}


// FloatOp:10
	float n_out10p0 = n_out3p1 * n_out9p0;


// Output:0
	ALBEDO = vec3(n_out8p0.xyz);
	ALPHA = n_out10p0;


}
                   !         &         '         .         1   
     /D  C2             3   
      �  �C4            5   
     �  �B6            7   
     ��  �A8            9   
    ���  HC:            ;   
     ��  ��<            =   
   �A7����B>            ?   
      B  p�@            A   
     �  DB            C   
   {d�Cͼ�CD       $                                                                                           
       	       
      
                    RSRC