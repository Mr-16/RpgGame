using Godot;
using RpgGame.Scripts.InventorySystem;
using System;
using System.Collections.Generic;

public partial class InventoryGrid : GridContainer
{


    public Inventory Inventory;
    public List<ItemSlotPanel> SlotList = new List<ItemSlotPanel>();
    [Export] public PackedScene ItemSlotScene;

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
	
	public void Init(Inventory inventory)
	{
		Inventory = inventory;
		for (int i = 0; i < Inventory.Capacity; i++)
		{
            ItemSlotPanel slot = ItemSlotScene.Instantiate<ItemSlotPanel>();
            slot.ItemSwapped += (indexA, indexB) =>
            {
                Inventory.SwapItem(indexA, indexB);
            };
            slot.Index = i;
            AddChild(slot);
            SlotList.Add(slot);
        }
        Inventory.ItemChanged += (index, item) =>
        {
            SlotList[index].SetItem(item);
        };
	}
    
}


//public VeryGoodInventory Inventory;
//   private Dictionary<ItemSlot, ItemSlotControl> slotCtrlMap = new Dictionary<ItemSlot, ItemSlotControl>();

//   [Export]
//   public PackedScene ItemSlotCtrlScene;

//   public void BuildInventoryGrid(VeryGoodInventory inventory)
//   {
//       Inventory = inventory;
//       Inventory.OnSlotChanged += Inventory_OnSlotChanged;


//       foreach (var slot in Inventory.SlotList)
//       {
//           ItemSlotControl ctrl = ItemSlotCtrlScene.Instantiate<ItemSlotControl>();
//           ctrl.SetSlot(slot);          // 绑定数据
//           AddChild(ctrl);

//           slotCtrlMap.Add(slot, ctrl);
//       }
//   }


//   private void Inventory_OnSlotChanged(ItemSlot slot)
//   {
//       if (slotCtrlMap.TryGetValue(slot, out var ctrl))
//           ctrl.Refresh();
//   }
