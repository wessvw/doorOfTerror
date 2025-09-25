using Godot;
using System;

public partial class Hotbar : Control
{
	// Called when the node enters the scene tree for the first time.
	private Inventoryui ui;
	public int selectedSlot = 0;
	public override void _Ready()
	{
		ui = GetParent().GetNode("InventoryUI") as Inventoryui;
		GD.Print(ui);
		ChangeSelectedHotbarSlot();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("scrollUp"))
		{
			if (selectedSlot < 2)
			{
				selectedSlot++;
			}
			ChangeSelectedHotbarSlot();
		}
		if (Input.IsActionJustPressed("scrollDown"))
		{
			if (selectedSlot > 0 )
			{
				selectedSlot--;
			}
			ChangeSelectedHotbarSlot();
		}
	}

	private void ChangeSelectedHotbarSlot()
	{
		for (int i = 0; i < ui.hotBarSlots.Count; i++)
		{
			ui.hotBarSlots[i].selectedSprite.Visible = false;
		}
		ui.hotBarSlots[selectedSlot].selectedSprite.Visible = true;
	}
}
