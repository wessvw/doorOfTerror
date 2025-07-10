using Godot;
using System;

public partial class SpawnedItem : StaticBody3D
{
	private PlayerMovement player;
	private MeshInstance3D mesh;
	private Sprite3D ebutton;
	[Export] Material outlineMaterial;
	[Export] Texture2D itemTexture;
	private Item cube;
	public override void _Ready()
	{
		PackedScene myScene = GD.Load<PackedScene>("res://Scenes/Items/cube.tscn");
		cube = new Item("cube", "just a regular cube",1, myScene,itemTexture);
		player = GetTree().Root.FindChild("Player", true, false) as PlayerMovement;
		mesh = GetChild<MeshInstance3D>(0);
		ebutton = GetChild<Sprite3D>(1);
	}

	public override void _Process(double delta)
	{
		// GD.Print(player.aplayer.Backpack);
		if (NearPlayer())
		{
			ebutton.Visible = true;
			mesh.MaterialOverlay = outlineMaterial;
			if (Input.IsActionJustPressed("accept"))
			{
				if (player.aplayer.Backpack.Put("cube", cube))
				{
					QueueFree();
				}
			}
			// ebutton.Position == player.Position;

		}
		else
		{
			ebutton.Visible = false;
			mesh.MaterialOverlay = null;
		}
	}

	public bool NearPlayer()
	{
		float distance = GlobalPosition.DistanceTo(player.GlobalPosition);
		return distance <= 5f;
	}
}
