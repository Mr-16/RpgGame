using Godot;
using RpgGame.Scripts.Characters.Enemies;
using RpgGame.Scripts.Datas;
using RpgGame.Scripts.GameSystem;
using RpgGame.Scripts.InventorySystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;


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



        public Inventory Inventory;
        public Equipment Equipment;
        public ItemManager ItemManager;
        public override void _Ready()
        {
            GameManager.Instance().Player = this;
            Sm = new StateMachine(this);
            InitAttribute();//属性
            InitSkillData();//技能
            InitLevel();//初始化等级
            AtkRangeSq = AtkRange * AtkRange;

            Equipment = new Equipment();
            Inventory = new Inventory(30);
            ItemManager = new ItemManager(Equipment, Inventory);
            
            Inventory.ItemList[0] = new ItemInstance(ItemDatabase.Instance().GetItemData(ItemId.ManaPotion), 100); 
            Inventory.ItemList[5] = new EquipInstance(ItemDatabase.Instance().GetItemData(ItemId.Sword), 1, 1);
            Inventory.ItemList[10] = new EquipInstance(ItemDatabase.Instance().GetItemData(ItemId.Bow), 1, 3);
            Inventory.ItemList[15] = new EquipInstance(ItemDatabase.Instance().GetItemData(ItemId.Armor), 1, 10);

            ItemManager.EquipFromInv(5);//穿戴剑
            ItemManager.EquipFromInv(10);//弓->剑
            ItemManager.UnequipToInv(EquipType.Weapon);//卸下弓, 自动放在1
            ItemManager.EquipFromInv(15);//穿戴甲
            ItemManager.UnequipToInv(EquipType.Armor, 16);//卸甲, 指定16


        }

        public override void _Process(double delta)
        {
            Sm.CurState.Update((float)delta);
            //GD.Print("curState" + Sm.curState);

            if (Input.IsActionJustPressed("Inventory"))
            {
                //if (InventoryView.Visible == false)
                //{
                //    GD.Print("pause");
                //    InventoryView.Visible = true;
                //}
                //else
                //{
                //    //恢复游戏
                //    GD.Print("restart");
                //    InventoryView.Visible = false;
                //}
            }
        }
        public override void _PhysicsProcess(double delta)
        {
            Sm.CurState.FixedUpdate((float)delta);
            //GD.Print("Stamina" + curStamina);
            RegenHealth((float)delta);
            RegenMana((float)delta);

            UpdateUi((float)delta);
        }

        //public override void _Input(InputEvent e)
        //{
            
        //}

        private void InitAttribute()
        {
            //初始化玩家的属性
            //基础属性直接赋值(后面有存档了改成在json里读)
            //最终属性根据装备等加成计算
            BaseAttr.MaxHealth = 100;
            BaseAttr.HealthRegen = 3;
            BaseAttr.MaxMana = 1000;
            BaseAttr.ManaRegen = 3;
            BaseAttr.MaxStam = 200;
            BaseAttr.StamRegen = 300;//每秒恢复量
            BaseAttr.MoveSpeed = 300;
            BaseAttr.RollSpeed = 1000;
            //攻击属性
            BaseAttr.AtkSpeed = 0.5f;
            BaseAttr.PhyAtk = 50;
            BaseAttr.PhyPen = 0.2f;
            BaseAttr.PhyPenFlat = 500;
            BaseAttr.MagAtk = 10;
            BaseAttr.MagPen = 0.1f;
            BaseAttr.MagPenFlat = 10;
            BaseAttr.CritRate = 0.1f;
            BaseAttr.CritMult = 2;
            //防御属性
            BaseAttr.PhyDef = 200;
            BaseAttr.MagDef = 200;
            //特殊属性
            BaseAttr.LifeSteal = 0;

            //FinalAttr.MaxHealth = BaseAttr.MaxHealth + Weapon.BonusAttr.MaxHealth + Helmet.BonusAttr.MaxHealth + Armor.BonusAttr.MaxHealth + Boot.BonusAttr.MaxHealth;
            //FinalAttr.HealthRegen = BaseAttr.HealthRegen + Weapon.BonusAttr.HealthRegen + Helmet.BonusAttr.HealthRegen + Armor.BonusAttr.HealthRegen + Boot.BonusAttr.HealthRegen;
            //FinalAttr.MaxMana = BaseAttr.MaxMana + Weapon.BonusAttr.MaxMana + Helmet.BonusAttr.MaxMana + Armor.BonusAttr.MaxMana + Boot.BonusAttr.MaxMana;
            //FinalAttr.ManaRegen = BaseAttr.ManaRegen + Weapon.BonusAttr.ManaRegen + Helmet.BonusAttr.ManaRegen + Armor.BonusAttr.ManaRegen + Boot.BonusAttr.ManaRegen;
            //FinalAttr.MaxStam = BaseAttr.MaxStam + Weapon.BonusAttr.MaxStam + Helmet.BonusAttr.MaxStam + Armor.BonusAttr.MaxStam + Boot.BonusAttr.MaxStam;
            //FinalAttr.StamRegen = BaseAttr.StamRegen + Weapon.BonusAttr.StamRegen + Helmet.BonusAttr.StamRegen + Armor.BonusAttr.StamRegen + Boot.BonusAttr.StamRegen;
            //FinalAttr.MoveSpeed = BaseAttr.MoveSpeed + Weapon.BonusAttr.MoveSpeed + Helmet.BonusAttr.MoveSpeed + Armor.BonusAttr.MoveSpeed + Boot.BonusAttr.MoveSpeed;
            //FinalAttr.RollSpeed = BaseAttr.RollSpeed + Weapon.BonusAttr.RollSpeed + Helmet.BonusAttr.RollSpeed + Armor.BonusAttr.RollSpeed + Boot.BonusAttr.RollSpeed;
            ////攻击属性
            //FinalAttr.AtkSpeed = BaseAttr.AtkSpeed + Weapon.BonusAttr.AtkSpeed + Helmet.BonusAttr.AtkSpeed + Armor.BonusAttr.AtkSpeed + Boot.BonusAttr.AtkSpeed;
            //FinalAttr.PhyAtk = BaseAttr.PhyAtk + Weapon.BonusAttr.PhyAtk + Helmet.BonusAttr.PhyAtk + Armor.BonusAttr.PhyAtk + Boot.BonusAttr.PhyAtk;
            //FinalAttr.PhyPen = BaseAttr.PhyPen + Weapon.BonusAttr.PhyPen + Helmet.BonusAttr.PhyPen + Armor.BonusAttr.PhyPen + Boot.BonusAttr.PhyPen;
            //FinalAttr.PhyPenFlat = BaseAttr.PhyPenFlat + Weapon.BonusAttr.PhyPenFlat + Helmet.BonusAttr.PhyPenFlat + Armor.BonusAttr.PhyPenFlat + Boot.BonusAttr.PhyPenFlat;
            //FinalAttr.MagAtk = BaseAttr.MagAtk + Weapon.BonusAttr.MagAtk + Helmet.BonusAttr.MagAtk + Armor.BonusAttr.MagAtk + Boot.BonusAttr.MagAtk;
            //FinalAttr.MagPen = BaseAttr.MagPen + Weapon.BonusAttr.MagPen + Helmet.BonusAttr.MagPen + Armor.BonusAttr.MagPen + Boot.BonusAttr.MagPen;
            //FinalAttr.MagPenFlat = BaseAttr.MagPenFlat + Weapon.BonusAttr.MagPenFlat + Helmet.BonusAttr.MagPenFlat + Armor.BonusAttr.MagPenFlat + Boot.BonusAttr.MagPenFlat;
            //FinalAttr.CritRate = BaseAttr.CritRate + Weapon.BonusAttr.CritRate + Helmet.BonusAttr.CritRate + Armor.BonusAttr.CritRate + Boot.BonusAttr.CritRate;
            //FinalAttr.CritMult = BaseAttr.CritMult + Weapon.BonusAttr.CritMult + Helmet.BonusAttr.CritMult + Armor.BonusAttr.CritMult + Boot.BonusAttr.CritMult;
            ////防御属性
            //FinalAttr.PhyDef = BaseAttr.PhyDef + Weapon.BonusAttr.PhyDef + Helmet.BonusAttr.PhyDef + Armor.BonusAttr.PhyDef + Boot.BonusAttr.PhyDef;
            //FinalAttr.MagDef = BaseAttr.MagDef + Weapon.BonusAttr.MagDef + Helmet.BonusAttr.MagDef + Armor.BonusAttr.MagDef + Boot.BonusAttr.MagDef;
            ////特殊属性
            //FinalAttr.LifeSteal = BaseAttr.LifeSteal + Weapon.BonusAttr.LifeSteal + Helmet.BonusAttr.LifeSteal + Armor.BonusAttr.LifeSteal + Boot.BonusAttr.LifeSteal;

            CurHealth = FinalAttr.MaxHealth;
            CurMana = FinalAttr.MaxMana;
            CurStam = FinalAttr.MaxStam;
        }

        public void Walk(Vector2 moveDir)
        {
            if (moveDir == Vector2.Zero) return;
            CurDir = moveDir;
            if (CurDir.X < 0) Anim.FlipH = true;
            else if (CurDir.X > 0) Anim.FlipH = false;
            Velocity = FinalAttr.MoveSpeed * moveDir;
            MoveAndSlide();
        }
        public void Run(Vector2 moveDir)
        {
            if (moveDir == Vector2.Zero) return;
            CurDir = moveDir;
            if (CurDir.X < 0) Anim.FlipH = true;
            else if(CurDir.X > 0) Anim.FlipH = false;
            Velocity = 2 * FinalAttr.MoveSpeed * moveDir;
            MoveAndSlide();
        }
        public void Roll()
        {
            Velocity = FinalAttr.RollSpeed * CurDir;
            MoveAndSlide();
        }

        public void RegenHealth(float delta)
        {
            if (CurHealth < FinalAttr.MaxHealth)
            {
                CurHealth += FinalAttr.HealthRegen * delta;
            }
            else
            {
                CurHealth = FinalAttr.MaxHealth;
            }
        }
        public void RegenMana(float delta)
        {
            if (CurMana < FinalAttr.MaxMana)
            {
                CurMana += FinalAttr.ManaRegen * delta;
            }
            else
            {
                CurMana = FinalAttr.MaxMana;
            }
        }
        public void RegenStam(float delta)
        {
            if (CurStam < FinalAttr.MaxStam)
            {
                CurStam += FinalAttr.StamRegen * delta;
            }
            else
            {
                CurStam = FinalAttr.MaxStam;
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

                enemy.TakeDmg(CalcPhyDamage(enemy.FinalAttr));
            }
        }
        private float uiUpdateTimer = 0;
        private void UpdateUi(float delta)
        {
            uiUpdateTimer += delta;
            if (uiUpdateTimer < 0.1f) return;
            uiUpdateTimer = 0;
            //TODO : 更新ui
            StamPb.MaxValue = FinalAttr.MaxStam;
            StamPb.Value = CurStam;
            HealthPb.MaxValue = FinalAttr.MaxHealth;
            HealthPb.Value = CurHealth;
            ManaPb.MaxValue = FinalAttr.MaxMana;
            ManaPb.Value = CurMana;

            //经验
            ExpLb.Text = $"{CurExp} / {ExpToNextLevel}";
            ExpPb.Value = CurExp;
            ExpPb.MaxValue = ExpToNextLevel;
            LevelLb.Text = $"等级 : {Level}";

            //for(int i = 0; i < InventoryCapacity; i++)
            //{
            //    ItemSlot slot = ItemSlot.Instantiate<ItemSlot>();
            //    slot.IconTr.Texture = null;
            //    slot.CountLb.Text = "";
            //    //ItemView.InventoryGc.AddChild(slot);
            //}
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
            FireBall fireBall =  FireBallScene.Instantiate<FireBall>();
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




        #region 等级系统
        public int CurExp;
        public int ExpToNextLevel;
        public event Action<int> LevelUped;
        public void AddExp(int exp)
        {
            CurExp += exp;
            FloatText expText = FloatTextLabel.Instantiate<FloatText>();
            expText.GlobalPosition = GlobalPosition;
            expText.Init(exp.ToString(), new Color(0, 1, 0, 1), 0.5f);
            GetTree().CurrentScene.AddChild(expText);
            while (CurExp >= ExpToNextLevel)
            {
                CurExp -= ExpToNextLevel;
                Level++;
                ExpToNextLevel = NeedExp(Level);
                LevelUped?.Invoke(Level);
                GD.Print("Level : " + Level);
                FloatText levelText = FloatTextLabel.Instantiate<FloatText>();
                levelText.GlobalPosition = GlobalPosition;
                levelText.Init($"LEVEL UP!! 当前等级 : {Level}", new Color(0, 0, 1, 1), 2);
                levelText.ZIndex = 120;
                GetTree().CurrentScene.AddChild(levelText);
            }
        }
        public static int NeedExp(int level)
        {
            return (int)(100 * Math.Pow(level, 1.5));
        }
        private void InitLevel()
        {
            //Json序列化出来
            Level = 1;
            ExpToNextLevel = NeedExp(Level);
            CurExp = 0;
        }
        #endregion


        #region 库存系统
        //ItemInstance item1;
        //ItemInstance item2;
        //ItemInstance item3;
        //[Export] public PackedScene ItemSlot;

        //public List<ItemInstance> ItemInstanceList = new List<ItemInstance>();
        //public Dictionary<ItemInstance, ItemSlot> InstanceSlotMap = new Dictionary<ItemInstance, ItemSlot>();
        //public int InventoryCapacity = 30;

        //public bool AddItem(ItemData data, int quantity = 1)
        //{
        //    if (data.MaxStack > 1)
        //    {
        //        // 尝试堆叠
        //        ItemInstance existedItemInstance = ItemInstanceList.Find(i => i.ItemData.Id == data.Id && i.Count < data.MaxStack);
        //        if(existedItemInstance != null)
        //        {
        //            int space = data.MaxStack - existedItemInstance.Count;
        //            int toAdd = Mathf.Min(space, quantity);
        //            existedItemInstance.Count += toAdd;
        //            quantity -= toAdd;
        //            InstanceSlotMap[existedItemInstance].CountLb.Text = existedItemInstance.Count.ToString();
        //        }
               
        //    }
        //    while (quantity > 0)
        //    {
        //        if (ItemInstanceList.Count >= InventoryCapacity)
        //            return false; // 背包已满
        //        int toAdd = Mathf.Min(data.MaxStack, quantity);
        //        ItemInstance newItemInstance = new ItemInstance(data, toAdd);
        //        ItemInstanceList.Add(newItemInstance);
        //        quantity -= toAdd;
        //        //ui放入槽中
        //        ItemSlot slot = ItemSlot.Instantiate<ItemSlot>();
        //        slot.IconTr.Texture = GD.Load<Texture2D>(data.IconPath);
        //        slot.CountLb.Text = newItemInstance.Count.ToString();
        //        //ItemView.InventoryGc.AddChild(slot);
        //        InstanceSlotMap[newItemInstance] = slot;
        //    }
        //    return true;
        //}

        // 移除物品
        //public bool RemoveItem(int itemId, int quantity = 1)
        //{
        //    ItemInstance instance = ItemInstanceList.Find(i => i.ItemData.Id == itemId);
        //    if (instance == null)
        //        return false;

        //    if (instance.Count > quantity)
        //    {
        //        instance.Count -= quantity;
        //        InstanceSlotMap[instance].CountLb.Text = instance.Count.ToString();
        //    }
        //    else
        //    {
        //        ItemInstanceList.Remove(instance);
        //        //ui放入槽中
        //        //ItemView.InventoryGc.RemoveChild(InstanceSlotMap[instance]);
        //    }

        //    return true;
        //}
        

        //private void InitInventory()
        //{
        //    AddItem(ItemDataBase.Instance().IdItemMap[0], 1);
        //    AddItem(ItemDataBase.Instance().IdItemMap[0], 11);
        //    AddItem(ItemDataBase.Instance().IdItemMap[1], 1);
        //    AddItem(ItemDataBase.Instance().IdItemMap[1], 11);
        //    AddItem(ItemDataBase.Instance().IdItemMap[2], 1);
        //    AddItem(ItemDataBase.Instance().IdItemMap[2], 11);
        //}
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
