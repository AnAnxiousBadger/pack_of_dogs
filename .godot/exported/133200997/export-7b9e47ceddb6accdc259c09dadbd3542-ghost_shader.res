RSRC                    VisualShader            ��������                                            o      resource_local_to_scene    resource_name    output_port_for_preview    default_input_values    expanded_output_ports    linked_parent_graph_frame    input_name    script    parameter_name 
   qualifier    texture_type    color_default    texture_filter    texture_repeat    texture_source    source    texture    op_type 	   operator 	   function    default_value_enabled    default_value    code    graph_offset    mode    modes/blend    modes/depth_draw    modes/cull    modes/diffuse    modes/specular    flags/depth_prepass_alpha    flags/depth_test_disabled    flags/sss_mode_skin    flags/unshaded    flags/wireframe    flags/skip_vertex_transform    flags/world_vertex_coords    flags/ensure_correct_normals    flags/shadows_disabled    flags/ambient_light_disabled    flags/shadow_to_opacity    flags/vertex_lighting    flags/particle_trails    flags/alpha_to_coverage     flags/alpha_to_coverage_and_one    flags/debug_shadow_splits    flags/fog_disabled    nodes/vertex/0/position    nodes/vertex/2/node    nodes/vertex/2/position    nodes/vertex/3/node    nodes/vertex/3/position    nodes/vertex/4/node    nodes/vertex/4/position    nodes/vertex/5/node    nodes/vertex/5/position    nodes/vertex/6/node    nodes/vertex/6/position    nodes/vertex/7/node    nodes/vertex/7/position    nodes/vertex/8/node    nodes/vertex/8/position    nodes/vertex/9/node    nodes/vertex/9/position    nodes/vertex/10/node    nodes/vertex/10/position    nodes/vertex/connections    nodes/fragment/0/position    nodes/fragment/2/node    nodes/fragment/2/position    nodes/fragment/3/node    nodes/fragment/3/position    nodes/fragment/4/node    nodes/fragment/4/position    nodes/fragment/5/node    nodes/fragment/5/position    nodes/fragment/6/node    nodes/fragment/6/position    nodes/fragment/7/node    nodes/fragment/7/position    nodes/fragment/8/node    nodes/fragment/8/position    nodes/fragment/9/node    nodes/fragment/9/position    nodes/fragment/10/node    nodes/fragment/10/position    nodes/fragment/11/node    nodes/fragment/11/position    nodes/fragment/12/node    nodes/fragment/12/position    nodes/fragment/14/node    nodes/fragment/14/position    nodes/fragment/15/node    nodes/fragment/15/position    nodes/fragment/connections    nodes/light/0/position    nodes/light/connections    nodes/start/0/position    nodes/start/connections    nodes/process/0/position    nodes/process/connections    nodes/collide/0/position    nodes/collide/connections    nodes/start_custom/0/position    nodes/start_custom/connections     nodes/process_custom/0/position !   nodes/process_custom/connections    nodes/sky/0/position    nodes/sky/connections    nodes/fog/0/position    nodes/fog/connections        $   local://VisualShaderNodeInput_ge3do 3      1   local://VisualShaderNodeTexture2DParameter_bealy l      &   local://VisualShaderNodeTexture_b2new �      '   local://VisualShaderNodeVectorOp_2cqdu �      '   local://VisualShaderNodeVectorOp_7ho7p !      %   local://VisualShaderNodeUVFunc_68lph �      $   local://VisualShaderNodeInput_hrer6 �      ,   local://VisualShaderNodeVec2Parameter_nwxp2 �      '   local://VisualShaderNodeVectorOp_u0qgr E      1   local://VisualShaderNodeTexture2DParameter_54qe0 �      1   local://VisualShaderNodeTexture2DParameter_8p1p6       &   local://VisualShaderNodeTexture_os5kr l      &   local://VisualShaderNodeTexture_c3lvg �      &   local://VisualShaderNodeFloatOp_2emqp �      %   local://VisualShaderNodeUVFunc_c1f6e 0      $   local://VisualShaderNodeInput_qd2cf W      ,   local://VisualShaderNodeVec2Parameter_5dvrw �      '   local://VisualShaderNodeVectorOp_lvkn0 �      -   local://VisualShaderNodeColorParameter_nwdws W      '   local://VisualShaderNodeVectorOp_73i7x �      '   local://VisualShaderNodeVectorOp_61cgy 1      $   local://VisualShaderNodeInput_vesdm �      >   res://Assets/materials/shaders/ghost_shader/ghost_shader.tres �         VisualShaderNodeInput             vertex       #   VisualShaderNodeTexture2DParameter             vertex_noise          VisualShaderNodeTexture                      VisualShaderNodeVectorOp                      VisualShaderNodeVectorOp                                            ���=���=���=                  VisualShaderNodeUVFunc             VisualShaderNodeInput             time          VisualShaderNodeVec2Parameter             vertex_noise_panning_speed          VisualShaderNodeVectorOp                    
                 
                                    #   VisualShaderNodeTexture2DParameter              alpha_vertical_gradient_texture       #   VisualShaderNodeTexture2DParameter             alpha_smoke_texture          VisualShaderNodeTexture                                      VisualShaderNodeTexture                                      VisualShaderNodeFloatOp                      VisualShaderNodeUVFunc             VisualShaderNodeInput             time          VisualShaderNodeVec2Parameter             alpha_noise_panning_speed          VisualShaderNodeVectorOp                    
                 
                                       VisualShaderNodeColorParameter                             color          VisualShaderNodeVectorOp                                                                                           VisualShaderNodeVectorOp                    
                 
                              VisualShaderNodeInput             node_position_world          VisualShader 2         �  shader_type spatial;
