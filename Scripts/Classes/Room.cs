using Godot;
using System;

public partial class Room : Node
{
	public string RName;

	public string RDescription;
	public Inventory chest;
	public PackedScene Scene;

	public Room(string name, string description, PackedScene scene)
	{
		name = RName;
		description = RDescription;
		Scene = scene;
	}

}
