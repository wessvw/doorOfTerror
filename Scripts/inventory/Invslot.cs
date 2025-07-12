using Godot;
using System;
using System.Linq;
public partial class Invslot : Panel
{
	private Sprite2D itemVisual;
	private RichTextLabel textLabel;
	private Panel buttons;
	private Inventoryui ui;
	private int number;


	public override void _Ready()
	{
		// GD.Print(string.Format("[color=#000]", item.count));
		textLabel = GetNode<RichTextLabel>("itemCount");
		itemVisual = GetChild(1).GetChild(0).GetNode<Sprite2D>("ItemContainer");
		ui = GetParent().GetParent().GetParent<Inventoryui>();
		buttons = GetChild<Panel>(3);
		number = (int)GetMeta("type");
	}


	public override void _GuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Right && mouseEvent.Pressed)
			{
				if (this.buttons.Visible == false)
				{
					buttons.Visible = true;
					ui.ignoreSlots(number - 1);
				}
				else
				{
					buttons.Visible = false;
					ui.ignoreSlots(number- 1);
				}
			}
		}
	}

	public void UpdateTexture(Item item)
	{
		if (item == null)
		{
			itemVisual.Visible = false;
		}
		else
		{
			itemVisual.Visible = true;
			itemVisual.Texture = item.Texture;
		}
	}
	public void UpdateCount(Item item)
	{
		textLabel.Text = string.Format("[color=#000]{0}[/color]", item.count);
	}

	// public delegate void SlotPressedEventHandler(Item item);
}
