using Godot;
using RpgGame.Scripts.GameSystem;
using RpgGame.Scripts.Systems.AttributeSystem;
using RpgGame.Scripts.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Enemies
{
    public partial class Enemy : CharacterBase
    {
        [Export]
        public GpuParticles2D DmgParticles;
        [Export]
        public PackedScene ExpBall;

        private Random rd = new Random();

        public override void _Ready()
        {
            base._Ready();
            GameManager.Instance().EnemyList.Add(this);
            //DmgParticles.Visible = false;
            //DmgParticles.Emitting = false;
            //Level = rd.Next(1, 30);
        }

        public override void _Process(double delta)
        {
            base._Process(delta);

        }

        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);

        }

        public override void _ExitTree()
        {
            base._ExitTree();
            GameManager.Instance().EnemyList.Remove(this);
        }


        public override async void TakeDmg(float dmg)
        {
            base.TakeDmg(dmg);
            //DmgParticles.Visible = true;

            //// 重置粒子（非常重要）
            DmgParticles.Restart();
            DmgParticles.Emitting = true;

            //// 等待粒子生命周期结束
            //double time = DmgParticles.Lifetime;
            ////await ToSignal(GetTree().CreateTimer(time), "timeout");
            //await Task.Delay((int)time*1000);
            //// 爆炸结束后消失
            //Visible = false;

            //// 如果是一次性特效，可以直接销毁
            //DmgParticles.QueueFree();
        }

        public void SpawnExpBall()
        {
            foreach (KeyValuePair<WuXingType, int> pair in LevelMap)
            {
                //todo 遍历五个灵根的等级, 获取可掉落的属性经验生成经验球
                if(pair.Value == 0) continue;
                int exp = LevelToExp(pair.Value);
                ExpBall expBall = ExpBall.Instantiate<ExpBall>();
                float force = (float)rd.NextDouble() * (2000 - 1000) + 1000;
                float angle = (float)rd.NextDouble() * MathF.PI * 2f;
                Vector2 forceDir = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
                expBall.ApplyImpulse(force * forceDir);
                expBall.Init(pair.Key, exp, GlobalPosition);
                GetTree().CurrentScene.AddChild(expBall);
            }
        }
        private int LevelToExp(int level) => 10 * level;
        public Dictionary<WuXingType, int> LevelMap = new Dictionary<WuXingType, int>();
        protected float GetAttr(AttributeType type)
        {
            switch(type)
            {
                case AttributeType.Def:
                    return 5 + LevelMap[WuXingType.Metal] * 1;
                case AttributeType.HpRegen:
                    return 1 + LevelMap[WuXingType.Wood] * 0.1f;
                case AttributeType.EnergyRegen:
                    return 0.5f + LevelMap[WuXingType.Wood] * 0.05f;
                case AttributeType.MoveSpeed:
                    return 100 + LevelMap[WuXingType.Wood] * 0.01f;
                case AttributeType.MaxEnergy:
                    return 50 + LevelMap[WuXingType.Water] * 2;
                case AttributeType.Atk:
                    return 10 + LevelMap[WuXingType.Fire] * 2;
                case AttributeType.MaxHp:
                    return 100 + LevelMap[WuXingType.Earth] * 4;
            }
            return -1;
        }

    }
}
