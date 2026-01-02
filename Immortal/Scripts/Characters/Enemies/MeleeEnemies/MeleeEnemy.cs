using Godot;
using RpgGame.Scripts.AttributeSystem;
using RpgGame.Scripts.Characters.Players;
using RpgGame.Scripts.GameSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Enemies.MeleeEnemies
{
    public partial class MeleeEnemy : Enemy
    {
        public StateMachine Sm;

        public Vector2 startPos;

        [Export]
        public AnimatedSprite2D Anim;
        [Export]
        public ProgressBar HealthPb;

        [Export] public float ChaseRange = 500;
        [Export] public float AtkRange = 50;
        public float ChaseRangeSq;
        public float AtkRangeSq;
        public Player Player;


        [Export]
        public float patrolSpeed = 100;
        [Export]
        public float chaseSpeed = 200;

        

        public override void _Ready()
        {
            base._Ready();
            
            Sm = new StateMachine(this);
            startPos = GlobalPosition;
            InitAttr();
            Player = GameManager.Instance().Player;
            ChaseRangeSq = ChaseRange * ChaseRange;
            AtkRangeSq = AtkRange * AtkRange;
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
            Sm.CurState.Update((float)delta);

            
        }

        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
            Sm.CurState.FixedUpdate((float)delta);
            UpdateUi((float)delta);
        }

        protected override void InitAttr()
        {
            //先初始化等级, 等级之后改为配置文件读取, 目前先给定
            //再根据等级获取属性
            LevelMap.Add(WuXingType.Metal, 1);
            LevelMap.Add(WuXingType.Wood, 1);
            LevelMap.Add(WuXingType.Water, 0);
            LevelMap.Add(WuXingType.Fire, 1);
            LevelMap.Add(WuXingType.Earth, 0);

            AttrContainer.GetAttrValue(AttributeType.Def).BaseValue = GetAttr(AttributeType.Def);

            AttrContainer.GetAttrValue(AttributeType.HpRegen).BaseValue = GetAttr(AttributeType.HpRegen);
            AttrContainer.GetAttrValue(AttributeType.EnergyRegen).BaseValue = GetAttr(AttributeType.EnergyRegen);
            AttrContainer.GetAttrValue(AttributeType.MoveSpeed).BaseValue = GetAttr(AttributeType.MoveSpeed);

            AttrContainer.GetAttrValue(AttributeType.MaxEnergy).BaseValue = GetAttr(AttributeType.MaxEnergy);

            AttrContainer.GetAttrValue(AttributeType.Atk).BaseValue = GetAttr(AttributeType.Atk);

            AttrContainer.GetAttrValue(AttributeType.MaxHp).BaseValue = GetAttr(AttributeType.MaxHp);

            AttrContainer.RecalculateAllAttributes();

            CurHealth = AttrContainer.GetAttrValue(AttributeType.MaxHp).FinalValue;
            CurEnergy = AttrContainer.GetAttrValue(AttributeType.EnergyRegen).FinalValue;
        }

        public void PatrolTo(Vector2 tarPos)
        {
            CurDir = (tarPos - GlobalPosition).Normalized();
            if (CurDir.X < 0) Anim.FlipH = true;
            else Anim.FlipH = false;
            Velocity = CurDir * patrolSpeed;
            MoveAndSlide();
        }
        public void Chase()
        {
            CurDir = (Player.GlobalPosition - GlobalPosition).Normalized();
            if (CurDir.X < 0) Anim.FlipH = true;
            else Anim.FlipH = false;
            Velocity = CurDir * chaseSpeed;
            MoveAndSlide();
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
        }

        [Export] float AtkAngle = 160;
        public void Atk()
        {
            if (GlobalPosition.DistanceSquaredTo(Player.GlobalPosition) > AtkRangeSq) return;
            Vector2 dirToPlayer = (Player.GlobalPosition - GlobalPosition).Normalized();
            if (Mathf.Abs(dirToPlayer.AngleTo(CurDir)) > Mathf.DegToRad(AtkAngle / 2)) return;// 玩家是否在攻击扇形范围内
            Player.TakeDmg(DamageCalculator.CalculateDamage(AttrContainer, Player.AttrContainer));
        }

        public event Action TakeDmged;
        public override void TakeDmg(float dmg)
        {
            base.TakeDmg(dmg);
            if (Sm.CurState == Sm.DeathState) return;
            Sm.ChangeState(Sm.KnockBackState);
        }
        protected override void Die()
        {
            Sm.ChangeState(Sm.DeathState);
        }

        public override void _Draw()
        {
            base._Draw();
            //DrawCircle(Vector2.Zero, ChaseRange, new Color(1, 0, 0));
        }
    }
}
