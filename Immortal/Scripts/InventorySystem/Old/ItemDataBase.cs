using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.InventorySystem.Old
{
    public class ItemIds
    {
        static public string Arrow = "Arrow";
        static public string Stone = "Stone";
        static public string Stone2 = "Stone2";
        static public string Wd40 = "Wd40";
        static public string Sword = "Sword";
        static public string Bow = "Bow";
    }

    public class ItemDataBase
    {
        public Dictionary<string, ItemDataOld> IdItemMap = new Dictionary<string, ItemDataOld>();

        private static ItemDataBase instance;
        public static ItemDataBase Instance()
        {
            if(instance == null)
                instance = new ItemDataBase();
            return instance;
        }

        public ItemDataBase()
        {
            IdItemMap.Add(ItemIds.Arrow, new ItemDataOld(ItemIds.Arrow, ItemTypeOld.Consumable, GD.Load<Texture2D>("res://Assets/Tiny Swords (Free Pack)/Tiny Swords (Free Pack)/Units/Black Units/Archer/Arrow.png") , 16, "箭", "我是箭"));
            IdItemMap.Add(ItemIds.Stone, new ItemDataOld(ItemIds.Stone, ItemTypeOld.Consumable, GD.Load<Texture2D>("res://Assets/Tiny Swords (Free Pack)/Tiny Swords (Free Pack)/Decorations/Rocks/Rock1.png"), 16, "石头", "我是石头"));
            IdItemMap.Add(ItemIds.Stone2, new ItemDataOld(ItemIds.Stone2, ItemTypeOld.Consumable, GD.Load<Texture2D>("res://Assets/Tiny Swords (Free Pack)/Tiny Swords (Free Pack)/Decorations/Rocks/Rock2.png"), 16, "石头2", "我是石头2"));
            IdItemMap.Add(ItemIds.Wd40, new ItemDataOld(ItemIds.Wd40, ItemTypeOld.Material, GD.Load<Texture2D>("res://Assets/MyAssets/Wd40.png"), 16, "WD40", "我是WD40"));
            IdItemMap.Add(ItemIds.Sword, new EquipDataOld(ItemIds.Sword, ItemTypeOld.Equipment, GD.Load<Texture2D>("res://Assets/MyAssets/Sword.png"), 1, "双股剑", "我是双股剑", EquipTypeOld.Weapon, RarityTypeOld.Common));
            IdItemMap.Add(ItemIds.Bow, new EquipDataOld(ItemIds.Bow, ItemTypeOld.Equipment, GD.Load<Texture2D>("res://Assets/MyAssets/Bow.png"), 1, "弓", "我是弓", EquipTypeOld.Weapon, RarityTypeOld.Common));
        }

        public ItemDataOld GetData(string id)
        {
            if(IdItemMap.TryGetValue(id, out ItemDataOld itemData)) { return itemData; }
            return null;
        }
    }
}
