RSRC                    VisualShader            ��������                                            a      resource_local_to_scene    resource_name    output_port_for_preview    default_input_values    expanded_output_ports    linked_parent_graph_frame    parameter_name 
   qualifier    texture_type    color_default    texture_filter    texture_repeat    texture_source    script    source    texture 	   function    input_name    op_type 	   operator    default_value_enabled    default_value    hint    min    max    step    code    graph_offset    mode    modes/blend    modes/depth_draw    modes/cull    modes/diffuse    modes/specular    flags/depth_prepass_alpha    flags/depth_test_disabled    flags/sss_mode_skin    flags/unshaded    flags/wireframe    flags/skip_vertex_transform    flags/world_vertex_coords    flags/ensure_correct_normals    flags/shadows_disabled    flags/ambient_light_disabled    flags/shadow_to_opacity    flags/vertex_lighting    flags/particle_trails    flags/alpha_to_coverage     flags/alpha_to_coverage_and_one    flags/debug_shadow_splits    flags/fog_disabled    nodes/vertex/0/position    nodes/vertex/connections    nodes/fragment/0/position    nodes/fragment/2/node    nodes/fragment/2/position    nodes/fragment/3/node    nodes/fragment/3/position    nodes/fragment/4/node    nodes/fragment/4/position    nodes/fragment/5/node    nodes/fragment/5/position    nodes/fragment/6/node    nodes/fragment/6/position    nodes/fragment/7/node    nodes/fragment/7/position    nodes/fragment/8/node    nodes/fragment/8/position    nodes/fragment/9/node    nodes/fragment/9/position    nodes/fragment/10/node    nodes/fragment/10/position    nodes/fragment/11/node    nodes/fragment/11/position    nodes/fragment/12/node    nodes/fragment/12/position    nodes/fragment/13/node    nodes/fragment/13/position    nodes/fragment/14/node    nodes/fragment/14/position    nodes/fragment/connections    nodes/light/0/position    nodes/light/connections    nodes/start/0/position    nodes/start/connections    nodes/process/0/position    nodes/process/connections    nodes/collide/0/position    nodes/collide/connections    nodes/start_custom/0/position    nodes/start_custom/connections     nodes/process_custom/0/position !   nodes/process_custom/connections    nodes/sky/0/position    nodes/sky/connections    nodes/fog/0/position    nodes/fog/connections        1   local://VisualShaderNodeTexture2DParameter_411hh �      &   local://VisualShaderNodeTexture_gbe5x *      &   local://VisualShaderNodeFresnel_55o3k ^      1   local://VisualShaderNodeTexture2DParameter_teqoj �      &   local://VisualShaderNodeTexture_2t3ko       %   local://VisualShaderNodeUVFunc_854p8 P      $   local://VisualShaderNodeInput_ryqph �      '   local://VisualShaderNodeVectorOp_smb6g �      ,   local://VisualShaderNodeVec2Parameter_rdg1x W      $   local://VisualShaderNodeInput_46em6 �      '   local://VisualShaderNodeVectorOp_cp6cj �      -   local://VisualShaderNodeFloatParameter_sgk2y J      &   local://VisualShaderNodeFloatOp_3acw0 �      c   res://Assets/materials/shaders/node_modifier_visual_effect_shaders/roll_button_rosette_shader.tres �      #   VisualShaderNodeTexture2DParameter             button_highlight_texture          VisualShaderNodeTexture                      VisualShaderNodeFresnel                                      ?      #   VisualShaderNodeTexture2DParameter             button_highlight_noise          VisualShaderNodeTexture                                      VisualShaderNodeUVFunc                   
     �?  �?      
     �?             VisualShaderNodeInput             time          VisualShaderNodeVectorOp                    
     �?          
                                       VisualShaderNodeVec2Parameter             highligh_speed          VisualShaderNodeInput             uv          VisualShaderNodeVectorOp                    
                 
      @  �?                            VisualShaderNodeFloatParameter             shader_alpha                   VisualShaderNodeFloatOp                      VisualShader          �  shader_type spatial;
render_mode blend_mix, depth_draw_opaque, cull_back, diffuse_lambert, specular_schlick_ggx;

uniform sampler2D button_highlight_texture;
uniform vec2 highligh_speed;
uniform sampler2D button_highlight_noise;
uniform float shader_alpha : hint_range(0, 1);



void fragment() {
	vec4 n_out3p0;
// Texture2D:3
	n_out3p0 = texture(button_highlight_texture, UV);


// Input:11
	vec2 n_out11p0 = UV;


// VectorOp:12
	vec2 n_in12p1 = vec2(2.00000, 1.00000);
	vec2 n_out12p0 = n_out11p0 * n_in12p1;


// Vector2Parameter:10
	vec2 n_out10p0 = highligh_speed;


// Input:8
	float n_out8p0 = TIME;


// VectorOp:9
	vec2 n_out9p0 = n_out10p0 * vec2(n_out8p0);


// UVFunc:7
	vec2 n_in7p1 = vec2(1.00000, 1.00000);
	vec2 n_out7p0 = n_out9p0 * n_in7p1 + n_out12p0;


	vec4 n_out6p0;
// Texture2D:6
	n_out6p0 = texture(button_highlight_noise, n_out7p0);
	float n_out6p1 = n_out6p0.r;


// Fresnel:4
	float n_out4p0 = pow(1.0 - clamp(dot(NORMAL, VIEW), 0.0, 1.0), n_out6p1);


// FloatParameter:13
	float n_out13p0 = shader_alpha;


// FloatOp:14
	float n_out14p0 = n_out4p0 * n_out13p0;


// Output:0
	ALBEDO = vec3(n_out3p0.xyz);
	ALPHA = n_out14p0;


}
 6             7   
     ��  �B8            9   
     pB  �B:            ;   
     \�  �C<            =   
     M�  D>            ?   
     ��  �C@            A   
     M�  �CB            C   
    ���  DD            E   
     ��  �CF            G   
     ��  �CH         	   I   
    ���   CJ         
   K   
     ��   CL            M   
     \�  *DN            O   
     �B  �CP       4                                                                             	      	             
       	                                                                                  RSRC