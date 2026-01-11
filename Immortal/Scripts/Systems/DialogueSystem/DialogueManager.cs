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
    public class DialogueNode
    {
        public string Id { get; set; }
        public string SpeakerName { get; set; }
        public string SpeakerHeadPath { get; set; }
        public List<string> LineList { get; set; }
        public List<DialogueOption> OptionList { get; set; }
    }

    public class DialogueOption
    {
        public string OptionText { get; set; }
        public string NextNodeId { get; set; }
    }

    public class DialogueManager
    {
        public Dictionary<string, DialogueNode> IdNodeMap { get; private set; }
        private DialogueNode curNode;
        private int curLineIndex;

        public event Action<string> OnEnterNode;//nodeId
        public event Action<string> OnExitNode;//nodeId
        public event Action<int> OnNextLine;//LineIndex

        private static DialogueManager instance = new DialogueManager();
        public static DialogueManager Instance() => instance;
        private DialogueManager() 
        {
            string JsonPath = Path.Combine(GameManager.Instance().GetDataFolderPath(), "DialogueNodeMapData.json");
            Load(JsonPath);
            //InitJson();
        }

        private void Load(string filePath)
        {
            string jsonStr = File.ReadAllText(filePath);
            IdNodeMap = JsonSerializer.Deserialize<Dictionary<string, DialogueNode>>(jsonStr);
            Debug.WriteLine(jsonStr);
        }

        public void StartDialogue(string id)
        {
            curNode = IdNodeMap[id];
            curLineIndex = 0;
            OnEnterNode?.Invoke(id);
            OnNextLine?.Invoke(curLineIndex);
        }
        public void NextLine()//鼠标点空白区域切下一句
        {
            OnNextLine?.Invoke(++curLineIndex);
        }
        public void ChooseOption(int optionIndex)//点击选项按钮切下个node
        {
            OnExitNode?.Invoke(curNode.Id);
            curNode = IdNodeMap[curNode.OptionList[optionIndex].NextNodeId];
            OnEnterNode?.Invoke(curNode.Id);
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
            File.WriteAllText(Path.Combine(folderPath, "DialogueNodeMapData.json"),jsonStr);

        }
    }
}
