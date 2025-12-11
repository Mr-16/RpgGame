using Godot;
using RpgGame.Scripts.Characters.Enemies;
using RpgGame.Scripts.GameManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


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

        public int curSkillIndex = -1;//当前技能索引, 取值 : 012, 每次进入技能状态都要根据索引在技能list里找到具体技能

        [Export]
        public bool isMoveAtkEnable = true;

        [Export]
        public ProgressBar HealthPb;

        public override void _Ready()
        {
            AddToGroup("player");
            Sm = new StateMachine(this);
            InitAttribute();
            AtkRangeSq = AtkRange * AtkRange;
        }

        public override void _Process(double delta)
        {
            Sm.curState.Update((float)delta);
            //GD.Print("curState" + Sm.curState);

        }
        public override void _PhysicsProcess(double delta)
        {
            Sm.curState.FixedUpdate((float)delta);
            //GD.Print("Stamina" + curStamina);
            UpdateUi((float)delta);
        }

        private void InitAttribute()
        {
            //初始化玩家的属性
            //基础属性直接赋值(后面有存档了改成在json里读)
            //最终属性根据装备等加成计算
            BaseAttr.MaxHealth = 100;
            BaseAttr.MaxMana = 100;
            BaseAttr.MoveSpeed = 300;
            BaseAttr.RollSpeed = 1000;
            BaseAttr.MaxStam = 200;
            BaseAttr.StamRegen = 30;//每秒恢复量
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

            FinalAttr.MaxHealth = BaseAttr.MaxHealth + Weapon.BonusAttr.MaxHealth + Helmet.BonusAttr.MaxHealth + Armor.BonusAttr.MaxHealth + Boot.BonusAttr.MaxHealth;
            FinalAttr.MaxMana = BaseAttr.MaxMana + Weapon.BonusAttr.MaxMana + Helmet.BonusAttr.MaxMana + Armor.BonusAttr.MaxMana + Boot.BonusAttr.MaxMana;
            FinalAttr.MoveSpeed = BaseAttr.MoveSpeed + Weapon.BonusAttr.MoveSpeed + Helmet.BonusAttr.MoveSpeed + Armor.BonusAttr.MoveSpeed + Boot.BonusAttr.MoveSpeed;
            FinalAttr.RollSpeed = BaseAttr.RollSpeed + Weapon.BonusAttr.RollSpeed + Helmet.BonusAttr.RollSpeed + Armor.BonusAttr.RollSpeed + Boot.BonusAttr.RollSpeed;
            FinalAttr.MaxStam = BaseAttr.MaxStam + Weapon.BonusAttr.MaxStam + Helmet.BonusAttr.MaxStam + Armor.BonusAttr.MaxStam + Boot.BonusAttr.MaxStam;
            FinalAttr.StamRegen = BaseAttr.StamRegen + Weapon.BonusAttr.StamRegen + Helmet.BonusAttr.StamRegen + Armor.BonusAttr.StamRegen + Boot.BonusAttr.StamRegen;
            //攻击属性
            FinalAttr.AtkSpeed = BaseAttr.AtkSpeed + Weapon.BonusAttr.AtkSpeed + Helmet.BonusAttr.AtkSpeed + Armor.BonusAttr.AtkSpeed + Boot.BonusAttr.AtkSpeed;
            FinalAttr.PhyAtk = BaseAttr.PhyAtk + Weapon.BonusAttr.PhyAtk + Helmet.BonusAttr.PhyAtk + Armor.BonusAttr.PhyAtk + Boot.BonusAttr.PhyAtk;
            FinalAttr.PhyPen = BaseAttr.PhyPen + Weapon.BonusAttr.PhyPen + Helmet.BonusAttr.PhyPen + Armor.BonusAttr.PhyPen + Boot.BonusAttr.PhyPen;
            FinalAttr.PhyPenFlat = BaseAttr.PhyPenFlat + Weapon.BonusAttr.PhyPenFlat + Helmet.BonusAttr.PhyPenFlat + Armor.BonusAttr.PhyPenFlat + Boot.BonusAttr.PhyPenFlat;
            FinalAttr.MagAtk = BaseAttr.MagAtk + Weapon.BonusAttr.MagAtk + Helmet.BonusAttr.MagAtk + Armor.BonusAttr.MagAtk + Boot.BonusAttr.MagAtk;
            FinalAttr.MagPen = BaseAttr.MagPen + Weapon.BonusAttr.MagPen + Helmet.BonusAttr.MagPen + Armor.BonusAttr.MagPen + Boot.BonusAttr.MagPen;
            FinalAttr.MagPenFlat = BaseAttr.MagPenFlat + Weapon.BonusAttr.MagPenFlat + Helmet.BonusAttr.MagPenFlat + Armor.BonusAttr.MagPenFlat + Boot.BonusAttr.MagPenFlat;
            FinalAttr.CritRate = BaseAttr.CritRate + Weapon.BonusAttr.CritRate + Helmet.BonusAttr.CritRate + Armor.BonusAttr.CritRate + Boot.BonusAttr.CritRate;
            FinalAttr.CritMult = BaseAttr.CritMult + Weapon.BonusAttr.CritMult + Helmet.BonusAttr.CritMult + Armor.BonusAttr.CritMult + Boot.BonusAttr.CritMult;
            //防御属性
            FinalAttr.PhyDef = BaseAttr.PhyDef + Weapon.BonusAttr.PhyDef + Helmet.BonusAttr.PhyDef + Armor.BonusAttr.PhyDef + Boot.BonusAttr.PhyDef;
            FinalAttr.MagDef = BaseAttr.MagDef + Weapon.BonusAttr.MagDef + Helmet.BonusAttr.MagDef + Armor.BonusAttr.MagDef + Boot.BonusAttr.MagDef;
            //特殊属性
            FinalAttr.LifeSteal = BaseAttr.LifeSteal + Weapon.BonusAttr.LifeSteal + Helmet.BonusAttr.LifeSteal + Armor.BonusAttr.LifeSteal + Boot.BonusAttr.LifeSteal;

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
            List<Enemy> enemyList = EnemyManager.Instance().EnemyList;
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
        }

        public Enemy GetClosestEnemy()
        {
            Enemy tarEnmey = null;
            List<Enemy> enemyList = EnemyManager.Instance().EnemyList;
            float minDisSq = float.MaxValue;
            foreach (Enemy enmey in enemyList)
            {
                float curDisSq = GlobalPosition.DistanceSquaredTo(enmey.GlobalPosition);
                if (curDisSq > AtkRangeSq) continue;
                if(curDisSq < minDisSq)
                {
                    minDisSq = curDisSq;
                    tarEnmey = enmey;
                }
            }
            return tarEnmey;
        }

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
