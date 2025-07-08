using Godot;
using System;
using System.Collections.Generic;

public class Item
{

	public string Description { get; }
	public string IName { get; }
	public int Price { get; }

	public PackedScene Scene;

	public Item(string iName, string description, int price, PackedScene scene)
	{
		Scene = scene;
		Description = description;
		IName = iName;
		Price = price;
	}

}
