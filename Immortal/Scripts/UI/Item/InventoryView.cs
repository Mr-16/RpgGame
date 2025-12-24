using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class InventoryView : PanelContainer
{
   
    //[Export] public GridContainer GridContainer;
    //[Export] PackedScene CtrlScene;

    //// key: itemId, value: List<ItemInstCtrl>
    //public Dictionary<string, List<ItemInstCtrl>> IdCtrlListMap = new();

    //public override void _Ready()
    //{
        
    //}

    //public void Inventory_ItemAdded(ItemInstance inst)
    //{
    //    // 1. 找到空槽
    //    var emptySlot = GridContainer.GetChildren().OfType<ItemInstSlot>().FirstOrDefault(s => s.ItemInstCtrl == null);
    //    if (emptySlot == null)
    //    {
    //        GD.Print("没有空槽了！");
    //        return;
    //    }

    //    // 2. 实例化 ItemInstCtrl
    //    var ctrl = CtrlScene.Instantiate<ItemInstCtrl>();
    //    ctrl.SetItem(inst);

    //    // 添加到槽位，而不是用 Reparent
    //    emptySlot.AddChild(ctrl);
    //    emptySlot.ItemInstCtrl = ctrl;


    //    // 4. 更新 IdCtrlListMap
    //    if (!IdCtrlListMap.TryGetValue(inst.ItemData.Id, out var list))
    //        list = new List<ItemInstCtrl>();

    //    list.Add(ctrl);
    //    IdCtrlListMap[inst.ItemData.Id] = list;
    //}

    //public void Inventory_ItemRemoved(ItemInstance inst)
    //{
    //    if (IdCtrlListMap.TryGetValue(inst.ItemData.Id, out var list))
    //    {
    //        var ctrl = list.FirstOrDefault(c => c.ItemInst == inst);
    //        if (ctrl != null)
    //        {
    //            // 清理槽
    //            var slot = ctrl.GetParent<ItemInstSlot>();
    //            if (slot != null) slot.ItemInstCtrl = null;

    //            ctrl.QueueFree();
    //            list.Remove(ctrl);
    //        }

    //        if (list.Count == 0)
    //            IdCtrlListMap.Remove(inst.ItemData.Id);
    //    }
    //}

    //public void Inventory_ItemChanged(ItemInstance inst)
    //{
    //    if (IdCtrlListMap.TryGetValue(inst.ItemData.Id, out var list))
    //    {
    //        var ctrl = list.FirstOrDefault(c => c.ItemInst == inst);
    //        ctrl?.Refresh();
    //    }
    //}
}
