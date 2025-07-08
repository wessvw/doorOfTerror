using Godot;
using System;

public partial class PlayerMovement : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	public float speed = 0;
	Vector3 velocity;
	public Node3D cameraPivot;
	public float MouseSensitivity = 0.002f;
	private StaticBody3D door;	private Vector3 doorpos;
	public Player aplayer;

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

	}

	public override void _Process(double delta)
	{
		aplayer.Position = GlobalPosition;
		// GD.Print(player.Backpack);

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
			RotateY(-motion.Relative.X * MouseSensitivity);

			pitch -= motion.Relative.Y * MouseSensitivity;
			pitch = Mathf.Clamp(pitch, Mathf.DegToRad(-40), Mathf.DegToRad(40));

			if (cameraPivot != null)
			{
				cameraPivot.Rotation = new Vector3(pitch, 0, 0);
			}
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
	}
}