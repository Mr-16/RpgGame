using RpgGame.Scripts.Characters;
using RpgGame.Scripts.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.InventorySystem
{
    public class ItemInstance
    {
        public ItemData Data;
        public int Count;
        public ItemInstance(ItemData data, int count)
        {
            Data = data;
            Count = count;
        }
    }
    public class EquipInstance : ItemInstance
    {
        public int Level;
        public CharAttribute AttrBonus;

        public EquipInstance(ItemData data, int count, int level) : base(data, count)
        {
            Level = level;
        }
    }
}
