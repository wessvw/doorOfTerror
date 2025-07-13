using Godot;
using System;
using System.Linq;

public partial class Inventoryui : Control
{
	private PlayerMovement playerscript;
	private Inventory playerinv;
	public Godot.Collections.Array<Node> slots;
	private Godot.Collections.Array<Node> hotBarSlots;
	private int hotBarNumber = 0;
	private int needToRemoveTextureSlot;

	public override void _Ready()
	{
		slots = GetChild(0).GetChild(0).GetChildren();
		playerscript = GetTree().Root.FindChild("Player", true, false) as PlayerMovement;
		playerinv = playerscript.aplayer.Backpack;
		hotBarSlots = GetParent().GetNode("Hotbar").GetChildren();
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
						GD.Print(needToRemoveTextureSlot - 1);
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

			}
		}

	}

	public void ignoreSlots(int exception)
	{
		GD.Print(exception);
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
}
