using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public string itemName;
    public int itemID;
    public string itemDescription;
    public Sprite itemIcon;

    public Item(string name, int id, string description)
    {
        itemName = name;
        itemID = id;
        itemDescription = description;
        itemIcon = Resources.Load<Sprite>("Prefabs/Icons/" + name);
    }

    public Item()
    {
        //slot constructor
    }
}
