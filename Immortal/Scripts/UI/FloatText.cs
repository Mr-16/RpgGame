using Godot;

public partial class FloatText : Node2D
{
    [Export] private Label FloatTextLabel;
    [Export] public float LifeTime = 0.8f;          // 总生命周期，稍延长点更舒服
    [Export] public float FloatSpeed = 80f;         // 上浮速度（像素/秒），调低一点让动画更明显
    [Export] public float InitialPopScale = 2.0f;   // 初始弹出缩放（暴击时可更大）
    [Export] public float ShakeAmount = 10f;        // 摇晃幅度
    [Export] public Vector2 RandomOffset = new(20, 10); // 初始随机偏移加大

    private Tween _tween;

    public override void _Ready()
    {
        // 初始随机偏移
        Position += new Vector2(
            (float)GD.RandRange(-RandomOffset.X, RandomOffset.X),
            (float)GD.RandRange(-RandomOffset.Y, RandomOffset.Y)
        );

        FloatTextLabel.PivotOffset = FloatTextLabel.Size / 2; // 以中心为轴心缩放/旋转
        FloatTextLabel.Position = -FloatTextLabel.PivotOffset;


    }

    public void Init(string text, Color color, float scaleMultiplier = 1f, bool isCritical = false)
    {
        _tween = CreateTween();
        _tween.SetParallel(true); // 允许同时执行多个动画
        FloatTextLabel.Text = text;
        FloatTextLabel.Modulate = color;

        // 基础缩放 + 暴击加成
        float baseScale = scaleMultiplier * (isCritical ? 1.5f : 1f);
        FloatTextLabel.Scale = Vector2.One * 0.1f; // 从很小开始弹出

        // 1. 缩放弹出：快速变大 → 轻微超大 → 恢复正常
        _tween.TweenProperty(FloatTextLabel, "scale", Vector2.One * (baseScale * InitialPopScale), 0.15f)
              .SetTrans(Tween.TransitionType.Back)
              .SetEase(Tween.EaseType.Out);
        _tween.TweenProperty(FloatTextLabel, "scale", Vector2.One * baseScale, 0.2f)
              .SetDelay(0.15f)
              .SetTrans(Tween.TransitionType.Elastic)
              .SetEase(Tween.EaseType.Out);

        // 2. 上浮 + 轻微抛物线侧移（摇晃感）
        Vector2 targetPos = Position + Vector2.Up * FloatSpeed * LifeTime;
        if (isCritical)
            targetPos += Vector2.Up * 30; // 暴击浮得更高

        // 主上浮
        _tween.TweenProperty(this, "position", targetPos, LifeTime)
              .SetTrans(Tween.TransitionType.Cubic)
              .SetEase(Tween.EaseType.Out);

        // 额外侧向摇晃（正弦波）
        float shakeStrength = ShakeAmount * (isCritical ? 1.5f : 1f);
        _tween.TweenMethod(Callable.From<float>(ShakeUpdate), 0f, 1f, LifeTime);

        // 3. 淡出（从0.3秒后开始淡出，更持久）
        _tween.TweenProperty(FloatTextLabel, "modulate:a", 1f, LifeTime * 0.4f).SetDelay(LifeTime * 0.6f);
        _tween.TweenProperty(FloatTextLabel, "modulate:a", 0f, LifeTime * 0.4f).SetDelay(LifeTime * 0.6f);

        // 结束后自动释放
        _tween.Finished += QueueFree;
    }

    // 摇晃回调：用sin波让文字左右轻晃
    private void ShakeUpdate(float progress)
    {
        float shake = Mathf.Sin(progress * Mathf.Pi * 8) * ShakeAmount * (1 - progress); // 前期晃得多，后期减弱
        Position += new Vector2(shake, 0);
    }
}



//public partial class FloatText : Node2D
//{
//    [Export] private Label floatTextLabel; // 变量名规范优化
//    [Export] public float lifeTime = 0.8f; // 延长一点生命周期，给动画更多空间
//    [Export] public float baseFloatSpeed = 400f; // 基础上浮速度
//    [Export] public Vector2 randomOffset = new(15, 8); // 增大随机偏移，更分散
//    [Export] public float maxRotateAngle = 8f; // 最大旋转角度（轻微旋转增加动感）
//    [Export] public float scalePulseFactor = 1.3f; // 初始缩放脉冲倍数
//    [Export] public float colorBloomFactor = 1.2f; // 初始颜色亮度增益

