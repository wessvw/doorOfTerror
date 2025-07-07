using Godot;
using System;

public partial class DoorHandler : StaticBody3D
{
	public Vector3 doorpos;
	private MeshInstance3D mesh;
	private Sprite3D ebutton;
	[Export] Material outlineMaterial;
	PlayerMovement playerMovement;



	public Vector3 Doorpos
	{
		get { return doorpos; }
	}
	// private Player player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerMovement = GetTree().Root.FindChild("Player", true, false) as PlayerMovement;
		doorpos = GlobalPosition;
		mesh = GetChild<MeshInstance3D>(0);
		ebutton = GetChild<Sprite3D>(1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (playerMovement.isPlayerNearDoor())
		{
			ebutton.Visible = true;
			mesh.MaterialOverlay = outlineMaterial;
		}
		else
		{
			ebutton.Visible = false;
			mesh.MaterialOverlay = null;
		}
	}

}
