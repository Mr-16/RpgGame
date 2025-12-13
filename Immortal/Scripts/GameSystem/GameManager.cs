using Godot;
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
    }
}