//    private Tween _mainTween;

//    public override void _Ready()
//    {
//        // 安全校验：避免Label未赋值导致空引用
//        if (floatTextLabel == null)
//        {
//            floatTextLabel = GetNode<Label>("Label");
//            GD.PushWarning("FloatTextLabel未手动赋值，自动获取Label节点");
//        }

//        // 初始随机偏移（保留原有逻辑，增大范围）
//        Position += new Vector2(
//            (float)GD.RandRange(-randomOffset.X, randomOffset.X),
//            (float)GD.RandRange(-randomOffset.Y, randomOffset.Y)
//        );

//        // 初始随机旋转（左右轻微摆动）
//        float randomRotate = (float)GD.RandRange(-maxRotateAngle, maxRotateAngle);
//        RotationDegrees = randomRotate;

//        // 初始化Label锚点和居中（修复可能的偏移问题）
//        floatTextLabel.HorizontalAlignment = HorizontalAlignment.Center;
//        floatTextLabel.VerticalAlignment = VerticalAlignment.Center;
//        floatTextLabel.PivotOffset = floatTextLabel.Size / 2;
//        floatTextLabel.Position = Vector2.Zero; // 直接居中，简化偏移逻辑
//    }

//    // 初始化文本、颜色、缩放（核心入口）
//    public void Init(string text, Color color, float baseScale = 1f)
//    {
//        floatTextLabel.Text = text;
//        floatTextLabel.Scale = Vector2.One * baseScale;
//        floatTextLabel.Modulate = color;

//        // 启动打击感动画序列
//        PlayImpactAnimation(color, baseScale);
//    }

//    // 核心：打击感动画序列（缩放脉冲+缓动上浮+旋转回弹+透明度/颜色淡出）
//    private void PlayImpactAnimation(Color baseColor, float baseScale)
//    {
//        // 停止旧动画，避免叠加
//        if (_mainTween != null && _mainTween.IsRunning())
//        {
//            _mainTween.Kill();
//        }

//        _mainTween = CreateTween();
//        _mainTween.SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Cubic); // 缓动曲线：先快后慢，更自然

//        // 1. 缩放脉冲（先放大再回弹，模拟冲击感）
//        _mainTween.TweenProperty(
//            floatTextLabel,
//            "scale",
//            Vector2.One * baseScale * scalePulseFactor,
//            lifeTime * 0.15f // 快速放大（15%生命周期）
//        );
//        _mainTween.TweenProperty(
//            floatTextLabel,
//            "scale",
//            Vector2.One * baseScale,
//            lifeTime * 0.2f // 回弹到原大小（20%生命周期）
//        ).SetTrans(Tween.TransitionType.Elastic); // 弹性回弹，更有张力

//        // 2. 缓动上浮（随机速度偏移，避免千篇一律）
//        float randomSpeed = (float)GD.RandRange(0.8f, 1.2f); // 速度随机波动±20%
//        _mainTween.Parallel().TweenProperty(
//            this,
//            "position",
//            Position + Vector2.Up * baseFloatSpeed * randomSpeed * 0.8f, // 上浮距离
//            lifeTime
//        );

//        // 3. 旋转回弹（轻微回正，增加动态）
//        _mainTween.Parallel().TweenProperty(
//            this,
//            "rotation_degrees",
//            0, // 回正到0角度
//            lifeTime * 0.6f // 60%生命周期内回正
//        ).SetEase(Tween.EaseType.InOut);

//        // 4. 颜色亮度脉冲+淡出（初始更亮，再渐变）
//        Color bloomColor = new Color(
//            Mathf.Clamp(baseColor.R * colorBloomFactor, 0f, 1f),
//            Mathf.Clamp(baseColor.G * colorBloomFactor, 0f, 1f),
//            Mathf.Clamp(baseColor.B * colorBloomFactor, 0f, 1f),
//            baseColor.A
//        );
//        _mainTween.Parallel()
//            .TweenProperty(floatTextLabel, "modulate", bloomColor, lifeTime * 0.1f); // 快速提亮


//        // 动画结束后销毁节点
//        _mainTween.Finished += QueueFree;
//    }

//    // 防止节点被提前销毁时动画报错
//    public override void _ExitTree()
//    {
//        if (_mainTween != null)
//        {
//            _mainTween.Kill();
//        }
//    }
//}