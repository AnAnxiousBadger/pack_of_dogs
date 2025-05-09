RSRC                    VisualShader            ��������                                            y      resource_local_to_scene    resource_name    output_port_for_preview    default_input_values    expanded_output_ports    linked_parent_graph_frame    parameter_name 
   qualifier    texture_type    color_default    texture_filter    texture_repeat    texture_source    script    source    texture 	   operator    input_name    op_type 	   function    hint    min    max    step    default_value_enabled    default_value    code    graph_offset    mode    modes/blend    modes/depth_draw    modes/cull    modes/diffuse    modes/specular    flags/depth_prepass_alpha    flags/depth_test_disabled    flags/sss_mode_skin    flags/unshaded    flags/wireframe    flags/skip_vertex_transform    flags/world_vertex_coords    flags/ensure_correct_normals    flags/shadows_disabled    flags/ambient_light_disabled    flags/shadow_to_opacity    flags/vertex_lighting    flags/particle_trails    flags/alpha_to_coverage     flags/alpha_to_coverage_and_one    flags/debug_shadow_splits    flags/fog_disabled    nodes/vertex/0/position    nodes/vertex/connections    nodes/fragment/0/position    nodes/fragment/2/node    nodes/fragment/2/position    nodes/fragment/3/node    nodes/fragment/3/position    nodes/fragment/4/node    nodes/fragment/4/position    nodes/fragment/5/node    nodes/fragment/5/position    nodes/fragment/6/node    nodes/fragment/6/position    nodes/fragment/7/node    nodes/fragment/7/position    nodes/fragment/8/node    nodes/fragment/8/position    nodes/fragment/9/node    nodes/fragment/9/position    nodes/fragment/10/node    nodes/fragment/10/position    nodes/fragment/11/node    nodes/fragment/11/position    nodes/fragment/12/node    nodes/fragment/12/position    nodes/fragment/13/node    nodes/fragment/13/position    nodes/fragment/14/node    nodes/fragment/14/position    nodes/fragment/15/node    nodes/fragment/15/position    nodes/fragment/16/node    nodes/fragment/16/position    nodes/fragment/17/node    nodes/fragment/17/position    nodes/fragment/18/node    nodes/fragment/18/position    nodes/fragment/19/node    nodes/fragment/19/position    nodes/fragment/20/node    nodes/fragment/20/position    nodes/fragment/33/node    nodes/fragment/33/position    nodes/fragment/34/node    nodes/fragment/34/position    nodes/fragment/36/node    nodes/fragment/36/position    nodes/fragment/37/node    nodes/fragment/37/position    nodes/fragment/38/node    nodes/fragment/38/position    nodes/fragment/39/node    nodes/fragment/39/position    nodes/fragment/connections    nodes/light/0/position    nodes/light/connections    nodes/start/0/position    nodes/start/connections    nodes/process/0/position    nodes/process/connections    nodes/collide/0/position    nodes/collide/connections    nodes/start_custom/0/position    nodes/start_custom/connections     nodes/process_custom/0/position !   nodes/process_custom/connections    nodes/sky/0/position    nodes/sky/connections    nodes/fog/0/position    nodes/fog/connections        1   local://VisualShaderNodeTexture2DParameter_c0a1i �      &   local://VisualShaderNodeTexture_uuphh G      1   local://VisualShaderNodeTexture2DParameter_jnrdn �      &   local://VisualShaderNodeTexture_kv2ta �      &   local://VisualShaderNodeFloatOp_rl26e       $   local://VisualShaderNodeInput_hgs7r       '   local://VisualShaderNodeVectorOp_eooju �      (   local://VisualShaderNodeFloatFunc_5fp6a Y      (   local://VisualShaderNodeFloatFunc_8yayw �      .   local://VisualShaderNodeVectorDecompose_r5s6k �      &   local://VisualShaderNodeFloatOp_qwbqu E      &   local://VisualShaderNodeFloatOp_0hj4e �      &   local://VisualShaderNodeFloatOp_cxn4q �      &   local://VisualShaderNodeFloatOp_o7p14       &   local://VisualShaderNodeFloatOp_6tw56 E      &   local://VisualShaderNodeFloatOp_otqrk �      ,   local://VisualShaderNodeVectorCompose_mp6mr �      '   local://VisualShaderNodeVectorOp_j08w6 �      &   local://VisualShaderNodeFloatOp_ark5f �      -   local://VisualShaderNodeFloatParameter_yki1p �      $   local://VisualShaderNodeInput_h20m8 (      &   local://VisualShaderNodeFloatOp_vw25d _      -   local://VisualShaderNodeFloatParameter_2siuy �      1   local://VisualShaderNodeTexture2DParameter_1au68       &   local://VisualShaderNodeTexture_m3xyg m      a   res://Assets/materials/shaders/node_modifier_visual_effect_shaders/rosette_node_glow_shader.tres �      #   VisualShaderNodeTexture2DParameter          	   noise_01          VisualShaderNodeTexture                                   #   VisualShaderNodeTexture2DParameter          	   noise_02          VisualShaderNodeTexture                                      VisualShaderNodeFloatOp                                      �?                  VisualShaderNodeInput                                 uv          VisualShaderNodeVectorOp                              
                 
      ?   ?                                      VisualShaderNodeFloatFunc                                 VisualShaderNodeFloatFunc                                 VisualShaderNodeVectorDecompose                    
                                        VisualShaderNodeFloatOp                                VisualShaderNodeFloatOp                                VisualShaderNodeFloatOp                                VisualShaderNodeFloatOp                                VisualShaderNodeFloatOp                                VisualShaderNodeFloatOp                       VisualShaderNodeVectorCompose                                 VisualShaderNodeVectorOp                              
                 
      ?   ?                             VisualShaderNodeFloatOp                                VisualShaderNodeFloatParameter             noise_rotation_speed                  �?         VisualShaderNodeInput             time          VisualShaderNodeFloatOp                                      �?                  VisualShaderNodeFloatParameter             shader_alpha                  �?      #   VisualShaderNodeTexture2DParameter             color_gradient          VisualShaderNodeTexture                      VisualShader 6         
  shader_type spatial;
