using Godot;
using RpgGame.Scripts.AttributeSystem;
using RpgGame.Scripts.Characters.Enemies;
using RpgGame.Scripts.Datas;
using RpgGame.Scripts.GameSystem;
using RpgGame.Scripts.InventorySystem;
using System;
using System.Collections.Generic;

using RpgGame.Scripts.LevelSystems;


namespace RpgGame.Scripts.Characters.Players
{
    public partial class Player : CharacterBase
    {
        public StateMachine Sm;

        
        [Export] 
        public ProgressBar StamPb;

        [Export]
        public AnimatedSprite2D Anim;


        [Export] float AtkRange = 50;
        public float AtkRangeSq;
        [Export] float AtkAngle = 160;


        [Export]
        public bool isMoveAtkEnable = true;

        [Export]
        public ProgressBar HealthPb;
        [Export]
        public ProgressBar ManaPb;
        [Export]
        public ProgressBar ExpPb;
        [Export]
        public Label ExpLb;
        [Export]
        public Label LevelLb;


        public ItemManager ItemManager;
        [Export] InventoryView InventoryView;//背包系统的大UI面板
        public override void _Ready()
        {
            GameManager.Instance().Player = this;
            Sm = new StateMachine(this);
            InitAttr();//属性
            InitSkillData();//技能
            InitLevelSytem();//初始化等级
            AtkRangeSq = AtkRange * AtkRange;

            
            
            ItemManager = new ItemManager(new Equipment(), new Inventory(30));
            InventoryView.Init(ItemManager);
            //ItemManager.EquipFromInv(5);//穿戴剑
            //ItemManager.EquipFromInv(10);//弓->剑
            //ItemManager.UnequipToInv(EquipType.Weapon);//卸下弓, 自动放在1
            //ItemManager.EquipFromInv(15);//穿戴甲
            //ItemManager.UnequipToInv(EquipType.Armor, 16);//卸甲, 指定16
            ItemManager.Inventory.AddItem(ItemFactory.Instance().Create(ItemId.HealthPotion, 15));
            ItemManager.Inventory.AddItem(ItemFactory.Instance().Create(ItemId.ManaPotion, 15));
            ItemManager.Inventory.AddItem(ItemFactory.Instance().Create(ItemId.Bow, 1));
            ItemManager.Inventory.AddItem(ItemFactory.Instance().Create(ItemId.Sword, 1));
            ItemManager.Inventory.AddItem(ItemFactory.Instance().Create(ItemId.Sword, 1));
            ItemManager.Inventory.AddItem(ItemFactory.Instance().Create(ItemId.Armor, 1));
            ItemManager.Inventory.AddItem(ItemFactory.Instance().Create(ItemId.Wd40, 15));
            ItemManager.Inventory.AddItem(ItemFactory.Instance().Create(ItemId.Wd40, 12));
            ItemManager.Inventory.AddItem(ItemFactory.Instance().Create(ItemId.Wd40, 11));
            ItemManager.Equipment.Equip(ItemFactory.Instance().Create(ItemId.Armor, 1));
            ItemManager.Equipment.Equip(ItemFactory.Instance().Create(ItemId.Sword, 1));
            //ItemManager.Inventory.RemoveItem(1, 100);
            //ItemManager.Inventory.RemoveItem(2, 100);

        }

        public override void _Process(double delta)
        {
            Sm.CurState.Update((float)delta);
            //GD.Print("curState" + Sm.curState);

            if (Input.IsActionJustPressed("Inventory"))
            {
                if (InventoryView.Visible == false)
                {
                    GD.Print("pause");
                    InventoryView.Visible = true;
                }
                else
                {
                    //恢复游戏
                    GD.Print("restart");
                    InventoryView.Visible = false;
                }
            }
        }
        public override void _PhysicsProcess(double delta)
        {
            Sm.CurState.FixedUpdate((float)delta);
            //GD.Print("Stamina" + curStamina);
            RegenHealth((float)delta);
            RegenEnergy((float)delta);

            UpdateUi((float)delta);
        }

