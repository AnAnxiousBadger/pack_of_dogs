RSRC                    VisualShader            ��������                                            S      resource_local_to_scene    resource_name    output_port_for_preview    default_input_values    expanded_output_ports    linked_parent_graph_frame    script    parameter_name 
   qualifier    texture_type    color_default    texture_filter    texture_repeat    texture_source    source    texture    input_name 	   operator    hint    min    max    step    default_value_enabled    default_value    code    graph_offset    mode    modes/blend    modes/depth_draw    modes/cull    modes/diffuse    modes/specular    flags/depth_prepass_alpha    flags/depth_test_disabled    flags/sss_mode_skin    flags/unshaded    flags/wireframe    flags/skip_vertex_transform    flags/world_vertex_coords    flags/ensure_correct_normals    flags/shadows_disabled    flags/ambient_light_disabled    flags/shadow_to_opacity    flags/vertex_lighting    flags/particle_trails    flags/alpha_to_coverage     flags/alpha_to_coverage_and_one    flags/debug_shadow_splits    flags/fog_disabled    nodes/vertex/0/position    nodes/vertex/connections    nodes/fragment/0/position    nodes/fragment/2/node    nodes/fragment/2/position    nodes/fragment/5/node    nodes/fragment/5/position    nodes/fragment/6/node    nodes/fragment/6/position    nodes/fragment/7/node    nodes/fragment/7/position    nodes/fragment/9/node    nodes/fragment/9/position    nodes/fragment/10/node    nodes/fragment/10/position    nodes/fragment/11/node    nodes/fragment/11/position    nodes/fragment/connections    nodes/light/0/position    nodes/light/connections    nodes/start/0/position    nodes/start/connections    nodes/process/0/position    nodes/process/connections    nodes/collide/0/position    nodes/collide/connections    nodes/start_custom/0/position    nodes/start_custom/connections     nodes/process_custom/0/position !   nodes/process_custom/connections    nodes/sky/0/position    nodes/sky/connections    nodes/fog/0/position    nodes/fog/connections        &   local://VisualShaderNodeFresnel_8ha3e  
      1   local://VisualShaderNodeTexture2DParameter_38i2v x
      &   local://VisualShaderNodeTexture_8qgvo �
      $   local://VisualShaderNodeInput_fii13 �
      &   local://VisualShaderNodeFloatOp_0aiiy G      &   local://VisualShaderNodeFloatOp_ng6a2 {      -   local://VisualShaderNodeFloatParameter_63xyc �      \   res://Assets/materials/shaders/node_modifier_visual_effect_shaders/rosette_mesh_shader.tres          VisualShaderNodeFresnel                                )   �������?      #   VisualShaderNodeTexture2DParameter             rosette_diffuse          VisualShaderNodeTexture                      VisualShaderNodeInput                             color          VisualShaderNodeFloatOp                      VisualShaderNodeFloatOp                      VisualShaderNodeFloatParameter             alpha                           �?         VisualShader          �  shader_type spatial;
render_mode blend_mix, depth_draw_opaque, cull_back, diffuse_lambert, specular_schlick_ggx, unshaded, shadows_disabled;

uniform sampler2D rosette_diffuse;
uniform float alpha : hint_range(0, 1, 0.10000000149012) = 1;



void fragment() {
	vec4 n_out6p0;
// Texture2D:6
	n_out6p0 = texture(rosette_diffuse, UV);


// Input:7
	vec4 n_out7p0 = COLOR;
	float n_out7p4 = n_out7p0.a;


// Fresnel:2
	float n_in2p3 = 0.20000;
	float n_out2p0 = pow(1.0 - clamp(dot(NORMAL, VIEW), 0.0, 1.0), n_in2p3);


// FloatOp:9
	float n_out9p0 = n_out7p4 * n_out2p0;


// FloatParameter:11
	float n_out11p0 = alpha;


// FloatOp:10
	float n_out10p0 = n_out9p0 * n_out11p0;


// Output:0
	ALBEDO = vec3(n_out6p0.xyz);
	ALPHA = n_out10p0;


}
 #         (         4             5   
     �  %D6            7   
     ��  pB8            9   
     �  pB:            ;   
     �  �C<            =   
     H�  �C>            ?   
         �C@            A   
     p�   DB                                             	            	       	       
       
                     
            RSRC