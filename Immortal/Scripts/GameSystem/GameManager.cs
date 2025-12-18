using Godot;
using RpgGame.Scripts.Characters.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.GameSystem
{
    public enum GameState
    {
        StartMenu,
        Play,
        Pause,
        GameOver,
    }

    public partial class GameManager : Node
    {
        //单例, 管理游戏状态 + 游戏全局数据
        private static GameManager instance;
        public static GameManager Instance()
        {
            return instance;
        }
        public override void _Ready()
        {
            base._Ready();
            instance = this;
        }

        //管理玩家引用
        public Player Player;

        //统一管理所有的游戏场景
        [Export]
        public PackedScene StartMenuScene;
        [Export]
        public PackedScene MainScene;
        [Export]
        public PackedScene GameOverScene;
        private void ChangeScene(PackedScene newPackedScene)
        {
            if (newPackedScene == null) return;
            SceneTree tree = GetTree();

            tree.CurrentScene.QueueFree();
            Node node = newPackedScene.Instantiate();
            tree.Root.AddChild(node);
            tree.CurrentScene = node;
        }

        //游戏状态机
        public GameState curState;
        

        private void EnterState(GameState state)
        {
            switch (state)
            {
                case GameState.StartMenu:
                    ChangeScene(StartMenuScene);
                    break;
                case GameState.Play:
                    ChangeScene(MainScene);
                    break;
                case GameState.Pause:
                    
                    break;
                case GameState.GameOver:
                    ChangeScene(GameOverScene);
                    break;
                default:
                    break;
            }
        }
        private void ExitState(GameState state)
        {
            switch (state)
            {
                case GameState.StartMenu:
                    break;
                case GameState.Play:

                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
            }
        }
        public void ChangeState(GameState state)
        {
            ExitState(curState);
            curState = state;
            EnterState(state);
        }

        //技能系统
        //暂时初始化时填值, 后期序列化到Json配置文件里
        public Dictionary<SkillType, SkillData> SkillDataMap = new Dictionary<SkillType, SkillData>()
        {
            { SkillType.FireBall, new SkillData("火球术", 3, 5, "FireBall", 2, 2f, 600 * 600, true)},
            { SkillType.IceSpike, new SkillData("冰刺术", 5, 10, "IceSpike", 2, 2f, 300 * 300, false)},
            { SkillType.Lightning, new SkillData("闪电术", 10, 15, "Lightning", 2, 2f, 800 * 800, true)},
        };
    }

    public enum SkillType
    {
        None,
        // 主动技能 - 伤害
        FireBall,      // 火球, 射火球, 然后爆炸
        IceSpike,      // 冰刺, 360度射出
        Lightning,     // 闪电, 类似飞龙, 连环弹弹弹

        // 主动技能 - 强化
        DashAtk,       // 冲刺攻击
        PowerUp,       // 攻击强化

        // 回复 / 辅助
        AddHealth,      
    }

    public class SkillData
    {
        public SkillData(string name, float cooldown, float manaCost, string animStr, int castFrame, float animSpeedScale, float rangeSq, bool isFaceTar)
        {
            Name = name;
            Cooldown = cooldown;
            ManaCost = manaCost;
            AnimStr = animStr;
            CastFrame = castFrame;
            AnimSpeedScale = animSpeedScale;
            RangeSq = rangeSq;
            IsFaceTar = isFaceTar;
        }

        public string Name;
        public float Cooldown;
        public float ManaCost;
        public string AnimStr;      //技能播放的动画
        public int CastFrame;       //技能释放帧
        public float AnimSpeedScale;
        public float RangeSq;
        public bool IsFaceTar;
    }
}
