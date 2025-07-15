using Godot;
using System;

public partial class hotBarSlot : Panel
{
	private Sprite2D itemVisual;
	public Sprite2D selectedSprite;
	private RichTextLabel textLabel;
	public Item itemInSlot;


	public override void _Ready()
	{
		textLabel = GetNode<RichTextLabel>("itemCount");
		itemVisual = GetChild(1).GetChild(0).GetNode<Sprite2D>("ItemContainer");
		selectedSprite = GetNode<Sprite2D>("selectSprite");
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
