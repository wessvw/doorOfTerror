using Godot;
using System;
using System.Linq;
public partial class Invslot : Button
{
	private Sprite2D itemVisual;
	private RichTextLabel textLabel;
	private Panel buttons;
	private Inventoryui ui;
	private int number;
	public Item itemInSlot;


	public override void _Ready()
	{
		// GD.Print(string.Format("[color=#000]", item.count));
		textLabel = GetNode<RichTextLabel>("itemCount");
		itemVisual = GetChild(1).GetChild(0).GetNode<Sprite2D>("ItemContainer");
		ui = GetParent().GetParent().GetParent<Inventoryui>();
		buttons = GetChild<Panel>(3);
		number = (int)GetMeta("type");

		this.Pressed += () => buttonPressed();
		this.ButtonUp += () => buttonUp();
		this.ButtonDown += () => buttonDown();
	}


	public override void _GuiInput(InputEvent @event)
	{

	}

	private void buttonPressed()
	{
		GD.Print("pressed");
	}

	private void buttonUp()
	{
		GD.Print("up");
	}

	private void buttonDown()
	{
		GD.Print("down");
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
	

}
