RSRC                    VisualShader            ��������                                            y      resource_local_to_scene    resource_name    output_port_for_preview    default_input_values    expanded_output_ports    linked_parent_graph_frame    parameter_name 
   qualifier    texture_type    color_default    texture_filter    texture_repeat    texture_source    script    source    texture    input_name 	   function    op_type 	   operator    default_value_enabled    default_value    hint    min    max    step    code    graph_offset    mode    modes/blend    modes/depth_draw    modes/cull    modes/diffuse    modes/specular    flags/depth_prepass_alpha    flags/depth_test_disabled    flags/sss_mode_skin    flags/unshaded    flags/wireframe    flags/skip_vertex_transform    flags/world_vertex_coords    flags/ensure_correct_normals    flags/shadows_disabled    flags/ambient_light_disabled    flags/shadow_to_opacity    flags/vertex_lighting    flags/particle_trails    flags/alpha_to_coverage     flags/alpha_to_coverage_and_one    flags/debug_shadow_splits    flags/fog_disabled    nodes/vertex/0/position    nodes/vertex/2/node    nodes/vertex/2/position    nodes/vertex/3/node    nodes/vertex/3/position    nodes/vertex/4/node    nodes/vertex/4/position    nodes/vertex/6/node    nodes/vertex/6/position    nodes/vertex/7/node    nodes/vertex/7/position    nodes/vertex/8/node    nodes/vertex/8/position    nodes/vertex/9/node    nodes/vertex/9/position    nodes/vertex/11/node    nodes/vertex/11/position    nodes/vertex/12/node    nodes/vertex/12/position    nodes/vertex/13/node    nodes/vertex/13/position    nodes/vertex/14/node    nodes/vertex/14/position    nodes/vertex/connections    nodes/fragment/0/position    nodes/fragment/2/node    nodes/fragment/2/position    nodes/fragment/3/node    nodes/fragment/3/position    nodes/fragment/4/node    nodes/fragment/4/position    nodes/fragment/5/node    nodes/fragment/5/position    nodes/fragment/6/node    nodes/fragment/6/position    nodes/fragment/7/node    nodes/fragment/7/position    nodes/fragment/8/node    nodes/fragment/8/position    nodes/fragment/9/node    nodes/fragment/9/position    nodes/fragment/10/node    nodes/fragment/10/position    nodes/fragment/11/node    nodes/fragment/11/position    nodes/fragment/12/node    nodes/fragment/12/position    nodes/fragment/13/node    nodes/fragment/13/position    nodes/fragment/14/node    nodes/fragment/14/position    nodes/fragment/15/node    nodes/fragment/15/position    nodes/fragment/connections    nodes/light/0/position    nodes/light/connections    nodes/start/0/position    nodes/start/connections    nodes/process/0/position    nodes/process/connections    nodes/collide/0/position    nodes/collide/connections    nodes/start_custom/0/position    nodes/start_custom/connections     nodes/process_custom/0/position !   nodes/process_custom/connections    nodes/sky/0/position    nodes/sky/connections    nodes/fog/0/position    nodes/fog/connections        1   local://VisualShaderNodeTexture2DParameter_5h81b �      &   local://VisualShaderNodeTexture_ji43c       $   local://VisualShaderNodeInput_b8cr3 V      %   local://VisualShaderNodeUVFunc_6xdda �      $   local://VisualShaderNodeInput_wqcs0 �      '   local://VisualShaderNodeVectorOp_3ows0       ,   local://VisualShaderNodeVec2Parameter_03kd2 v      ,   local://VisualShaderNodeVectorCompose_srd0m �      '   local://VisualShaderNodeVectorOp_c2mp1 �      ,   local://VisualShaderNodeVectorCompose_g82dk t      ,   local://VisualShaderNodeVectorCompose_mdy37 �      1   local://VisualShaderNodeTexture2DParameter_vt8qy �      &   local://VisualShaderNodeTexture_dyfbx 3      1   local://VisualShaderNodeTexture2DParameter_65cf7 g      &   local://VisualShaderNodeTexture_ukif5 �      &   local://VisualShaderNodeFresnel_g8j7l �      &   local://VisualShaderNodeFloatOp_pkv1f Q      -   local://VisualShaderNodeFloatParameter_3h1pv �      1   local://VisualShaderNodeTexture2DParameter_ilwpu �      &   local://VisualShaderNodeTexture_1u0x7 !      &   local://VisualShaderNodeFloatOp_s2ltu i      %   local://VisualShaderNodeUVFunc_651ud �      $   local://VisualShaderNodeInput_3355m �      '   local://VisualShaderNodeVectorOp_c4ncs �      ,   local://VisualShaderNodeVec2Parameter_1heis p      Q   res://Assets/materials/shaders/kur_connection_shader/kur_connections_shader.tres �      #   VisualShaderNodeTexture2DParameter             vertex_noise          VisualShaderNodeTexture                                      VisualShaderNodeInput                             vertex          VisualShaderNodeUVFunc             VisualShaderNodeInput             time          VisualShaderNodeVectorOp                    
                 
     �?  �?                            VisualShaderNodeVec2Parameter             vertex_speed          VisualShaderNodeVectorCompose             VisualShaderNodeVectorOp                    
                 
                                                       VisualShaderNodeVectorCompose                       VisualShaderNodeVectorCompose                    #   VisualShaderNodeTexture2DParameter             ray_texture          VisualShaderNodeTexture                   #   VisualShaderNodeTexture2DParameter             opacity_sahder          VisualShaderNodeTexture                                      VisualShaderNodeFresnel                                      @         VisualShaderNodeFloatOp                      VisualShaderNodeFloatParameter             fresnel_power       #   VisualShaderNodeTexture2DParameter             alpha_noise_texture          VisualShaderNodeTexture                                      VisualShaderNodeFloatOp                      VisualShaderNodeUVFunc             VisualShaderNodeInput             time          VisualShaderNodeVectorOp                    
                 
                                       VisualShaderNodeVec2Parameter             alpha_noise_speed          VisualShader ;         k  shader_type spatial;
