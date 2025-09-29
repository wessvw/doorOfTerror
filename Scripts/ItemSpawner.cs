using Godot;
using System;
using System.Collections.Generic;

public partial class ItemSpawner : Node
{
	private Godot.Collections.Array<Node> spawnpoints;
	// items
	public Item cube;
	public Item key;
	// item textures
	[Export] Texture2D cubeTexture;
	Dictionary<string, Texture2D> textures;
	Dictionary<string, PackedScene> scenes;
	public override void _Ready()
	{
		spawnpoints = GetChildren();
		// LoadPngTextures("res://2D assets/itemTextures/");
		// LoadScenes("res://Scenes/Items/");
		// foreach (var key in scenes.Keys)
		// {
		// 	GD.Print("Loaded scene: " + key);
		// }
		// foreach (var key in textures.Keys)
		// {
		// 	GD.Print("Loaded texture: " + key);
		// }

		GD.Print("aaa");
		//cube item
		cube = new Item("cube", "just a regular cube", 1);
		//key item
		key = new Item("key", "a key to open something", 1);
		//document item

		// SpawnItems();
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

	// private Dictionary<string, Texture2D> LoadPngTextures(string path)
	// {
	// 	GD.Print($"Trying to open: {path}");
	// 	textures = new Dictionary<string, Texture2D>();

	// 	using DirAccess dir = DirAccess.Open(path);
	// 	if (dir == null)
	// 	{
	// 		GD.PrintErr($"Could not open directory: {path}");
	// 		return textures;
	// 	}

	// 	dir.ListDirBegin();

	// 	while (true)
	// 	{
	// 		string fileName = dir.GetNext();
	// 		if (fileName == "")
	// 			break;

	// 		if (dir.CurrentIsDir())
	// 			continue;

	// 		if (fileName.EndsWith(".png"))
	// 		{
	// 			string fullPath = $"{path}{fileName}";
	// 			GD.Print($"Loading: {fullPath}");
	// 			Texture2D texture = GD.Load<Texture2D>(fullPath);
	// 			if (texture != null)
	// 			{
	// 				string key = fileName.GetBaseName();
	// 				textures[key] = texture;
	// 			}
	// 			else
	// 			{
	// 				GD.PrintErr($"Failed to load texture at: {fullPath}");
	// 			}
	// 		}
	// 	}

	// 	dir.ListDirEnd();

	// 	return textures;
	// }


	// private Dictionary<string, PackedScene> LoadScenes(string path)
	// {
	// 	scenes = new Dictionary<string, PackedScene>();

	// 	using DirAccess dir = DirAccess.Open(path);
	// 	if (dir == null)
	// 	{
	// 		//GD.PrintErr($"Could not open directory: {path}");
	// 		return scenes;
	// 	}

	// 	dir.ListDirBegin();

	// 	while (true)
	// 	{
	// 		string fileName = dir.GetNext();
	// 		if (fileName == "")
	// 			break;

	// 		if (dir.CurrentIsDir())
	// 			continue; // skip folders

	// 		if (fileName.EndsWith(".tscn"))
	// 		{
	// 			string fullPath = $"{path}{fileName}";
	// 			PackedScene scene = GD.Load<PackedScene>(fullPath);
	// 			if (scene != null)
	// 			{
	// 				// Remove extension for key
	// 				string key = fileName.GetBaseName();
	// 				scenes[key] = scene;
	// 			}
	// 		}
	// 	}

	// 	dir.ListDirEnd();

	// 	return scenes;
	// }
}
