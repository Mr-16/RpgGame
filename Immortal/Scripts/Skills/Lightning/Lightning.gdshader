shader_type canvas_item;

uniform float thickness = 0.012;
uniform float glow_strength = 3.5;
uniform float speed = 3.5;
uniform float noise_scale = 10.0;
uniform float distortion = 0.2;
uniform float color_shift_speed = 1.5;
uniform float pulse_strength = 1.3;

/* 随机 */
float rand(vec2 n) {
    return fract(sin(dot(n, vec2(12.9898, 78.233))) * 43758.5453);
}

/* 噪声 */
float noise(vec2 p) {
    vec2 i = floor(p);
    vec2 f = fract(p);

    float a = rand(i);
    float b = rand(i + vec2(1.0, 0.0));
    float c = rand(i + vec2(0.0, 1.0));
    float d = rand(i + vec2(1.0, 1.0));

    vec2 u = f * f * (3.0 - 2.0 * f);
    return mix(a, b, u.x) +
           (c - a) * u.y * (1.0 - u.x) +
           (d - b) * u.x * u.y;
}

/* HSV → RGB */
vec3 hsv2rgb(vec3 c) {
    vec4 K = vec4(1.0, 2.0/3.0, 1.0/3.0, 3.0);
    vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

/* 闪电主干 */
float lightning(vec2 uv, float t, float scale) {
    float y = uv.y * noise_scale * scale;
    float offset = 0.0;

    offset += noise(vec2(y, t)) * distortion;
    offset += noise(vec2(y * 2.0, t * 1.7)) * distortion * 0.6;
    offset += noise(vec2(y * 4.0, t * 2.5)) * distortion * 0.3;

    float x = uv.x + offset;
    return smoothstep(thickness, 0.0, abs(x - 0.5));
}

void fragment() {
    vec2 uv = UV;
    float t = TIME * speed;

    /* 核心 & 外层 */
    float core = lightning(uv, t, 1.0);
    float outer = lightning(uv, t + 0.4, 1.3) * glow_strength;

    /* 脉冲闪烁 */
    float pulse = sin(TIME * 8.0 + uv.y * 12.0) * 0.5 + 0.5;
    pulse = mix(1.0, pulse * 1.8, pulse_strength);

    float alpha = clamp((core + outer) * pulse, 0.0, 1.0);

    /* 多彩流动颜色 */
    float hue = fract(uv.y * 1.5 + TIME * color_shift_speed);
    vec3 color = hsv2rgb(vec3(hue, 1.0, 1.0));

    /* 核心更白 */
    vec3 final_color = mix(color, vec3(1.0), core * 0.7);

    COLOR = vec4(final_color * alpha * 1.4, alpha);
}
