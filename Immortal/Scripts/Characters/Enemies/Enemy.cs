using Godot;
using RpgGame.Scripts.GameSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Enemies
{
    public partial class Enemy : CharacterBase
    {
        [Export]
        public GpuParticles2D DmgParticles;

        public override void _Ready()
        {
            base._Ready();
            EnemyManager.Instance().EnemyList.Add(this);
            //DmgParticles.Visible = false;
            //DmgParticles.Emitting = false;
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
            EnemyManager.Instance().EnemyList.Remove(this);
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
    }
}
