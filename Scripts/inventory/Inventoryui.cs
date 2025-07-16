using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class Inventoryui : Control
{
	private PlayerMovement playerscript;
	private Inventory playerinv;
	public Godot.Collections.Array<Node> slots;
	public Godot.Collections.Array<hotBarSlot> hotBarSlots;
	public Hotbar hotbarNode;
	private int hotBarNumber = 0;
	private int needToRemoveTextureSlot;

	public override void _Ready()
	{
		slots = GetChild(0).GetChild(0).GetChildren();
		playerscript = GetTree().Root.FindChild("Player", true, false) as PlayerMovement;
		playerinv = playerscript.aplayer.Backpack;
		hotbarNode = GetParent().GetNode<Hotbar>("Hotbar");
		var children = hotbarNode.GetChildren();
		hotBarSlots = new Godot.Collections.Array<hotBarSlot>();

		foreach (Node child in children)
		{
			if (child is hotBarSlot slot)
			{
				hotBarSlots.Add(slot);
			}
		}
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

		var keys = playerinv.Contents.Keys.ToList();
		int limit = Math.Min(keys.Count, slots.Count);

		for (int i = 0; i < limit; i++)
		{
			string key = keys[i];
			Item item = playerinv.Contents[key];
			if (item.slot > 100)
			{
				if (hotBarSlots[item.slot - 101] is hotBarSlot hslot)
				{
					hslot.UpdateTexture(item);
					hslot.UpdateCount(item);
					if (slots[needToRemoveTextureSlot - 1] is Invslot slot)
					{
						// GD.Print(needToRemoveTextureSlot - 1);
						slot.UpdateTexture(null);
						slot.UpdateCount(null);
						slot.itemInSlot = null;
					}
				}
			}
			else if (item.slot == -1)
			{
				item.slot = i;
			}
			else if (slots[item.slot] is Invslot slot)
			{
				slot.UpdateTexture(item);
				slot.UpdateCount(item);
				if (needToRemoveTextureSlot > 100)
				{
					// GD.Print(needToRemoveTextureSlot - 101);
					if (hotBarSlots[needToRemoveTextureSlot - 101] is hotBarSlot hslot)
					{
						// GD.Print(needToRemoveTextureSlot);
						hslot.UpdateTexture(null);
						hslot.UpdateCount(null);
						hslot.itemInSlot = null;
						needToRemoveTextureSlot = -1;
					}
				}

			}
		}

	}

	public void ignoreSlots(int exception)
	{
		// GD.Print(exception);
		for (int i = 0; i < slots.Count; i++)
		{
			if (slots[i] is Invslot slot)
			{
				if (slot.MouseFilter == Control.MouseFilterEnum.Ignore)
				{
					slot.MouseFilter = Control.MouseFilterEnum.Stop;
				}
				else
				{
					if (i == exception)
					{
						slot.MouseFilter = Control.MouseFilterEnum.Stop;
					}
					else
					{
						slot.MouseFilter = Control.MouseFilterEnum.Ignore;
					}
				}
			}
		}
	}

	public void ChangeToHotbarSlot(int hotbarslotNumber, int removeSlotNumber, Item item)
	{
		hotBarNumber = hotbarslotNumber;
		needToRemoveTextureSlot = removeSlotNumber;
		item.slot = hotbarslotNumber;
	}
	public void ChangeToInventorySlot(int hotbarslotNumber, int changeToSlotNumber)
	{
		Item item = hotBarSlots[hotbarslotNumber - 101].itemInSlot;
		item.slot = changeToSlotNumber - 1;
		needToRemoveTextureSlot = hotbarslotNumber;
		// GD.Print(hotbarslotNumber);
	}

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
				GD.Print(usable);
				usable.setUp(item, playerscript);
				usable.Use();
			}
		}
	}
}
