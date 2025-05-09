RSRC                    VisualShader            ��������                                            g      resource_local_to_scene    resource_name    output_port_for_preview    default_input_values    expanded_output_ports    linked_parent_graph_frame    parameter_name 
   qualifier    texture_type    color_default    texture_filter    texture_repeat    texture_source    script    source    texture    default_value_enabled    default_value    input_name    op_type 	   operator 	   function    code    graph_offset    mode    modes/blend    modes/depth_draw    modes/cull    modes/diffuse    modes/specular    flags/depth_prepass_alpha    flags/depth_test_disabled    flags/sss_mode_skin    flags/unshaded    flags/wireframe    flags/skip_vertex_transform    flags/world_vertex_coords    flags/ensure_correct_normals    flags/shadows_disabled    flags/ambient_light_disabled    flags/shadow_to_opacity    flags/vertex_lighting    flags/particle_trails    flags/alpha_to_coverage     flags/alpha_to_coverage_and_one    flags/debug_shadow_splits    flags/fog_disabled    nodes/vertex/0/position    nodes/vertex/connections    nodes/fragment/0/position    nodes/fragment/2/node    nodes/fragment/2/position    nodes/fragment/3/node    nodes/fragment/3/position    nodes/fragment/4/node    nodes/fragment/4/position    nodes/fragment/5/node    nodes/fragment/5/position    nodes/fragment/6/node    nodes/fragment/6/position    nodes/fragment/7/node    nodes/fragment/7/position    nodes/fragment/8/node    nodes/fragment/8/position    nodes/fragment/9/node    nodes/fragment/9/position    nodes/fragment/10/node    nodes/fragment/10/position    nodes/fragment/11/node    nodes/fragment/11/position    nodes/fragment/12/node    nodes/fragment/12/position    nodes/fragment/15/node    nodes/fragment/15/position    nodes/fragment/16/node    nodes/fragment/16/position    nodes/fragment/17/node    nodes/fragment/17/position    nodes/fragment/19/node    nodes/fragment/19/position    nodes/fragment/20/node    nodes/fragment/20/position    nodes/fragment/22/node    nodes/fragment/22/position    nodes/fragment/23/node    nodes/fragment/23/position    nodes/fragment/connections    nodes/light/0/position    nodes/light/connections    nodes/start/0/position    nodes/start/connections    nodes/process/0/position    nodes/process/connections    nodes/collide/0/position    nodes/collide/connections    nodes/start_custom/0/position    nodes/start_custom/connections     nodes/process_custom/0/position !   nodes/process_custom/connections    nodes/sky/0/position    nodes/sky/connections    nodes/fog/0/position    nodes/fog/connections        1   local://VisualShaderNodeTexture2DParameter_fyxew �      &   local://VisualShaderNodeTexture_mkc1l �      ,   local://VisualShaderNodeVec2Parameter_s05l7 D      $   local://VisualShaderNodeInput_knjqn �      '   local://VisualShaderNodeVectorOp_j60sr �      %   local://VisualShaderNodeUVFunc_a18cg 1      ,   local://VisualShaderNodeVec2Parameter_01cm2 X      '   local://VisualShaderNodeVectorOp_dsto0 �      $   local://VisualShaderNodeInput_hvoer       1   local://VisualShaderNodeTexture2DParameter_w7a2q L      &   local://VisualShaderNodeTexture_fcc00 �      &   local://VisualShaderNodeFresnel_5gkxl �      &   local://VisualShaderNodeFloatOp_ah8w5 7      '   local://VisualShaderNodeVectorOp_kfycp k      ,   local://VisualShaderNodeVec2Parameter_n01fu �      '   local://VisualShaderNodeVectorOp_px0bw .      $   local://VisualShaderNodeInput_68w8a �      %   local://VisualShaderNodeUVFunc_hj3un �      B   res://Assets/materials/shaders/tornado_shader/tornado_shader.tres �      #   VisualShaderNodeTexture2DParameter             diffuse_noise          VisualShaderNodeTexture                                      VisualShaderNodeVec2Parameter          	   uv_scale          VisualShaderNodeInput             uv          VisualShaderNodeVectorOp                    
                 
                                       VisualShaderNodeUVFunc             VisualShaderNodeVec2Parameter             panning_speed          VisualShaderNodeVectorOp                    
                 
                                       VisualShaderNodeInput             time       #   VisualShaderNodeTexture2DParameter             alpha_noise          VisualShaderNodeTexture                                      VisualShaderNodeFresnel                               )   �������?         VisualShaderNodeFloatOp                      VisualShaderNodeVectorOp                    
                 
                                       VisualShaderNodeVec2Parameter             alpha_panning_speed          VisualShaderNodeVectorOp                    
                 
                                       VisualShaderNodeInput             uv          VisualShaderNodeUVFunc             VisualShader (         �  shader_type spatial;
render_mode blend_mix, depth_draw_opaque, cull_back, diffuse_lambert, specular_schlick_ggx;

uniform vec2 uv_scale;
uniform vec2 panning_speed;
uniform sampler2D diffuse_noise;
uniform vec2 alpha_panning_speed;
uniform sampler2D alpha_noise;



void fragment() {
// Input:5
	vec2 n_out5p0 = UV;


// Vector2Parameter:4
	vec2 n_out4p0 = uv_scale;


// VectorOp:6
	vec2 n_out6p0 = n_out5p0 * n_out4p0;


// Vector2Parameter:8
	vec2 n_out8p0 = panning_speed;


// Input:10
	float n_out10p0 = TIME;


// VectorOp:9
	vec2 n_out9p0 = n_out8p0 * vec2(n_out10p0);


// UVFunc:7
	vec2 n_in7p1 = vec2(1.00000, 1.00000);
	vec2 n_out7p0 = n_out9p0 * n_in7p1 + n_out6p0;


	vec4 n_out3p0;
// Texture2D:3
	n_out3p0 = texture(diffuse_noise, n_out7p0);


// Input:22
	vec2 n_out22p0 = UV;


// VectorOp:17
	vec2 n_out17p0 = n_out22p0 * n_out4p0;


// Vector2Parameter:19
	vec2 n_out19p0 = alpha_panning_speed;


// VectorOp:20
	vec2 n_out20p0 = n_out19p0 * vec2(n_out10p0);


// UVFunc:23
	vec2 n_in23p1 = vec2(1.00000, 1.00000);
	vec2 n_out23p0 = n_out20p0 * n_in23p1 + n_out17p0;


	vec4 n_out12p0;
// Texture2D:12
	n_out12p0 = texture(alpha_noise, n_out23p0);
	float n_out12p1 = n_out12p0.r;


// Fresnel:15
	float n_in15p3 = 0.80000;
	float n_out15p0 = pow(clamp(dot(NORMAL, VIEW), 0.0, 1.0), n_in15p3);


// FloatOp:16
	float n_out16p0 = n_out12p1 * n_out15p0;


// Output:0
	ALBEDO = vec3(n_out3p0.xyz);
	ALPHA = n_out16p0;


}
 1   
     D  �B2             3   
     ��  �C4            5   
     4�  p�6            7   
     ��  H�8            9   
    ���  ��:            ;   
     ��  ��<            =   
     W�  ��>            ?   
    ���   B@            A   
    ���   BB            C   
    ���  �CD         	   E   
     %�  \DF         
   G   
     ��  %DH            I   
     \�  kDJ            K   
     �A   DL            M   
    ���  �CN            O   
     ��  HDP            Q   
     ��  HDR            S   
     ��  �CT            U   
     H�  %DV       P                                                                              	       
       	      	                                                                                                                                      
                                                RSRC