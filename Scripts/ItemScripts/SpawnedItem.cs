using Godot;
using System;

public partial class SpawnedItem : StaticBody3D, IUsable
{
	private ItemSpawner spawner;
	private PlayerMovement playerscript;
	private MeshInstance3D mesh;
	private Sprite3D ebutton;
	[Export] Material outlineMaterial;
	private Item cube;
	public override void _Ready()
	{
		spawner = GetTree().Root.FindChild("ItemSpawner", true, false) as ItemSpawner;
		playerscript = GetTree().Root.FindChild("Player", true, false) as PlayerMovement;
		mesh = GetChild<MeshInstance3D>(0);
		ebutton = GetChild<Sprite3D>(1);
		cube = spawner.cube;
		GD.Print(cube);
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
					if (playerscript.aplayer.Backpack.Put("cube", spawner.cube))
					{
						spawner.cube.count++;
						QueueFree();
					}
				}
			}
			ebutton.Rotation = playerscript.Rotation;

		}
		else
		{
			ebutton.Visible = false;
			mesh.MaterialOverlay = null;
		}
	}

	public bool NearPlayer()
	{
		float distance = GlobalPosition.DistanceTo(playerscript.GlobalPosition);
		return distance <= 5f;
	}

	public void setUp(Item item, PlayerMovement playerScript)
	{
		playerscript = playerScript;
		cube = item;
	}

	public void Use()
	{
		if (cube.count > 1)
		{
			cube.count = cube.count - 1;
		}
		else if (cube.count == 1)
		{
			playerscript.aplayer.Backpack.Get(cube.IName);
		}
		playerscript.aplayer.Sanity = playerscript.aplayer.Sanity + 100;
		GD.Print(playerscript.aplayer.Sanity);
	}
}
