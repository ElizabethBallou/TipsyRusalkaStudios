using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    
    public int slotsX = 4;
    public int slotsY = 5;
    public GameObject inventoryHolder;
    public GameObject tooltip;
    
    private ItemDatabase _database;
    private int _prevIndex;
    private int _currentIndex;
    private GUIStyle _style;
    private Vector2 _size;
    private bool _showInventory;
    private bool _showTooltip;
    private bool _draggingItem;
    private Item _draggedItem;
    private string _tooltip;
    private Sprite _currentItem;
    private TextMeshProUGUI _tooltipText;

    public static Inventory instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _tooltipText = tooltip.GetComponentInChildren<TextMeshProUGUI>();

        //for the grid defined by the values slotsX and slotsY
        for (int i = 0; i < (slotsX * slotsY); i++)
        {
            //add a slot
            slots.Add(new Item());
            //add an empty item constructor)
            inventory.Add(new Item());
        }
        
        //get our reference to our Item database
        _database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        
        //Add the following items to our inventory from the start
        AddItem(2);
        AddItem(5);
        AddItem(6);
        AddItem(8);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            _showInventory = !_showInventory;
            if(_showInventory)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
        
        if (_showInventory)
        {
            if (GetCurrentItemIcon())
            {
                DragItem();
                ShowTooltip();
            }
            else
            {
                //Move items into empty slots - need index to reference empty slot.
                /*if (Input.GetMouseButtonUp(0) && _draggingItem)
                {
                    //the item we're currently dragging goes into the slot our cursor is hovering over
                    inventory[_currentIndex] = _draggedItem;
                    _draggingItem = false;
                    _draggedItem = null;
                }*/
                tooltip.SetActive(false);
            }
        }
    }

    void OpenInventory()
    {
        int i = 0;

        //for every row in our inventory
        for (int y = 0; y < slotsY; y++)
        {
            //and for every slot in every row
            for (int x = 0; x < slotsX; x++)
            {
                //create an empty slot and child it to the inventory
                Image slot = Instantiate(Resources.Load<Image>("Prefabs/Inventory/Slot"), inventoryHolder.transform);
                //set empty slots to correspond to empty item constructors
                slots[i] = inventory[i];
                
                //if a slot is NOT empty
                if (slots[i].itemName != null)
                {
                    //create an item prefab in that slot and parent it to the slot
                    Image icon = Instantiate(Resources.Load<Image>("Prefabs/Inventory/Icon"), slot.transform);
                    //set the sprite equal to the slots current item icon
                    icon.sprite = slots[i].itemIcon;
                }
                i++;
            }
        }
    }
    
    void CloseInventory()
    {
        for (int i = 0; i < inventoryHolder.transform.childCount; i++)
        {
            Destroy(inventoryHolder.transform.GetChild(i).gameObject);
        }
    }

    bool GetCurrentItemIcon()
    {
        var mousePos = Input.mousePosition;
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward, 1000f);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name == "Icon(Clone)")
            {
                _currentItem = hit.collider.GetComponent<Image>().sprite;
                return true;
            }
        }
        return false;
    }
    
    string CreateTooltipText(Item item)
    {
        _tooltip = "<color=#E9E9E9><b>" + item.itemName + "</b></color>\n\n" +
                   "<color=#39AB3E><b>" + item.itemDescription + "</b></color>";
        return _tooltip;
    }

    public void ShowTooltip()
    {
        //show the tooltip based on the information defined by our CreateTooltip Function
        for (int i = 0; i < inventory.Count; i++)
        {
            if (_currentItem == inventory[i].itemIcon)
            {
                _tooltipText.text = CreateTooltipText(inventory[i]);
                _currentIndex = i;
                //Debug.Log(inventory[i].itemName);
            }
        }

        var rt = tooltip.GetComponent<RectTransform>();
        tooltip.SetActive(true);
        tooltip.transform.position = Input.mousePosition + new Vector3(rt.rect.width * 1.75f, -rt.rect.height * 1.75f, 0.0f);
    }
    
    void DragItem()
    {
        if (Input.GetMouseButtonDown(0) && !_draggingItem)
        {
            _draggingItem = true;
            //store the index of the item we're dragging
            _prevIndex = _currentIndex;
            //set the dragged item to the item we clicked on
            _draggedItem = inventory[_currentIndex];
            //put an empty item constructor in the slot we've just emptied
            inventory[_currentIndex] = new Item();
        }
        //or if the left mouse button is released/clicked AND we're already dragging an item
        if(Input.GetMouseButtonUp(0) && _draggingItem)
        {
            //the item our cursor is hovering over goes into the slot where we previously grabbed the item we're dragging
            inventory[_prevIndex] = inventory[_currentIndex];
            //the item we're currently dragging goes into the slot our cursor is hovering over
            inventory[_currentIndex] = _draggedItem;
            _draggingItem = false;
            _draggedItem = null;
            CloseInventory();
            OpenInventory();
        }
    }
    
    public void AddItem(int id)
    {    
        //loop through every element in our inventory
        for (int i = 0; i < inventory.Count; i++)
        {
            //when you find an empty element
            if (inventory[i].itemName == null)
            {
                //loop through every item in our database
                for (int j = 0; j < _database.itemList.Count; j++)
                {
                    //when you find the item in our database whose id matches the id defined by our parameter
                    if (_database.itemList[j].itemID == id)
                    {
                        //add that item into the empty slot
                        inventory[i] = _database.itemList[j];   
                    }
                }
                break;
            }
        }
    }

    void RemoveItem(int id)
    {
        //loop through every element in our inventory
        for (int i = 0; i < inventory.Count; i++)
        {
            //when you find the item in our inventory whose id matches the id defined by our parameter
            if (inventory[i].itemID == id)
            {
                //replace that item with an empty item constructor
                inventory[i] = new Item();
                break;
            }
        }
    }

    void UseItem(Item item, int slot, bool deleteItem)
    {
        //this is an example of how this function could be used to different affect if we had a Stats class (or something similar) that was affected
        //by different kinds of items
        switch (item.itemID)
        {
            case 4:
                /*PlayerStats.IncreaseStat(3, 15, 50f)*/
                Debug.Log("Used consumable: " + item.itemName);
                break;
            default:
                break;
        }
        
        //if the item is "deletable"
        if (deleteItem)
        {
            //replace the item defined by our parameters with an empty item constructor
            inventory[slot] = new Item();
        }
    }

    bool InventoryContains(int id)
    {
        bool result = false;
        //loop through every element in our inventory
        for (int i = 0; i < inventory.Count; i++)
        {
            //if the id of an item matches the id defined by our parameter than result is true
            result = inventory[i].itemID == id;
            if (result)
            {
                break;
            }
        }
        return result;
    }

}
