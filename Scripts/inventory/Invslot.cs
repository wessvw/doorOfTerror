using Godot;
using System;

public partial class Invslot : Panel
{
	private Sprite2D itemVisual;
	public override void _Ready()
	{
		// GD.Print(GetChild(1).GetChild(0).GetNode<Sprite2D>("ItemContainer"));
		itemVisual = GetChild(1).GetChild(0).GetNode<Sprite2D>("ItemContainer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

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
}
