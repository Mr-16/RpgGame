using RpgGame.Scripts.GameSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Systems.DialogueSystem
{
    //Lines 逐条播放
    //如果播完：
    //有 Options → 显示选项
    //没 Options 但有 NextNodeId → 自动跳
    //都没有 → 对话结束
    public class DialogueNode
    {
        public string Id { get; set; }
        public string SpeakerName { get; set; }
        public string SpeakerHeadPath { get; set; }
        public string NextNodeId { get; set; }
        public List<string> LineList { get; set; }
        public List<DialogueOption> OptionList { get; set; }
    }

    public class DialogueOption
    {
        public string OptionText { get; set; }
        public string NextNodeId { get; set; }
    }

    public enum DialogueState
    {
        Idle,
        PlayingLines,
        WaitingForOption,
        End
    }

    public class DialogueManager
    {
        public Dictionary<string, DialogueNode> IdNodeMap { get; private set; }

        private DialogueNode curNode;
        private int curLineIndex;
        private DialogueState state = DialogueState.Idle;

        /* ========= Events（UI 只关心这些） ========= */

        public event Action<DialogueNode> OnEnterNode;
        public event Action<int, string> OnLineChanged;
        public event Action<List<DialogueOption>> OnOptionsShow;
        public event Action OnDialogueEnd;

        /* ========= Singleton ========= */

        private static readonly DialogueManager instance = new DialogueManager();
        public static DialogueManager Instance() => instance;

        private DialogueManager()
        {
            string path = Path.Combine(GameManager.Instance().GetDataFolderPath(), "DialogueNodeMapData.json");
            Load(path);
        }

        private void Load(string filePath)
        {
            string json = File.ReadAllText(filePath);
            IdNodeMap = JsonSerializer.Deserialize<Dictionary<string, DialogueNode>>(json);
        }

        /* ========= Public API ========= */

        public bool IsRunning => state != DialogueState.Idle;

        public void StartDialogue(string nodeId)
        {
            if (!IdNodeMap.ContainsKey(nodeId))
                throw new Exception($"DialogueNode not found: {nodeId}");

            EnterNode(IdNodeMap[nodeId]);
        }

        /// <summary>
        /// 玩家点击 / 自动播放时调用
        /// </summary>
        public void Next()
        {
            if (state != DialogueState.PlayingLines)
                return;

            curLineIndex++;

            if (curLineIndex < curNode.LineList.Count)
            {
                EmitLine();
            }
            else
            {
                HandleNodeFinished();
            }
        }

        public void ChooseOption(int optionIndex)
        {
            if (state != DialogueState.WaitingForOption)
                return;

            var option = curNode.OptionList[optionIndex];
            EnterNode(IdNodeMap[option.NextNodeId]);
        }

        /* ========= Internal ========= */

        private void EnterNode(DialogueNode node)
        {
            curNode = node;
            curLineIndex = 0;
            state = DialogueState.PlayingLines;

            OnEnterNode?.Invoke(curNode);
            EmitLine();
        }

        private void EmitLine()
        {
            string line = curNode.LineList[curLineIndex];
            OnLineChanged?.Invoke(curLineIndex, line);
        }

        private void HandleNodeFinished()
        {
            // 1️⃣ 有选项
            if (curNode.OptionList != null && curNode.OptionList.Count > 0)
            {
                state = DialogueState.WaitingForOption;
                OnOptionsShow?.Invoke(curNode.OptionList);
                return;
            }

            // 2️⃣ 自动跳转
            if (!string.IsNullOrEmpty(curNode.NextNodeId))
            {
                EnterNode(IdNodeMap[curNode.NextNodeId]);
                return;
            }

            // 3️⃣ 结束
            EndDialogue();
        }

        private void EndDialogue()
        {
            state = DialogueState.End;
            curNode = null;
            OnDialogueEnd?.Invoke();
            state = DialogueState.Idle;
        }
        private void InitJson()
        {
            IdNodeMap = new Dictionary<string, DialogueNode>();
            IdNodeMap.Add("testId1", new DialogueNode()
            {
                Id = "testId1",
                SpeakerName = "testSpeakerName",
                SpeakerHeadPath = "testSpeakerHeadPath",
                LineList = new List<string>()
                {
                    "testLine1",
                    "testLine2",
                },
                OptionList = new List<DialogueOption>()
                {
                    new DialogueOption() { OptionText = "testOption1", NextNodeId = "testId2" } ,
                    new DialogueOption() { OptionText = "testOption2", NextNodeId = "testId3" }
                }
            });
            IdNodeMap.Add("testId2", new DialogueNode()
            {
                Id = "testId2",
                SpeakerName = "testSpeakerName",
                SpeakerHeadPath = "testSpeakerHeadPath",
                LineList = new List<string>()
                {
                    "testLine1",
                    "testLine2",
                },
                OptionList = new List<DialogueOption>()
                {
                    new DialogueOption() { OptionText = "testOption1", NextNodeId = "testId2" } ,
                    new DialogueOption() { OptionText = "testOption2", NextNodeId = "testId3" }
                }
            });
            string jsonStr = JsonSerializer.Serialize(IdNodeMap);
            string folderPath = GameManager.Instance().GetDataFolderPath();
            Debug.WriteLine(jsonStr);
            Directory.CreateDirectory(folderPath);
            File.WriteAllText(Path.Combine(folderPath, "DialogueNodeMapData.json"), jsonStr);

        }
    }
}


//public class DialogueManager
//{
//    public Dictionary<string, DialogueNode> IdNodeMap { get; private set; }
//    private DialogueNode curNode;
//    private int curLineIndex;

//    public event Action<string> OnEnterNode;//nodeId
//    public event Action<string> OnExitNode;//nodeId
//    public event Action<int> OnLineChanged;//LineIndex
//    public event Action OnOptionShow;
//    public event Action OnDialogueDone;

//    private static DialogueManager instance = new DialogueManager();
//    public static DialogueManager Instance() => instance;
//    private DialogueManager() 
//    {
//        string JsonPath = Path.Combine(GameManager.Instance().GetDataFolderPath(), "DialogueNodeMapData.json");
//        Load(JsonPath);
//        //InitJson();
//    }

//    private void Load(string filePath)
//    {
//        string jsonStr = File.ReadAllText(filePath);
//        IdNodeMap = JsonSerializer.Deserialize<Dictionary<string, DialogueNode>>(jsonStr);
//        Debug.WriteLine(jsonStr);
//    }

//    public void StartDialogue(string id)
//    {
//        curNode = IdNodeMap[id];
//        OnEnterNode?.Invoke(id);
//        while(true)
//        {
//            curLineIndex++;
//            if (curLineIndex == curNode.LineList.Count) break;
//            OnLineChanged?.Invoke(curLineIndex);
//            Task.Delay(1000);//Line等待后自动切下一句
//        }
//        //有选项出选项
//        if (curNode.OptionList.Count > 0)
//        {
//            OnOptionShow?.Invoke();
//        }

//        //有下个node切下个node
//        if(curNode.NextNodeId != null)
//        {
//            StartDialogue(curNode.NextNodeId);
//        }
//            //结束对话
//            OnDialogueDone
//    }
//    //public void NextLine()//主动切下一句
//    //{
//    //    ChangeToNextLine();
//    //}
//    public void ChooseOption(int optionIndex)//点击选项按钮切下个node
//    {
//        OnExitNode?.Invoke(curNode.Id);
//        curNode = IdNodeMap[curNode.OptionList[optionIndex].NextNodeId];
//        OnEnterNode?.Invoke(curNode.Id);
//    }
//    private bool ChangeToNextLine()
//    {

//    }




//}
