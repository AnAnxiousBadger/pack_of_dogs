RSRC                    VisualShader            ��������                                            }      resource_local_to_scene    resource_name    output_port_for_preview    default_input_values    expanded_output_ports    linked_parent_graph_frame    parameter_name 
   qualifier    texture_type    color_default    texture_filter    texture_repeat    texture_source    script    source    texture 	   operator 	   function    op_type    input_name    default_value_enabled    default_value    hint    min    max    step    interpolation_mode    interpolation_color_space    offsets    colors 	   gradient    width    height    use_hdr    fill 
   fill_from    fill_to    repeat    code    graph_offset    mode    modes/blend    modes/depth_draw    modes/cull    modes/diffuse    modes/specular    flags/depth_prepass_alpha    flags/depth_test_disabled    flags/sss_mode_skin    flags/unshaded    flags/wireframe    flags/skip_vertex_transform    flags/world_vertex_coords    flags/ensure_correct_normals    flags/shadows_disabled    flags/ambient_light_disabled    flags/shadow_to_opacity    flags/vertex_lighting    flags/particle_trails    flags/alpha_to_coverage     flags/alpha_to_coverage_and_one    flags/debug_shadow_splits    flags/fog_disabled    nodes/vertex/0/position    nodes/vertex/connections    nodes/fragment/0/position    nodes/fragment/2/node    nodes/fragment/2/position    nodes/fragment/3/node    nodes/fragment/3/position    nodes/fragment/4/node    nodes/fragment/4/position    nodes/fragment/5/node    nodes/fragment/5/position    nodes/fragment/6/node    nodes/fragment/6/position    nodes/fragment/7/node    nodes/fragment/7/position    nodes/fragment/8/node    nodes/fragment/8/position    nodes/fragment/9/node    nodes/fragment/9/position    nodes/fragment/10/node    nodes/fragment/10/position    nodes/fragment/11/node    nodes/fragment/11/position    nodes/fragment/12/node    nodes/fragment/12/position    nodes/fragment/13/node    nodes/fragment/13/position    nodes/fragment/14/node    nodes/fragment/14/position    nodes/fragment/15/node    nodes/fragment/15/position    nodes/fragment/17/node    nodes/fragment/17/position    nodes/fragment/18/node    nodes/fragment/18/position    nodes/fragment/19/node    nodes/fragment/19/position    nodes/fragment/20/node    nodes/fragment/20/position    nodes/fragment/21/node    nodes/fragment/21/position    nodes/fragment/22/node    nodes/fragment/22/position    nodes/fragment/23/node    nodes/fragment/23/position    nodes/fragment/connections    nodes/light/0/position    nodes/light/connections    nodes/start/0/position    nodes/start/connections    nodes/process/0/position    nodes/process/connections    nodes/collide/0/position    nodes/collide/connections    nodes/start_custom/0/position    nodes/start_custom/connections     nodes/process_custom/0/position !   nodes/process_custom/connections    nodes/sky/0/position    nodes/sky/connections    nodes/fog/0/position    nodes/fog/connections        1   local://VisualShaderNodeTexture2DParameter_21iqk 7      &   local://VisualShaderNodeTexture_cym2y �      1   local://VisualShaderNodeTexture2DParameter_xkdkb �      &   local://VisualShaderNodeTexture_85qbc #      &   local://VisualShaderNodeFloatOp_v4cnf k      %   local://VisualShaderNodeUVFunc_yftwh �      '   local://VisualShaderNodeVectorOp_yxorm �      $   local://VisualShaderNodeInput_8culb ;      ,   local://VisualShaderNodeVec2Parameter_67q7o r      &   local://VisualShaderNodeFresnel_1olkx �      &   local://VisualShaderNodeFloatOp_a8wxq �      -   local://VisualShaderNodeFloatParameter_36ab7       -   local://VisualShaderNodeColorParameter_mcgcj e      %   local://VisualShaderNodeUVFunc_qridb �      '   local://VisualShaderNodeVectorOp_plipd �      &   local://VisualShaderNodeFloatOp_iel24 B         local://Gradient_sqadq �          local://GradientTexture2D_cpuiy �      &   local://VisualShaderNodeTexture_wesmn �      &   local://VisualShaderNodeFloatOp_hx1al I      &   local://VisualShaderNodeFloatOp_rahg6 }      -   local://VisualShaderNodeFloatParameter_28tdd �      ,   local://VisualShaderNodeVec2Parameter_5n0r2 �      \   res://Assets/materials/shaders/piece_arrived_effect_shader/piece_arrived_circle_sahder.tres =      #   VisualShaderNodeTexture2DParameter             ring_gradient_texture          VisualShaderNodeTexture                                   #   VisualShaderNodeTexture2DParameter             ring_noise_mask          VisualShaderNodeTexture                                      VisualShaderNodeFloatOp                      VisualShaderNodeUVFunc             VisualShaderNodeVectorOp                    
                 
                                       VisualShaderNodeInput             time          VisualShaderNodeVec2Parameter             noise_panning_speed          VisualShaderNodeFresnel             VisualShaderNodeFloatOp                      VisualShaderNodeFloatParameter             fresnel_power          VisualShaderNodeColorParameter             color          VisualShaderNodeUVFunc             VisualShaderNodeVectorOp                    
                 
                                       VisualShaderNodeFloatOp                                       �               	   Gradient             GradientTexture2D                #   
     �?  �?         VisualShaderNodeTexture                                         VisualShaderNodeFloatOp                      VisualShaderNodeFloatOp                      VisualShaderNodeFloatParameter             opacity          VisualShaderNodeVec2Parameter             circle_panning          VisualShader /   &      $  shader_type spatial;
