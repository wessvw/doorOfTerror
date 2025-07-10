using Godot;
using System;
using System.Collections.Generic;
public class Inventory
{
	private Dictionary<string, Item> contents;


	public Dictionary<string, Item> Contents
	{
		get { return contents; }
		set { contents = value; }
	}

	public Inventory()
	{
		this.contents = new Dictionary<string, Item>();
	}


	public string PrintInvItems()
	{
		string item = "";
		if (contents.Count == 0)
		{
			item = "There are no items in your inventory";
		}
		else
		{
			foreach (string name in contents.Keys)
			{
				item += name + " ";
				item += ", ";
			}
		}
		return item;
	}

	public bool Put(string itemName, Item item)
	{
		contents.Add(itemName, item);
		return true;
	}

	public Item Get(string itemName)
	{
		if (contents.ContainsKey(itemName))
		{
			Item gottenitem = contents[itemName];
			// contents.Remove(itemName);
			return gottenitem;
		}
		return null;
	}
}
