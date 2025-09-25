using Godot;
using System;

public partial class hotBarSlot : Button
{
	private Sprite2D itemVisual;
	public Sprite2D selectedSprite;
	private RichTextLabel textLabel;
	public Item itemInSlot;
	private Inventoryui ui;
	private int number;
	private Vector2 oldSpritePosition;
	private Vector2 oldTextLabelPosition;
	private bool activated;
	private bool buttonDownActivated = false;


	public override void _Ready()
	{
		textLabel = GetNode<RichTextLabel>("itemCount");
		itemVisual = GetChild(1).GetChild(0).GetNode<Sprite2D>("ItemContainer");
		selectedSprite = GetNode<Sprite2D>("selectSprite");
		number = (int)GetMeta("type");
		ui = GetParent().GetParent().GetNode("InventoryUI") as Inventoryui;

		this.ButtonUp += () => buttonUp();
		this.ButtonDown += () => buttonDown();
		this.MouseEntered += () => buttonMouseEntered();
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
}
