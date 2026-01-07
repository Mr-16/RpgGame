shader_type canvas_item;

uniform vec4 ice_color : source_color = vec4(0.6, 0.85, 1.0, 1.0);
uniform float sharpness = 3.0;     // 冰刺尖锐度
uniform float noise_strength = 0.15;
uniform float glow_strength = 0.6;
uniform float time_scale = 1.5;

void fragment() {
    vec2 uv = UV * 2.0 - 1.0; // 转成 -1 ~ 1
    uv.y += 0.5;             // 让冰刺往上

    // 冰刺主体形状（三角）
    float spike = 1.0 - abs(uv.x) * sharpness - uv.y;
    
    // 噪声（模拟冰内部折射）
    float noise = sin((uv.y + TIME * time_scale) * 12.0) * 
                  sin((uv.x + TIME) * 18.0);
    spike += noise * noise_strength;

    // 边缘平滑
    float alpha = smoothstep(0.0, 0.15, spike);

    // 中心高亮
    float glow = smoothstep(0.4, 1.0, spike) * glow_strength;

    vec3 final_color = ice_color.rgb;
    final_color += glow;

    COLOR = vec4(final_color, alpha * ice_color.a);
}
