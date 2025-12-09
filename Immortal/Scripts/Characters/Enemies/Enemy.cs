using RpgGame.Scripts.GameManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Enemies
{
    public partial class Enemy : CharacterBase
    {
        public override void _Ready()
        {
            base._Ready();
            EnemyManager.Instance().EnemyList.Add(this);
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
    }
}
