using Godot;
using System;
using System.Collections.Generic;

public class Item
{

	public string Description { get; }
	public string IName { get; }
	public int Price { get; }

	public PackedScene Scene;
	public Texture2D Texture;
	public int count = 0;
	public int slot = -1;

	public Item(string iName, string description, int price, PackedScene scene, Texture2D texture)
	{
		Scene = scene;
		Description = description;
		IName = iName;
		Price = price;
		Texture = texture;
	}

}