render_mode blend_mix, depth_draw_opaque, cull_disabled, diffuse_lambert, specular_disabled, unshaded;

uniform vec2 vertex_speed;
uniform sampler2D vertex_noise;
uniform sampler2D ray_texture;
uniform sampler2D opacity_sahder;
uniform vec2 alpha_noise_speed;
uniform sampler2D alpha_noise_texture;
uniform float fresnel_power;



void vertex() {
// Input:4
	vec3 n_out4p0 = VERTEX;
	float n_out4p1 = n_out4p0.r;
	float n_out4p2 = n_out4p0.g;
	float n_out4p3 = n_out4p0.b;


// VectorCompose:13
	vec2 n_out13p0 = vec2(n_out4p2, n_out4p3);


// Input:7
	float n_out7p0 = TIME;


// Vector2Parameter:9
	vec2 n_out9p0 = vertex_speed;


// VectorOp:8
	vec2 n_out8p0 = vec2(n_out7p0) * n_out9p0;


// UVFunc:6
	vec2 n_in6p1 = vec2(1.00000, 1.00000);
	vec2 n_out6p0 = n_out8p0 * n_in6p1 + UV;


	vec4 n_out3p0;
// Texture2D:3
	n_out3p0 = texture(vertex_noise, n_out6p0);
	float n_out3p2 = n_out3p0.g;
	float n_out3p3 = n_out3p0.b;


// VectorCompose:14
	vec2 n_out14p0 = vec2(n_out3p2, n_out3p3);


// VectorOp:12
	vec2 n_out12p0 = n_out13p0 * n_out14p0;
	float n_out12p1 = n_out12p0.r;
	float n_out12p2 = n_out12p0.g;


// VectorCompose:11
	vec3 n_out11p0 = vec3(n_out4p1, n_out12p1, n_out12p2);


// Output:0
	VERTEX = n_out11p0;


}

void fragment() {
	vec4 n_out3p0;
// Texture2D:3
	n_out3p0 = texture(ray_texture, UV);


	vec4 n_out5p0;
// Texture2D:5
	n_out5p0 = texture(opacity_sahder, UV);
	float n_out5p1 = n_out5p0.r;


// Input:13
	float n_out13p0 = TIME;


// Vector2Parameter:15
	vec2 n_out15p0 = alpha_noise_speed;


// VectorOp:14
	vec2 n_out14p0 = vec2(n_out13p0) * n_out15p0;


// UVFunc:12
	vec2 n_in12p1 = vec2(1.00000, 1.00000);
	vec2 n_out12p0 = n_out14p0 * n_in12p1 + UV;


	vec4 n_out10p0;
// Texture2D:10
	n_out10p0 = texture(alpha_noise_texture, n_out12p0);
	float n_out10p1 = n_out10p0.r;


// FloatOp:11
	float n_out11p0 = n_out5p1 * n_out10p1;


// FloatParameter:8
	float n_out8p0 = fresnel_power;


// Fresnel:6
	float n_out6p0 = pow(1.0 - clamp(dot(NORMAL, VIEW), 0.0, 1.0), n_out8p0);


// FloatOp:7
	float n_out7p0 = n_out11p0 * n_out6p0;


// Output:0
	ALBEDO = vec3(n_out3p0.xyz);
	ALPHA = n_out7p0;


}
          !         %         3   
     WD  \C4             5   
     ��  �C6            7   
     ��  HC8            9   
     p�   �:            ;   
     ��  C<            =   
    ���  �B>            ?   
   ��C��(C@            A   
     ��  �CB            C   
      D  �BD            E   
     �C  ��F         	   G   
     HC  ��H         
   I   
     4C  \CJ       <                                                         	                                                                                                                                              K   
     WD  HCL            M   
     ��  �BN            O   
      B  �BP            Q   
     ��  �CR            S   
     ��  �CT            U   
     �C  fDV            W   
     D  DX            Y   
     �B  zDZ            [   
     ��  uD\            ]   
     �  4D^            _   
   ��'C��D`            a   
     �  >Db            c   
    ���  /Dd            e   
     R�  9Df            g   
     ��  \Dh       8                                                                     	       
                                  
                                 
                                                     RSRC