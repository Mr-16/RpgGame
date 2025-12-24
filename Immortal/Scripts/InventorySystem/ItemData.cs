using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.InventorySystem
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
        public Texture2D Icon;
        public int MaxStack;
        public string Name;
        public string Description;

        public ItemData(string id, ItemType type, Texture2D icon, int maxStack, string name, string dscription)
        {
            Id = id;
            ItemType = type;
            Icon = icon;
            MaxStack = maxStack;
            Name = name;
            Description = dscription;
        }
    }
}
