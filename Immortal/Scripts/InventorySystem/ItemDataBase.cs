using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.InventorySystem
{
    public class ItemIds
    {
        static public string Arrow = "Arrow";
        static public string Stone = "Stone";
        static public string Stone2 = "Stone2";
    }

    public class ItemDataBase
    {
        public Dictionary<string, ItemData> IdItemMap = new Dictionary<string, ItemData>();

        private static ItemDataBase instance;
        public static ItemDataBase Instance()
        {
            if(instance == null)
                instance = new ItemDataBase();
            return instance;
        }

        public ItemDataBase()
        {
            IdItemMap.Add(ItemIds.Arrow, new ItemData(ItemIds.Arrow, ItemType.Consumable, GD.Load<Texture2D>("res://Assets/Tiny Swords (Free Pack)/Tiny Swords (Free Pack)/Units/Black Units/Archer/Arrow.png") , 16, "箭", "我是箭"));
            IdItemMap.Add(ItemIds.Stone, new ItemData(ItemIds.Stone, ItemType.Consumable, GD.Load<Texture2D>("res://Assets/Tiny Swords (Free Pack)/Tiny Swords (Free Pack)/Decorations/Rocks/Rock1.png"), 16, "石头", "我是石头"));
            IdItemMap.Add(ItemIds.Stone2, new ItemData(ItemIds.Stone2, ItemType.Consumable, GD.Load<Texture2D>("res://Assets/Tiny Swords (Free Pack)/Tiny Swords (Free Pack)/Decorations/Rocks/Rock2.png"), 16, "石头2", "我是石头2"));
        }

        public ItemData GetData(string id)
        {
            if(IdItemMap.TryGetValue(id, out ItemData itemData)) { return itemData; }
            return null;
        }
    }
}
