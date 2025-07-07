using Godot;
using System;

public partial class Player : Node
{
	private int health = 100;
	private int sanity = 100;
	private Vector3 position;


	public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }
}
