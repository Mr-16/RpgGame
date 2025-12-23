using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.ItemSystem
{
    public enum ItemType
    {
        Equipment,
        Consumable,
        Material,
    }

    public class ItemData
    {
        public string Id;
        public ItemType ItemType;
        public string IconPath;
        public int MaxStack;
        public string Name;
        public string Description;

        public ItemData(string id, ItemType type, string iconPath, int maxStack, string name, string dscription)
        {
            Id = id;
            ItemType = type;
            IconPath = iconPath;
            MaxStack = maxStack;
            Name = name;
            Description = dscription;
        }
    }
}
