using Godot;
using System;
using System.Linq;
public partial class Invslot : Button
{
	private Sprite2D itemVisual;
	private RichTextLabel textLabel;
	private Inventoryui ui;
	private int number;
	public Item itemInSlot;
	private Vector2 oldSpritePosition;
	private Vector2 oldTextLabelPosition;
	
	private bool activated;
	private bool buttonDownActivated = false;


	public override void _Ready()
	{
		// //GD.Print(string.Format("[color=#000]", item.count));
		textLabel = GetNode<RichTextLabel>("itemCount");
		itemVisual = GetChild(1).GetChild(0).GetNode<Sprite2D>("ItemContainer");
		ui = GetParent().GetParent().GetParent<Inventoryui>();
		number = (int)GetMeta("type");

		this.ButtonUp += () => buttonUp();
		this.ButtonDown += () => buttonDown();
		this.MouseEntered += () => buttonMouseEntered();
	}


	public override void _GuiInput(InputEvent @event)
	{

	}

	private void buttonMouseEntered()
	{
		GD.Print("mous entered" + this.Name);
		ui.slotToMoveTo = number;
	}


	private void buttonUp()
	{
		if (itemInSlot != null)
		{

			itemInSlot.slot = ui.slotToMoveTo;
			itemVisual.Texture = null;
			textLabel.Text = "";
			itemInSlot = null;
			activated = false;
		}
		buttonDownActivated = false;
		ui.updateInventory();
		itemVisual.Position = oldSpritePosition;
		textLabel.Position = oldTextLabelPosition;
	}

	private void buttonDown()
	{
		oldSpritePosition = itemVisual.Position;
		oldTextLabelPosition = textLabel.Position;
		buttonDownActivated = true;
		if (itemInSlot != null)
		{
			activated = true;
			itemVisual.GlobalPosition = GetGlobalMousePosition();
		}
	}

	public void UpdateTexture(Item item)
	{
		if (item == null)
		{
			itemVisual = null;
		}
		else
		{
			itemVisual.Visible = true;
			itemVisual.Texture = item.Texture;
			itemInSlot = item;
		}
	}

	// public void RemoveTexture()
	// {
	// 	itemVisual = null;
	// }

	public void UpdateCount(Item item)
	{
		if (item != null)
		{
			textLabel.Text = string.Format("[color=#000]{0}[/color]", item.count);
		}
		else
		{
			textLabel.Text = "";
		}
	}

	// public void toHotBarChange(int hotbarslotNumber)
	// {
	// 	ui.ChangeToHotbarSlot(hotbarslotNumber, number, itemInSlot);
	// }

	// public void backToInvChange(int hotbarslotNumber)
	// {
	// 	if (itemInSlot == null)
	// 	{
	// 		ui.ChangeToInventorySlot(hotbarslotNumber, number);
	// 	}

	// }

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion && buttonDownActivated)
		{
			itemVisual.GlobalPosition = GetGlobalMousePosition();
			textLabel.GlobalPosition = GetGlobalMousePosition();
		}
    }


}
