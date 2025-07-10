using Godot;
using System;

public partial class SpawnedItem : StaticBody3D
{
	private ItemSpawner spawner;
	private PlayerMovement player;
	private MeshInstance3D mesh;
	private Sprite3D ebutton;
	[Export] Material outlineMaterial;
	public override void _Ready()
	{
		spawner = GetTree().Root.FindChild("ItemSpawner", true, false) as ItemSpawner;
		player = GetTree().Root.FindChild("Player", true, false) as PlayerMovement;
		mesh = GetChild<MeshInstance3D>(0);
		ebutton = GetChild<Sprite3D>(1);
	}

	public override void _Process(double delta)
	{
		// GD.Print(spawner.cube.count);
		if (NearPlayer())
		{
			ebutton.Visible = true;
			mesh.MaterialOverlay = outlineMaterial;
			if (Input.IsActionJustPressed("accept"))
			{
				if (spawner.cube.count > 0)
				{
					spawner.cube.count++;
					QueueFree();

				}
				else
				{
					if (player.aplayer.Backpack.Put("cube", spawner.cube))
					{
						spawner.cube.count++;
						QueueFree();
					}
				}
			}
			ebutton.Rotation = player.Rotation;

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
