RSRC                    VisualShader            ��������                                            B      resource_local_to_scene    resource_name    output_port_for_preview    default_input_values    expanded_output_ports    linked_parent_graph_frame    input_name    script    parameter_name 
   qualifier    texture_type    color_default    texture_filter    texture_repeat    texture_source    source    texture    hint    min    max    step    default_value_enabled    default_value    op_type    code    graph_offset    mode    modes/blend    flags/skip_vertex_transform    flags/unshaded    flags/light_only    flags/world_vertex_coords    nodes/vertex/0/position    nodes/vertex/connections    nodes/fragment/0/position    nodes/fragment/2/node    nodes/fragment/2/position    nodes/fragment/3/node    nodes/fragment/3/position    nodes/fragment/4/node    nodes/fragment/4/position    nodes/fragment/5/node    nodes/fragment/5/position    nodes/fragment/6/node    nodes/fragment/6/position    nodes/fragment/7/node    nodes/fragment/7/position    nodes/fragment/8/node    nodes/fragment/8/position    nodes/fragment/connections    nodes/light/0/position    nodes/light/connections    nodes/start/0/position    nodes/start/connections    nodes/process/0/position    nodes/process/connections    nodes/collide/0/position    nodes/collide/connections    nodes/start_custom/0/position    nodes/start_custom/connections     nodes/process_custom/0/position !   nodes/process_custom/connections    nodes/sky/0/position    nodes/sky/connections    nodes/fog/0/position    nodes/fog/connections        $   local://VisualShaderNodeInput_8vtqv /      1   local://VisualShaderNodeTexture2DParameter_514mx k      &   local://VisualShaderNodeTexture_v317n �      -   local://VisualShaderNodeFloatParameter_df3vm 	      "   local://VisualShaderNodeMix_yy6cy o	      -   local://VisualShaderNodeColorParameter_2xpuv 
      -   local://VisualShaderNodeFloatParameter_8qgvd L
      $   res://Assets/UI/UI_blur_shader.tres �
         VisualShaderNodeInput          
   screen_uv       #   VisualShaderNodeTexture2DParameter             ScreenTexture                                     VisualShaderNodeTexture                      VisualShaderNodeFloatParameter             blur_amount                  �@         VisualShaderNodeMix                                                            �?  �?  �?  �?            ?   ?   ?   ?                  VisualShaderNodeColorParameter             color          VisualShaderNodeFloatParameter             color_weight                ��L=         VisualShader          �  shader_type canvas_item;
render_mode blend_disabled;

uniform float blur_amount : hint_range(0, 5, 0.10000000149012);
uniform sampler2D ScreenTexture : filter_linear_mipmap, repeat_enable, hint_screen_texture;
uniform vec4 color : source_color;
uniform float color_weight : hint_range(0, 1, 0.05000000074506);



void fragment() {
// Input:2
	vec2 n_out2p0 = SCREEN_UV;


// FloatParameter:5
	float n_out5p0 = blur_amount;


	vec4 n_out4p0;
// Texture2D:4
	n_out4p0 = textureLod(ScreenTexture, n_out2p0, n_out5p0);


// ColorParameter:7
	vec4 n_out7p0 = color;


// FloatParameter:8
	float n_out8p0 = color_weight;


// Mix:6
	vec4 n_out6p0 = mix(n_out4p0, n_out7p0, vec4(n_out8p0));


// Output:0
	COLOR.rgb = vec3(n_out6p0.xyz);


}
                             "   
     4D  �B#             $   
     p�  �A%            &   
     p�  �B'            (   
     �B  �B)            *   
     H�  �C+            ,   
     �C  �B-            .   
     H�  9D/            0   
     H�  kD1                                                                                                               RSRC