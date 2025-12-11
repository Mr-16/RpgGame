using Godot;
using RpgGame.Scripts.Characters.Players;
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


        //
        //[Export]
        //public Area2D chaseArea;
        //[Export]
        //public Area2D atkArea;
        //public Player chaseTarget = null;
        //public Player atkTarget = null;

        //[Export]
        //public Area2D DmgArea;
        //private Player dmgPlayer;
        //
        [Export] public float ChaseRange = 250;
        [Export] public float AtkRange = 100;
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
            //chaseArea.BodyEntered += ChaseArea_BodyEntered;
            //chaseArea.BodyExited += ChaseArea_BodyExited;
            //atkArea.BodyEntered += AtkArea_BodyEntered;
            //atkArea.BodyExited += AtkArea_BodyExited;
            //DmgArea.BodyEntered += DmgArea_BodyEntered;
            //DmgArea.BodyExited += DmgArea_BodyExited;
            InitAttr();
            var playerList = GetTree().GetNodesInGroup("player");
            Player = playerList[0] as Player;
            ChaseRangeSq = ChaseRange * ChaseRange;
            AtkRangeSq = AtkRange * AtkRange;
        }

        //private void DmgArea_BodyEntered(Node2D body)
        //{
        //    if (body is not Player player) return;
        //    dmgPlayer = player;
        //}
        //private void DmgArea_BodyExited(Node2D body)
        //{
        //    if (body is not Player player) return;
        //    dmgPlayer = null;
        //}

        //private void ChaseArea_BodyEntered(Node2D body)
        //{
        //    if (body is not Player player) return;
        //    chaseTarget = player;
        //}
        //private void ChaseArea_BodyExited(Node2D body)
        //{
        //    if (body is not Player player) return;
        //    chaseTarget = null;
        //}

        //private void AtkArea_BodyEntered(Node2D body)
        //{
        //    if (body is not Player player) return;
        //    atkTarget = player;
        //}
        //private void AtkArea_BodyExited(Node2D body)
        //{
        //    if (body is not Player player) return;
        //    atkTarget = null;
        //}

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

        private void InitAttr()
        {
            //初始化玩家的属性
            //基础属性直接赋值(后面有存档了改成在json里读)
            //最终属性根据装备等加成计算
            BaseAttr.MaxHealth = 100;
            BaseAttr.MaxMana = 100;
            BaseAttr.MoveSpeed = 300;
            BaseAttr.RollSpeed = 1000;
            BaseAttr.MaxStam = 200;
            BaseAttr.StamRegen = 10;//每秒恢复量
            //攻击属性
            BaseAttr.AtkSpeed = 0.1f;
            BaseAttr.PhyAtk = 10;
            BaseAttr.PhyPen = 0.1f;
            BaseAttr.PhyPenFlat = 10;
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
            HealthPb.MaxValue = FinalAttr.MaxHealth;
            HealthPb.Value = CurHealth;
        }

        [Export] float AtkAngle = 160;
        public void Atk()
        {
            if (GlobalPosition.DistanceSquaredTo(Player.GlobalPosition) > AtkRangeSq) return;
            Vector2 dirToPlayer = (Player.GlobalPosition - GlobalPosition).Normalized();
            if (Mathf.Abs(dirToPlayer.AngleTo(CurDir)) > Mathf.DegToRad(AtkAngle / 2)) return;// 玩家是否在攻击扇形范围内
            Player.TakeDmg(CalcPhyDamage(Player.FinalAttr));
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