render_mode blend_mix, depth_draw_opaque, cull_back, diffuse_lambert, specular_schlick_ggx;

uniform sampler2D color_gradient;
uniform float noise_rotation_speed = 1;
uniform sampler2D noise_01;
uniform sampler2D noise_02;
uniform float shader_alpha = 1;



void fragment() {
	vec4 n_out39p0;
// Texture2D:39
	n_out39p0 = texture(color_gradient, UV);


// Input:7
	vec2 n_out7p0 = UV;


// VectorOp:8
	vec2 n_in8p1 = vec2(0.50000, 0.50000);
	vec2 n_out8p0 = n_out7p0 - n_in8p1;


// VectorDecompose:11
	float n_out11p0 = n_out8p0.x;
	float n_out11p1 = n_out8p0.y;


// FloatParameter:33
	float n_out33p0 = noise_rotation_speed;


// Input:34
	float n_out34p0 = TIME;


// FloatOp:20
	float n_out20p0 = n_out33p0 * n_out34p0;


// FloatFunc:10
	float n_out10p0 = cos(n_out20p0);


// FloatOp:12
	float n_out12p0 = n_out11p0 * n_out10p0;


// FloatFunc:9
	float n_out9p0 = sin(n_out20p0);


// FloatOp:13
	float n_out13p0 = n_out11p1 * n_out9p0;


// FloatOp:16
	float n_out16p0 = n_out12p0 - n_out13p0;


// FloatOp:14
	float n_out14p0 = n_out11p0 * n_out9p0;


// FloatOp:15
	float n_out15p0 = n_out11p1 * n_out10p0;


// FloatOp:17
	float n_out17p0 = n_out14p0 + n_out15p0;


// VectorCompose:18
	vec2 n_out18p0 = vec2(n_out16p0, n_out17p0);


// VectorOp:19
	vec2 n_in19p1 = vec2(0.50000, 0.50000);
	vec2 n_out19p0 = n_out18p0 + n_in19p1;


	vec4 n_out3p0;
// Texture2D:3
	n_out3p0 = texture(noise_01, n_out19p0);
	float n_out3p1 = n_out3p0.r;


	vec4 n_out5p0;
// Texture2D:5
	n_out5p0 = texture(noise_02, UV);
	float n_out5p1 = n_out5p0.r;


// FloatOp:6
	float n_out6p0 = n_out3p1 * n_out5p1;


// FloatParameter:37
	float n_out37p0 = shader_alpha;


// FloatOp:36
	float n_out36p0 = n_out6p0 * n_out37p0;


// Output:0
	ALBEDO = vec3(n_out39p0.xyz);
	ALPHA = n_out36p0;


}
 5   
     zD  ��6             7   
     �  \C8            9   
      �    :            ;   
     9�  D<            =   
     ��  D>            ?   
     pC  �C@            A   
    ��  ��B            C   
     ��  ��D            E   
     ��   BF            G   
     ��  CH         	   I   
     ��  H�J         
   K   
    ���  H�L            M   
    ���   �N            O   
    ���  �BP            Q   
    ���  �CR            S   
     ��  H�T            U   
     ��  �BV            W   
    ���  p�X            Y   
     f�  p�Z            [   
     �  pB\            ]   
   é(�eJ��^            _   
    �(�  4C`            a   
     D  �Cb            c   
     HC  �Cd            e   
     �B  \�f            g   
     �C   �h       |                                                                                    
                          	                           	             
                                                                                                                                
              	                          $       %       $      $              !              "                           &       '      '                     RSRC