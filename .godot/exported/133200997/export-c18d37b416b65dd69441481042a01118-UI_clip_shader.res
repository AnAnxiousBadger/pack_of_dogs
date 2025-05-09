RSRC                    VisualShader            ��������                                            R      resource_local_to_scene    resource_name    output_port_for_preview    default_input_values    expanded_output_ports    linked_parent_graph_frame    parameter_name 
   qualifier    hint    min    max    step    default_value_enabled    default_value    script    type 	   function 
   condition    input_name    op_type 	   operator    texture_type    color_default    texture_filter    texture_repeat    texture_source    source    texture    code    graph_offset    mode    modes/blend    flags/skip_vertex_transform    flags/unshaded    flags/light_only    flags/world_vertex_coords    nodes/vertex/0/position    nodes/vertex/connections    nodes/fragment/0/position    nodes/fragment/2/node    nodes/fragment/2/position    nodes/fragment/4/node    nodes/fragment/4/position    nodes/fragment/5/node    nodes/fragment/5/position    nodes/fragment/6/node    nodes/fragment/6/position    nodes/fragment/7/node    nodes/fragment/7/position    nodes/fragment/8/node    nodes/fragment/8/position    nodes/fragment/9/node    nodes/fragment/9/position    nodes/fragment/10/node    nodes/fragment/10/position    nodes/fragment/11/node    nodes/fragment/11/position    nodes/fragment/12/node    nodes/fragment/12/position    nodes/fragment/13/node    nodes/fragment/13/position    nodes/fragment/14/node    nodes/fragment/14/position    nodes/fragment/15/node    nodes/fragment/15/position    nodes/fragment/connections    nodes/light/0/position    nodes/light/connections    nodes/start/0/position    nodes/start/connections    nodes/process/0/position    nodes/process/connections    nodes/collide/0/position    nodes/collide/connections    nodes/start_custom/0/position    nodes/start_custom/connections     nodes/process_custom/0/position !   nodes/process_custom/connections    nodes/sky/0/position    nodes/sky/connections    nodes/fog/0/position    nodes/fog/connections        -   local://VisualShaderNodeFloatParameter_arnuw �
      &   local://VisualShaderNodeCompare_j8ilx E      $   local://VisualShaderNodeInput_72vij y      %   local://VisualShaderNodeSwitch_4r1d3 �      &   local://VisualShaderNodeCompare_gv8nm �      %   local://VisualShaderNodeSwitch_e0hv5       &   local://VisualShaderNodeFloatOp_wq6u4 �      &   local://VisualShaderNodeCompare_odm4f �      %   local://VisualShaderNodeSwitch_cgnvv       %   local://VisualShaderNodeSwitch_k47xx ;      1   local://VisualShaderNodeTexture2DParameter_d4pv2 �      &   local://VisualShaderNodeTexture_sgiwr       $   local://VisualShaderNodeInput_rnhrs <      $   res://Assets/UI/UI_clip_shader.tres x         VisualShaderNodeFloatParameter             vertical_clip          	        ��                  VisualShaderNodeCompare                      VisualShaderNodeInput                             uv          VisualShaderNodeSwitch             VisualShaderNodeCompare                      VisualShaderNodeSwitch                                                                      VisualShaderNodeFloatOp                                      �?         VisualShaderNodeCompare                      VisualShaderNodeSwitch             VisualShaderNodeSwitch                                                                   #   VisualShaderNodeTexture2DParameter             Texture2DParameter                   VisualShaderNodeTexture                      VisualShaderNodeInput          
   screen_uv          VisualShader           �  shader_type canvas_item;
render_mode blend_mix;

uniform sampler2D Texture2DParameter : hint_screen_texture;
uniform float vertical_clip : hint_range(-1, 1) = 0;



void fragment() {
// Input:15
	vec2 n_out15p0 = SCREEN_UV;


	vec4 n_out14p0;
// Texture2D:14
	n_out14p0 = texture(Texture2DParameter, n_out15p0);


// FloatParameter:2
	float n_out2p0 = vertical_clip;


// Compare:4
	float n_in4p1 = 0.00000;
	bool n_out4p0 = n_out2p0 >= n_in4p1;


	float n_out6p0;
// Switch:6
	float n_in6p2 = 0.00000;
	n_out6p0 = mix(n_in6p2, n_out2p0, float(n_out4p0));


// Input:5
	vec2 n_out5p0 = UV;
	float n_out5p2 = n_out5p0.g;


// Compare:7
	bool n_out7p0 = n_out6p0 < n_out5p2;


	int n_out8p0;
// Switch:8
	int n_in8p1 = 1;
	int n_in8p2 = 0;
	if (n_out7p0) {
		n_out8p0 = n_in8p1;
	} else {
		n_out8p0 = n_in8p2;
	}


// FloatOp:9
	float n_in9p1 = 1.00000;
	float n_out9p0 = n_out2p0 + n_in9p1;


// Compare:10
	bool n_out10p0 = n_out9p0 > n_out5p2;


	int n_out12p0;
// Switch:12
	int n_in12p1 = 1;
	int n_in12p2 = 0;
	if (n_out10p0) {
		n_out12p0 = n_in12p1;
	} else {
		n_out12p0 = n_in12p2;
	}


	float n_out11p0;
// Switch:11
	n_out11p0 = mix(float(n_out12p0), float(n_out8p0), float(n_out4p0));


// Output:0
	COLOR.rgb = vec3(n_out14p0.xyz);
	COLOR.a = n_out11p0;


}
          "          &   
    ��D  �B'             (   
     \�  �A)            *   
     �C  �A+            ,   
     �C  �C-            .   
     D  pC/            0   
     CD  �C1            2   
     uD  �C3            4   
     4C   D5            6   
     HD   D7            8   
     �D  C9         	   :   
     D   D;         
   <   
     aD   �=            >   
    ��D  H�?            @   
     zD  ��A       D                                                                                           	       	       
             
                                 
                                                                                         RSRC