using Godot;
using System;

public partial class PlayerMovement : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	public float speed = 0;
	Vector3 velocity;
	public Node3D CameraPivot;
	public float MouseSensitivity = 0.002f;

	private float pitch = 0.0f;
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
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
			pitch = Mathf.Clamp(pitch, -Mathf.Pi / 2, Mathf.Pi / 2);

			if (CameraPivot != null)
			{
				CameraPivot.Rotation = new Vector3(pitch, 0, 0);
			}
		}
	}

	// public void push(float power, float direction)
	// {
	// 	velocity.X = velocity.X - Mathf.Cos(direction) * power;
	// 	velocity.Y = Mathf.Sin(direction) * power;
	// 	Velocity = velocity;
	// 	MoveAndSlide();
	// }

}