        protected override void InitAttr()
        {
            AttrContainer.GetAttrValue(AttributeType.Def).BaseValue = 5;

            AttrContainer.GetAttrValue(AttributeType.HpRegen).BaseValue = 10;
            AttrContainer.GetAttrValue(AttributeType.EnergyRegen).BaseValue = 10;
            AttrContainer.GetAttrValue(AttributeType.MoveSpeed).BaseValue = 300;

            AttrContainer.GetAttrValue(AttributeType.MaxEnergy).BaseValue = 50;

            AttrContainer.GetAttrValue(AttributeType.Atk).BaseValue = 10;

            AttrContainer.GetAttrValue(AttributeType.MaxHp).BaseValue = 100;

            AttrContainer.RecalculateAllAttributes();

            CurHealth = AttrContainer.GetAttrValue(AttributeType.MaxHp).FinalValue;
            CurEnergy = AttrContainer.GetAttrValue(AttributeType.EnergyRegen).FinalValue;
        }

        public void Walk(Vector2 moveDir)
        {
            if (moveDir == Vector2.Zero) return;
            CurDir = moveDir;
            if (CurDir.X < 0) Anim.FlipH = true;
            else if (CurDir.X > 0) Anim.FlipH = false;
            Velocity = AttrContainer.GetAttrValue(AttributeType.MoveSpeed).FinalValue * moveDir;
            MoveAndSlide();
        }
        public void Run(Vector2 moveDir)
        {
            if (moveDir == Vector2.Zero) return;
            CurDir = moveDir;
            if (CurDir.X < 0) Anim.FlipH = true;
            else if(CurDir.X > 0) Anim.FlipH = false;
            Velocity = AttrContainer.GetAttrValue(AttributeType.MoveSpeed).FinalValue * 2 * moveDir;
            MoveAndSlide();
        }
        public void Roll()
        {
            Velocity = AttrContainer.GetAttrValue(AttributeType.MoveSpeed).FinalValue * 4 * CurDir;
            MoveAndSlide();
        }

        public void RegenHealth(float delta)
        {
            if (CurHealth < AttrContainer.GetAttrValue(AttributeType.MaxHp).FinalValue)
            {
                CurHealth += AttrContainer.GetAttrValue(AttributeType.HpRegen).FinalValue * delta;
            }
            else
            {
                CurHealth = AttrContainer.GetAttrValue(AttributeType.MaxHp).FinalValue;
            }
        }
        public void RegenEnergy(float delta)
        {
            if (CurEnergy < AttrContainer.GetAttrValue(AttributeType.MaxEnergy).FinalValue)
            {
                CurEnergy += AttrContainer.GetAttrValue(AttributeType.EnergyRegen).FinalValue * delta;
            }
            else
            {
                CurEnergy = AttrContainer.GetAttrValue(AttributeType.MaxEnergy).FinalValue;
            }
        }

        public void Atk()
        {
            List<Enemy> enemyList = GameManager.Instance().EnemyList;
            foreach (Enemy enemy in enemyList)
            {
                float disSq = GlobalPosition.DistanceSquaredTo(enemy.GlobalPosition);
                if (disSq > AtkRange * AtkRange)
                    continue;

                //扇形攻击判断
                Vector2 dirToEnemy = (enemy.GlobalPosition - GlobalPosition).Normalized();
                if (dirToEnemy.AngleTo(CurDir) > AtkAngle / 2)
                    continue;

                //enemy.TakeDmg(CalcPhyDamage(enemy.FinalAttr));
                enemy.TakeDmg(DamageCalculator.CalculateDamage(AttrContainer, enemy.AttrContainer));
            }
        }
        private float uiUpdateTimer = 0;
        private void UpdateUi(float delta)
        {
            uiUpdateTimer += delta;
            if (uiUpdateTimer < 0.1f) return;
            uiUpdateTimer = 0;
            //TODO : 更新ui
            HealthPb.MaxValue = AttrContainer.GetAttrValue(AttributeType.MaxHp).FinalValue;
            HealthPb.Value = CurHealth;
            ManaPb.MaxValue = AttrContainer.GetAttrValue(AttributeType.MaxEnergy).FinalValue;
            ManaPb.Value = CurEnergy;

            //经验
            //ExpLb.Text = $"{CurExp} / {ExpToNextLevel}";
            //ExpPb.Value = CurExp;
            //ExpPb.MaxValue = ExpToNextLevel;
            //LevelLb.Text = $"等级 : {Level}";
        }

