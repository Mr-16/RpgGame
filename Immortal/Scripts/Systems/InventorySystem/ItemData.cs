using Godot;
using RpgGame.Scripts.Systems.SkillSystem;
using RpgGame.Scripts.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Systems.InventorySystem
{
    public interface IItemComponent {}

    public class EquippableComponent : IItemComponent
    {
        public SkillData SkillData;
        public WuXingType WuXingType;
        public int Level = 1;
        public EquippableComponent(WuXingType wuXingType, SkillData skillData)
        {
            WuXingType = wuXingType;
            SkillData = skillData;
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

        //public EquipType EquipType;//装备类型
        public WuXingType WuXingType;
        public SkillData SkillData;

        //组件映射表, 用于创建实例时找到对应的Component
        public HashSet<ItemComponentType> CompSet;
    }
}