render_mode blend_mix, depth_draw_opaque, cull_back, diffuse_lambert, specular_schlick_ggx;

uniform vec2 vertex_noise_panning_speed;
uniform sampler2D vertex_noise;
uniform vec4 color : source_color;
uniform vec2 alpha_noise_panning_speed;
uniform sampler2D alpha_smoke_texture;
uniform sampler2D alpha_vertical_gradient_texture;



void vertex() {
// Input:2
	vec3 n_out2p0 = VERTEX;


// Input:8
	float n_out8p0 = TIME;


// Vector2Parameter:9
	vec2 n_out9p0 = vertex_noise_panning_speed;


// VectorOp:10
	vec2 n_out10p0 = vec2(n_out8p0) * n_out9p0;


// UVFunc:7
	vec2 n_in7p1 = vec2(1.00000, 1.00000);
	vec2 n_out7p0 = n_out10p0 * n_in7p1 + UV;


	vec4 n_out4p0;
// Texture2D:4
	n_out4p0 = texture(vertex_noise, n_out7p0);


// VectorOp:5
	vec3 n_out5p0 = n_out2p0 * vec3(n_out4p0.xyz);


// Output:0
	VERTEX = n_out5p0;


}

void fragment() {
// ColorParameter:11
	vec4 n_out11p0 = color;


// Input:8
	float n_out8p0 = TIME;


// Vector2Parameter:9
	vec2 n_out9p0 = alpha_noise_panning_speed;


// VectorOp:10
	vec2 n_out10p0 = vec2(n_out8p0) * n_out9p0;


// Input:15
	vec3 n_out15p0 = NODE_POSITION_WORLD;


// VectorOp:14
	vec2 n_out14p0 = n_out10p0 + vec2(n_out15p0.xy);


// UVFunc:7
	vec2 n_in7p1 = vec2(1.00000, 1.00000);
	vec2 n_out7p0 = n_out14p0 * n_in7p1 + UV;


	vec4 n_out5p0;
// Texture2D:5
	n_out5p0 = texture(alpha_smoke_texture, n_out7p0);
	float n_out5p1 = n_out5p0.r;


// VectorOp:12
	vec4 n_out12p0 = n_out11p0 * n_out5p0;


	vec4 n_out4p0;
// Texture2D:4
	n_out4p0 = texture(alpha_vertical_gradient_texture, UV);
	float n_out4p1 = n_out4p0.r;


// FloatOp:6
	float n_out6p0 = n_out4p1 * n_out5p1;


// Output:0
	ALBEDO = vec3(n_out12p0.xyz);
	ALPHA = n_out6p0;


}
 /   
     D  �B0             1   
     ��  �B2            3   
     ��  �C4            5   
     H�  �C6            7   
     pC  pB8            9   
     pB  pC:            ;   
     ��  pC<            =   
     ��  \C>            ?   
     ��  �C@            A   
     4�  �CB       $                                                                        	       
             
       
                           C   
     \D  4CD         	   E   
     ��   CF         
   G   
     ��  RDH            I   
     �  pCJ            K   
     �  �CL            M   
     �C  �CN            O   
     �  DP            Q   
     ��  �CR            S   
     ��  %DT            U   
     f�  DV            W   
     �B  ��X            Y   
   ۩�C��BZ            [   
   L�0Į�6D\            ]   
     ��  aD^       8                                                                           
       	       
                                                              
                                              RSRC