        public Enemy GetClosestEnemy(float rangeSq)
        {
            Enemy tarEnmey = null;
            List<Enemy> enemyList = GameManager.Instance().EnemyList;
            float minDisSq = float.MaxValue;
            foreach (Enemy enmey in enemyList)
            {
                float curDisSq = GlobalPosition.DistanceSquaredTo(enmey.GlobalPosition);
                if (curDisSq > rangeSq) continue;
                if(curDisSq < minDisSq)
                {
                    minDisSq = curDisSq;
                    tarEnmey = enmey;
                }
            }
            return tarEnmey;
        }

        protected override void Die()
        {
            base.Die();
            Sm.ChangeState(Sm.DeathState);
        }

        #region 技能系统
        public void CastSkill(SkillType skillType)
        {
            //TODO : 根据技能类型和技能数据释放技能
            switch (skillType)
            {
                case SkillType.None:
                    return;
                case SkillType.FireBall:
                    FireBall();
                    return;
                case SkillType.IceSpike:
                    IceSpike();
                    return;
                case SkillType.Lightning:
                    Lightning();
                    break;
                case SkillType.DashAtk:
                    break;
                case SkillType.PowerUp:
                    break;
                case SkillType.AddHealth:
                    break;
                default:
                    break;
            }
        }

        private void FireBall()
        {
            SkillData data = GameManager.Instance().SkillDataMap[SkillType.FireBall];
            //范围内找到目标然后生成火球射过去
            Enemy tarEnmey = GetClosestEnemy(data.RangeSq);
            FireBall fireBall = FireBallScene.Instantiate<FireBall>();
            fireBall.GlobalPosition = GlobalPosition;
            if (tarEnmey == null) fireBall.Dir = CurDir;
            else
            {
                CurDir = (tarEnmey.GlobalPosition - GlobalPosition).Normalized();
                fireBall.Dir = CurDir;
            }

            GetTree().CurrentScene.AddChild(fireBall);
        }

        private void IceSpike()
        {
            float radius = 50f;

            for (int angle = 0; angle < 360; angle += 10)
            {
                IceSpike iceSpike = IceSpikeScene.Instantiate<IceSpike>();

                // 角度 → 弧度
                float rad = Mathf.DegToRad(angle);

                // 方向向量
                Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

                // 设置位置（环形）
                iceSpike.GlobalPosition = GlobalPosition + dir * radius;

                // 设置朝向（如果冰刺有方向）
                iceSpike.Rotation = rad;

                // 如果冰刺需要知道移动方向
                iceSpike.Dir = dir; // 前提：你在 IceSpike.cs 里定义了

                GetTree().CurrentScene.AddChild(iceSpike);
            }
        }
        private void Lightning()
        {
            SkillData data = GameManager.Instance().SkillDataMap[SkillType.Lightning];
            Enemy tarEnmey = GetClosestEnemy(data.RangeSq);
            if (tarEnmey == null) return;
            Lightning lightning = LightningScene.Instantiate<Lightning>();
            lightning.GlobalPosition = GlobalPosition;
            lightning.curTarget = tarEnmey;

            GetTree().CurrentScene.AddChild(lightning);
        }

