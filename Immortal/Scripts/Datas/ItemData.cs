using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Datas
{
    public interface IItemComponent {}

    public enum EquipType
    {
        Weapon,
        Helmet,
        Armor,
        Ring,
        Boot,
    }
    public class EquippableComponent : IItemComponent
    {
        public EquipType EquipType;
        public int Level = 1;
        public EquippableComponent(EquipType equipType)
        {
            EquipType = equipType;
        }

        public void Equip()
        {
            Debug.WriteLine("[EquipmentComponent] Equip");
        }

        public void Unequip()
        {
            Debug.WriteLine("[EquipmentComponent] Unequip");
        }

        public void GetExp(float exp)
        {
            Debug.WriteLine("[EquipmentComponent] GetExp + " + exp);
        }
    }

    public class ConsumableComponent : IItemComponent
    {
        public void Use()
        {
            Debug.WriteLine("[ConsumableComponent] Use");
        }
    }

    public enum WeaponType
    {
        Sword,
        Bow,
        Dao,
        Spear,
    }

    public enum ItemComponentType
    {
        EquippableComponent,
        ConsumableComponent,
    }

    public class ItemData
    {
        public string Id;
        public string Name;
        public Texture2D Icon;
        public string Description;
        public int MaxStack;

        public EquipType EquipType;//装备类型

        //组件映射表, 用于创建实例时找到对应的Component
        public HashSet<ItemComponentType> CompSet;

        //public ItemData(string id, string name, int maxStack, Texture2D icon, string description)
        //{
        //    Id = id;
        //    Name = name;
        //    Icon = icon;
        //    Description = description;
        //    MaxStack = maxStack;
        //}
    }
}
