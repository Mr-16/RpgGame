shader_type canvas_item;

uniform float time_speed : hint_range(0.1, 5.0) = 1.5;
uniform float noise_scale : hint_range(1.0, 10.0) = 4.0;
uniform float glow_strength : hint_range(0.0, 3.0) = 1.2;

void fragment()
{
    vec2 uv = UV * 2.0 - 1.0;
    float dist = length(uv);

    // 圆形遮罩
    float mask = smoothstep(0.6, 0.45, dist);

    // 时间扰动
    float t = TIME * time_speed;

    // 简单噪声（火焰扰动）
    float noise =
        sin((uv.x + t) * noise_scale) *
        cos((uv.y - t) * noise_scale);

    noise = noise * 0.15;

    // 火焰边缘波动
    float flame = smoothstep(0.5 + noise, 0.3, dist);

    // 颜色渐变（内黄 -> 外红）
    vec3 inner_color = vec3(1.0, 0.9, 0.3);
    vec3 outer_color = vec3(1.0, 0.2, 0.05);
    vec3 color = mix(outer_color, inner_color, flame);

    // 发光
    float glow = smoothstep(0.8, 0.2, dist) * glow_strength;

    COLOR = vec4(color * (flame + glow), mask);
}