        public int curSkillTypeIndex = -1;//当前技能索引, 取值 : 012, 每次进入技能状态都要根据索引在技能list里找到具体技能
        public List<SkillType> SkillTypeList = new List<SkillType>(3);
        private void InitSkillData()
        {
            //暂时随便填值, 后期加入存档后从Json里反序列化出来
            SkillTypeList.Add(SkillType.FireBall);
            SkillTypeList.Add(SkillType.Lightning);
            SkillTypeList.Add(SkillType.IceSpike);
        }

        [Export]
        public PackedScene FireBallScene;
        [Export]
        public PackedScene IceSpikeScene;
        [Export]
        public PackedScene LightningScene;


        #endregion

        #region 等级系统
        public LevelSystem LevelSystem;
        private void InitLevelSytem()
        {
            LevelSystem = new LevelSystem();
            LevelSystem.TakeExped += LevelSystem_TakeExped;
            LevelSystem.LevelUped += LevelSystem_LevelUped;
        }
        private void LevelSystem_TakeExped(WuXingType type, int exp)
        {
            FloatText expText = FloatTextLabel.Instantiate<FloatText>();
            expText.GlobalPosition = GlobalPosition;
            expText.Init(exp.ToString(), new Color(0, 1, 0, 1), 0.5f);
            GetTree().CurrentScene.AddChild(expText);
        }
        private void LevelSystem_LevelUped(WuXingType type, int level)
        {
            FloatText levelText = FloatTextLabel.Instantiate<FloatText>();
            levelText.GlobalPosition = GlobalPosition;
            string str = "";
            switch(type)
            { 
                case WuXingType.Metal:
                    str = "[金灵根]";
                    AttrContainer.GetAttrValue(AttributeType.Def).BaseValue += 2;
                    break;
                case WuXingType.Wood:
                    str = "[木灵根]";
                    AttrContainer.GetAttrValue(AttributeType.HpRegen).BaseValue += 1;
                    AttrContainer.GetAttrValue(AttributeType.EnergyRegen).BaseValue += 1;
                    AttrContainer.GetAttrValue(AttributeType.MoveSpeed).BaseValue += 0.01f;
                    break;
                case WuXingType.Water:
                    str = "[水灵根]";
                    AttrContainer.GetAttrValue(AttributeType.MaxEnergy).BaseValue += 10f;
                    break;
                case WuXingType.Fire:
                    str = "[火灵根]";
                    AttrContainer.GetAttrValue(AttributeType.Atk).BaseValue += 3f;
                    break;
                case WuXingType.Earth:
                    str = "[土灵根]";
                    AttrContainer.GetAttrValue(AttributeType.MaxHp).BaseValue += 20f;
                    break;
            }
            levelText.Init(str + "升至" + level + "级!!", new Color(0, 0, 1, 1), 2);
            levelText.ZIndex = 120;
            GetTree().CurrentScene.AddChild(levelText);


        }
        #endregion


        public override void _Draw()
        {
            //DrawCircle(Vector2.Zero, AtkRange, new Color(0, 1, 0, 0.3f));
            //// 扇形弧长
            //float startAngle = CurDir.Angle() - Mathf.DegToRad(AtkAngle) / 2;
            //float endAngle = CurDir.Angle() + Mathf.DegToRad(AtkAngle) / 2;

            //int points = 64; // 圆弧分段数
            //DrawArc(Vector2.Zero, AtkRange, startAngle, endAngle, points, new Color(1, 0, 0, 0.3f), 2);

            //// 可选：填充扇形（Debug用）
            //DrawLine(Vector2.Zero, Vector2.Zero + CurDir.Rotated(-Mathf.DegToRad(AtkAngle) / 2) * AtkRange, Colors.Red, 1);
            //DrawLine(Vector2.Zero, Vector2.Zero + CurDir.Rotated(Mathf.DegToRad(AtkAngle) / 2) * AtkRange, Colors.Red, 1);
        }
    }
}
