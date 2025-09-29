using Godot;
using System;
using System.Collections.Generic;

public class Item
{

	private string Description { get; }
	public string IName { get; }
	private int Price { get; }

	public PackedScene Scene { get; }
	public Texture2D Texture;
	public int count = 1;
	public int slot = -1;

	public Item(string iName, string description, int price)
	{
		Scene = GD.Load<PackedScene>("res://Scenes/Items/"+iName+".tscn");
		Texture = GD.Load<Texture2D>("res://2D assets/itemTextures/"+iName+".png");
		// Scene = scene;
		Description = description;
		IName = iName;
		Price = price;
	}

}
