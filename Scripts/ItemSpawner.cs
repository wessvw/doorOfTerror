using Godot;
using System;

public partial class ItemSpawner : Node
{
	private Godot.Collections.Array<Node> spawnpoints;
	public Item cube;
	
	[Export] Texture2D cubeTexture;
	public override void _Ready()
	{
		spawnpoints = GetChildren();
		//cube item
		PackedScene cubeScene = GD.Load<PackedScene>("res://Scenes/Items/cube.tscn");
		cube = new Item("cube", "just a regular cube", 1, cubeScene, cubeTexture);
		//key item
		//document item
		SpawnItems();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SpawnItems()
	{
		for (int i = 0; i < (spawnpoints.Count); i++)
		{
			Node3D cubeInstance = (Node3D)cube.Scene.Instantiate();
			Node3D spawnPoint = (Node3D)spawnpoints[i];
			AddChild(cubeInstance);
			cubeInstance.GlobalPosition = spawnPoint.GlobalPosition;
		}
	}
}
