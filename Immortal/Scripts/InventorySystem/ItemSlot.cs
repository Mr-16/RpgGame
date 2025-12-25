using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.InventorySystem
{
    public class ItemSlot
    {
        public ItemInstance Item = null;


        public int AddItem(int count)
        {
            if (Item == null) return 0;
            int canAdd = Item.Data.MaxStack - Item.Count; // 当前槽可增加的数量
            int addCount = Math.Min(canAdd, count);
            Item.Count += addCount;
            return addCount;
        }
        public int DelItemCount(int count)
        {
            if (Item == null) return 0;
            int delCount = Math.Min(Item.Count, count);
            Item.Count -= delCount;
            if (Item.Count <= 0) Item = null;
            return delCount;
        }



        //public int SetItem(ItemInstance item)
        //{
        //    if (item == null) return 0;

        //    if (Item == null)
        //    {
        //        // 槽为空，直接添加
        //        int addCount = Math.Min(item.Count, item.ItemData.MaxStack);
        //        Item = new ItemInstance(item.ItemData, addCount);
        //        return addCount;
        //    }
        //    else
        //    {
        //        // 槽已有物品，尝试堆叠
        //        if (Item.ItemData.Id != item.ItemData.Id) return 0; // 不同类型不能堆叠

        //        int canAdd = Item.ItemData.MaxStack - Item.Count; // 当前槽可增加的数量
        //        int addCount = Math.Min(canAdd, item.Count);

        //        Item.Count += addCount;
        //        return addCount;
        //    }
        //}

        

    }
}
