using Godot;
using System;

public partial class keyItem : StaticBody3D, IUsable
{
	private ItemSpawner spawner;
	private PlayerMovement playerscript;
	private MeshInstance3D mesh;
	private Sprite3D ebutton;
	[Export] Material outlineMaterial;
	private Item thisitem;
	public override void _Ready()
	{
		spawner = GetTree().Root.FindChild("ItemSpawner", true, false) as ItemSpawner;
		playerscript = GetTree().Root.FindChild("Player", true, false) as PlayerMovement;
		mesh = GetChild<MeshInstance3D>(0);
		ebutton = GetChild<Sprite3D>(1);
		thisitem = spawner.key;
		GD.Print(thisitem);
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
				if (thisitem.count > 0)
				{
					thisitem.count++;
					QueueFree();

				}
				else
				{
					if (playerscript.aplayer.Backpack.Put(thisitem.IName, thisitem))
					{
						thisitem.count++;
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
		thisitem = item;
	}

	public void Use()
	{
		if (thisitem.count > 1)
		{
			thisitem.count = thisitem.count - 1;
		}
		else if (thisitem.count == 1)
		{
			playerscript.aplayer.Backpack.Get(thisitem.IName);
			thisitem.count = 0;
			thisitem.slot = -1;
		}
		playerscript.aplayer.Sanity = playerscript.aplayer.Sanity + 100;
		GD.Print(playerscript.aplayer.Sanity);
	}
}
