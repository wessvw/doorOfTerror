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
	private StaticBody3D door;

	private float pitch = 0.0f;
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		cameraPivot = FindChild("CameraPivot", true, false) as Node3D;
		door = GetTree().Root.FindChild("door", true, false) as StaticBody3D;
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
		if (Input.IsActionJustPressed("accept"))
		{
			if (door != null && isPlayerNearDoor())
			{
				GD.Print("player is near the door");
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

	private bool isPlayerNearDoor()
	{
		float distance = GlobalPosition.DistanceTo(door.GlobalPosition);
		return distance <= 5f;
	}

	// public void push(float power, float direction)
	// {
	// 	velocity.X = velocity.X - Mathf.Cos(direction) * power;
	// 	velocity.Y = Mathf.Sin(direction) * power;
	// 	Velocity = velocity;
	// 	MoveAndSlide();
	// }

}
