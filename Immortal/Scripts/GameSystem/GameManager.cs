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
        private GameManager() { }
        public static GameManager Instance()
        {
            if(instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }

        //统一管理所有的游戏场景
        [Export]
        public PackedScene StartMenuScene;
        [Export]
        public PackedScene MainScene;
        [Export]
        public PackedScene GameOverScene;

        //游戏状态机
        public GameState curState;
        public void ChangeState(GameState state)
        {
            if(curState != null) ExitState(curState);
            curState = state;
            EnterState(state);
        }

        private void EnterState(GameState state)
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
        private void ExitState(GameState state)
        {

        }
    }
}