render_mode blend_mix, depth_draw_opaque, cull_back, diffuse_lambert, specular_schlick_ggx, unshaded;

uniform vec4 color : source_color;
uniform vec2 noise_panning_speed;
uniform vec2 circle_panning;
uniform sampler2D ring_gradient_texture;
uniform sampler2D ring_noise_mask;
uniform float fresnel_power;
uniform sampler2D tex_frg_19;
uniform float opacity;



void fragment() {
// ColorParameter:14
	vec4 n_out14p0 = color;


// Vector2Parameter:10
	vec2 n_out10p0 = noise_panning_speed;


// Vector2Parameter:23
	vec2 n_out23p0 = circle_panning;


// VectorOp:8
	vec2 n_out8p0 = n_out10p0 * n_out23p0;


// UVFunc:7
	vec2 n_in7p1 = vec2(1.00000, 1.00000);
	vec2 n_out7p0 = n_out8p0 * n_in7p1 + UV;


	vec4 n_out3p0;
// Texture2D:3
	n_out3p0 = texture(ring_gradient_texture, n_out7p0);
	float n_out3p1 = n_out3p0.r;


// Input:9
	float n_out9p0 = TIME;


// FloatOp:18
	float n_in18p1 = -0.50000;
	float n_out18p0 = n_out9p0 * n_in18p1;


// VectorOp:17
	vec2 n_out17p0 = n_out10p0 * vec2(n_out18p0);


// UVFunc:15
	vec2 n_in15p1 = vec2(1.00000, 1.00000);
	vec2 n_out15p0 = n_out17p0 * n_in15p1 + UV;


	vec4 n_out5p0;
// Texture2D:5
	n_out5p0 = texture(ring_noise_mask, n_out15p0);
	float n_out5p1 = n_out5p0.r;


// FloatOp:6
	float n_out6p0 = n_out3p1 * n_out5p1;


// FloatParameter:13
	float n_out13p0 = fresnel_power;


// Fresnel:11
	float n_out11p0 = pow(1.0 - clamp(dot(NORMAL, VIEW), 0.0, 1.0), n_out13p0);


// FloatOp:12
	float n_out12p0 = n_out6p0 * n_out11p0;


// Texture2D:19
	vec4 n_out19p0 = texture(tex_frg_19, UV);
	float n_out19p1 = n_out19p0.r;


// FloatOp:20
	float n_out20p0 = n_out12p0 * n_out19p1;


// FloatParameter:22
	float n_out22p0 = opacity;


// FloatOp:21
	float n_out21p0 = n_out20p0 * n_out22p0;


// Output:0
	ALBEDO = vec3(n_out14p0.xyz);
	ALPHA = n_out21p0;


}
 1         A   
     zD  CB             C   
     9�  CD            E   
     ��  p�F            G   
     C�   DH            I   
     ��  �CJ            K   
     p�  �CL            M   
     /�  ��N            O   
     p�  ��P            Q   
    ���  �CR            S   
     ��  ��T         	   U   
     ��  DV         
   W   
      C  �CX            Y   
     ��  9DZ            [   
     \C  \�\            ]   
     *�  �C^            _   
     u�  �C`            a   
     ��  �Cb            c   
      C  >Dd            e   
   u��C)$Df            g   
     %D  Dh            i   
     �C  /Dj            k   
    ���  4�l       X                                                                    
                                                                                                
                           	                                                                                                                 RSRC