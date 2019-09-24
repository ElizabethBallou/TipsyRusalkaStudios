using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();

    private void Start()
    {
        itemList.Add(new Item("Bone", 0, "A large femur bone. It looks like it's been gnawed on."));
        itemList.Add(new Item("Book of Babel", 1, "A book whose pages are filled with absolute gibberish."));
        itemList.Add(new Item("Book of Hours", 2, "A small but elaborate book of prayers."));
        itemList.Add(new Item("Book of Spells", 3, "An old tome filled with archaic runes. They don't resemble any language you've seen before."));
        itemList.Add(new Item("Key", 4, "An old and rusted key of bronze."));
        itemList.Add(new Item("Lock Pick", 5, "Your trusty lock pick. The one Sister Maria gave you at the nunnery."));
        itemList.Add(new Item("Quill", 6, "A gaudy quill made from an ostrich's feather. Alas, there is no ink."));
        itemList.Add(new Item("Ruby Amulet", 7, "A glowing red ruby set in a solid gold pendant."));
        itemList.Add(new Item("Scroll", 8, "A delicate, crumbling scroll written in the hand of the Librarian."));
        itemList.Add(new Item("Vial of Blood", 9, "A little glass vial of dark red blood."));
    }
}
