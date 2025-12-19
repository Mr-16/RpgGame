using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Items
{
    public class ItemDataBase
    {
        private static ItemDataBase instance;
        public static ItemDataBase Instance()
        {
            if(instance == null)
            {
                instance = new ItemDataBase();
            }
            return instance;
        }

        public Dictionary<int, ItemData> IdItemMap = new Dictionary<int, ItemData>();

        private ItemDataBase() 
        {
            IdItemMap[0] = new ItemData(0, "Test0", "this is a description0, fuckyou", ItemType.Consumable, 99, "res://icon.svg");
            IdItemMap[1] = new ItemData(1, "Test1", "this is a description1, fuckyou", ItemType.Material, 99, "res://Assets/Tiny Swords (Free Pack)/Tiny Swords (Free Pack)/Units/Black Units/Archer/Archer_Idle.png");
            IdItemMap[2] = new ItemData(2, "Test2", "this is a description2, fuckyou", ItemType.Equipment, 99, "res://Assets/Tiny Swords (Free Pack)/Tiny Swords (Free Pack)/Units/Black Units/Archer/Arrow.png");
        }
    }
}
