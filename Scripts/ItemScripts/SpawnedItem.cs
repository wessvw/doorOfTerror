using Godot;
using System;

public partial class SpawnedItem : StaticBody3D
{
	private PlayerMovement player;
	private MeshInstance3D mesh;
	private Sprite3D ebutton;
	[Export] Material outlineMaterial;
	private Item cube;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PackedScene myScene = GD.Load<PackedScene>("res://Scenes/Items/cube.tscn");
		Item cube = new Item("cube", "just a regular cube", 1, myScene);
		player = GetTree().Root.FindChild("Player", true, false) as PlayerMovement;
		mesh = GetChild<MeshInstance3D>(0);
		ebutton = GetChild<Sprite3D>(1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (NearPlayer())
		{
			ebutton.Visible = true;
			mesh.MaterialOverlay = outlineMaterial;
			if (Input.IsActionJustPressed("accept"))
			{
				player.aplayer.Backpack.Put("cube", cube);
				GD.Print(player.aplayer.Backpack);
			}
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
