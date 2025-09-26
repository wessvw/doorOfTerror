using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class Inventoryui : Control
{
	private PlayerMovement playerscript;
	private Inventory playerinv;
	public Godot.Collections.Array<Node> slots;
	public Godot.Collections.Array<hotBarSlot> hotBarSlots;
	public Hotbar hotbarNode;
	private int hotBarNumber = 0;
	private int needToRemoveTextureSlot;
	public int slotToMoveTo;

	public override void _Ready()
	{
		slots = GetChild(0).GetChild(0).GetChildren();
		playerscript = GetTree().Root.FindChild("Player", true, false) as PlayerMovement;
		playerinv = playerscript.aplayer.Backpack;
		hotbarNode = GetParent().GetNode<Hotbar>("Hotbar");
		// GD.Print(hotbarNode.GetChildren());
		var children = hotbarNode.GetChildren();
		hotBarSlots = new Godot.Collections.Array<hotBarSlot>();

		foreach (Node child in children)
		{
			// GD.Print(child);
			if (child is hotBarSlot slot)
			{

				hotBarSlots.Add(slot);
			}
		}
		GD.Print(hotBarSlots[2]);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("inventory"))
		{
			if (this.Visible == false)
			{

				this.Visible = true;
				Input.MouseMode = Input.MouseModeEnum.Visible;
			}
			else
			{
				this.Visible = false;
				Input.MouseMode = Input.MouseModeEnum.Captured;
			}
		}
		updateInventory();

	}

	public void updateInventory()
	{
		var keys = playerinv.Contents.Keys.ToList();
		int limit = Math.Min(keys.Count, slots.Count);
		
		for (int i = 0; i < limit; i++)
		{
			string key = keys[i];
			Item item = playerinv.Contents[key];
			// GD.Print(item.slot);
			if (item.slot > 100)
			{
				if (hotBarSlots[item.slot - 101] is hotBarSlot hslot)
				{
					hslot.UpdateTexture(item);
					hslot.UpdateCount(item);
				}
			}
			else if (item.slot == -1)
			{
				List<int> availableSlots = new List<int>();
				foreach (Invslot slot in slots)
				{
					if (slot.itemInSlot == null)
					{
						availableSlots.Add(slot.number);
					}
				}
				item.slot = availableSlots[0];
			}
			else if (slots[item.slot] is Invslot slot)
			{
				slot.UpdateTexture(item);
				slot.UpdateCount(item);

			}
		}
	}

	// public void ChangeToHotbarSlot(int hotbarslotNumber, int removeSlotNumber, Item item)
	// {
	// 	hotBarNumber = hotbarslotNumber;
	// 	needToRemoveTextureSlot = removeSlotNumber;
	// 	item.slot = hotbarslotNumber;
	// }
	// public void ChangeToInventorySlot(int hotbarslotNumber, int changeToSlotNumber)
	// {
	// 	if (hotBarSlots[hotbarslotNumber - 101].itemInSlot != null)
	// 	{
	// 		Item item = hotBarSlots[hotbarslotNumber - 101].itemInSlot;
	// 		item.slot = changeToSlotNumber - 1;
	// 		needToRemoveTextureSlot = hotbarslotNumber;
	// 	}
	// }

	public void useSelectedItem()
	{
		if (hotBarSlots[hotbarNode.selectedSlot].itemInSlot != null)
		{
			useItem(hotBarSlots[hotbarNode.selectedSlot].itemInSlot);
			if (hotBarSlots[hotbarNode.selectedSlot].itemInSlot.count == 0)
			{
				hotBarSlots[hotbarNode.selectedSlot].itemInSlot = null;
				hotBarSlots[hotbarNode.selectedSlot].UpdateTexture(null);
				hotBarSlots[hotbarNode.selectedSlot].UpdateCount(null);
			}
		}
	}

	public void useItem(Item item)
	{
		if (item != null)
		{
			Node instance = item.Scene.Instantiate();
			if (instance is IUsable usable)
			{
				//GD.Print(usable);
				usable.setUp(item, playerscript);
				usable.Use();
			}
		}
	}
}
