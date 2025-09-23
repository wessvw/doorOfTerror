using Godot;
using System;

public partial class ItemFollowMouse : Node
{
	private Control ui;
	public override void _Ready()
	{
		ui = GetNode<Control>("InventoryUI");
		GD.Print(ui);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// GD.Print(GetViewport().GetMousePosition());
	}
}
