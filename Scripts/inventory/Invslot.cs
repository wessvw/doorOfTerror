using Godot;
using System;

public partial class Invslot : Panel
{
	private Sprite2D itemVisual;
	private RichTextLabel textLabel;

	public override void _Ready()
	{
		// GD.Print(string.Format("[color=#000]", item.count));
		textLabel = GetNode<RichTextLabel>("itemCount");
		itemVisual = GetChild(1).GetChild(0).GetNode<Sprite2D>("ItemContainer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

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
}
