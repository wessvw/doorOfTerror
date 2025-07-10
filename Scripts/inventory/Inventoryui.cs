using Godot;
using System;
using System.Linq;

public partial class Inventoryui : Control
{
	private PlayerMovement playerscript;
	private Inventory playerinv;
	private Godot.Collections.Array<Node> slots;
	// private Item item;

	public override void _Ready()
	{
		slots = GetChild(0).GetChild(0).GetChildren();
		playerscript = GetTree().Root.FindChild("Player", true, false) as PlayerMovement;
		playerinv = playerscript.aplayer.Backpack;
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

			if (slots[i] is Invslot slot)
			{
				slot.UpdateTexture(item);
			}
		}

	}
}
