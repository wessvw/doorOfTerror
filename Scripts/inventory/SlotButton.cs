using Godot;
using System;

public partial class SlotButton : Button
{
	private int slotNumber;
	public override void _Ready()
	{
		slotNumber = (int)GetMeta("type");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _GuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				GD.Print("hello" + slotNumber);
			}
		}
	}


}
