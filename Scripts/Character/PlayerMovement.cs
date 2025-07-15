using Godot;
using System;

public partial class PlayerMovement : CharacterBody3D
{
	private const float Speed = 5.0f;
	private const float JumpVelocity = 4.5f;
	private float MouseSensitivity = 0.002f;
	Vector3 velocity;
	private Node3D cameraPivot;
	private StaticBody3D door;
	public Player aplayer;
	private Inventoryui invUI;

	private float pitch = 0.0f;

	public PlayerMovement()
	{
		aplayer = new Player();
	}
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		cameraPivot = FindChild("CameraPivot", true, false) as Node3D;
		door = GetTree().Root.FindChild("doorObject", true, false) as StaticBody3D;
		invUI = GetTree().Root.FindChild("InventoryUI", true, false) as Inventoryui;

	}

	public override void _Process(double delta)
	{
		aplayer.Position = GlobalPosition;
		// GD.Print(player.Backpack);
	}

	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}
		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}
		Vector2 inputDir = Input.GetVector("left", "right", "forward", "backward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(velocity.Z, 0, Speed);
		}


		Velocity = velocity;
		MoveAndSlide();

	}

	// mouse camera movement
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion motion)
		{
			if (!invUI.Visible)
			{
				RotateY(-motion.Relative.X * MouseSensitivity);

				pitch -= motion.Relative.Y * MouseSensitivity;
				pitch = Mathf.Clamp(pitch, Mathf.DegToRad(-40), Mathf.DegToRad(40));

				if (cameraPivot != null)
				{
					cameraPivot.Rotation = new Vector3(pitch, 0, 0);
				}
			}
		}
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (Input.IsActionJustPressed("leftClick"))
			{
				// invUI.useSelectedItem();
			}
		}
		if (Input.IsActionJustPressed("accept"))
		{
			GD.Print(door.GlobalPosition);
			if (door != null && isPlayerNearDoor())
			{
				// door.GetNode<Sprite3D>("eButton").Visible = true;
			}
			else
			{
				GD.Print("player is not close enough");
			}
		}
		if (Input.IsActionJustPressed("escape"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}

	public bool isPlayerNearDoor()
	{
		float distance = GlobalPosition.DistanceTo(door.GlobalPosition);
		return distance <= 5f;
	}

}
public partial class Player : Node
{
	public Room CurrentRoom { get; set; }
	private int health = 100;
	private int sanity = 100;
	private Vector3 position;
	private Inventory backpack;


	public Vector3 Position
	{
		get { return position; }
		set { position = value; }
	}

	public Inventory Backpack
	{
		get { return backpack; }
		set { backpack = value; }
	}

	public int Sanity
	{
		get { return sanity; }
		set { sanity = value; }
	}

	public Player()
	{
		CurrentRoom = null;
		health = 100;
		backpack = new Inventory();
		sanity = 100;
	}
}