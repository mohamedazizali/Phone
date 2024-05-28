using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // List of items for this inventory
    public List<Item> Items = new List<Item>();

    // References to UI elements
    public Transform ItemContent;
    public GameObject InventoryItem;
    private GameObject polaroid;
    private Transform PhotoFrameBG, PhotoFrameBGChild, PhotoFrameBGBigChild;
    private void Start()
    {
        gameObject.SetActive(false);
        polaroid = GameObject.FindGameObjectWithTag("polaroidHolder");
        PhotoFrameBG = polaroid.transform.Find("PhotoFrameBG");



        PhotoFrameBGBigChild = polaroid.transform.Find("PhotoFrameBG/PhotoHolderMask/PhotoDisplayArea");
    }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Ensure this instance is set as the Instance only if it's not already set
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Add an item to this inventory
    public void Add(Item item)
    {
        Items.Add(item);
    }

    // Remove an item from this inventory
    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    // List all items in this inventory
    public void ListItems()
    {
        // Destroy existing UI elements
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        // Create UI elements for each item
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            // Set item name and icon
            itemIcon.sprite = item.icon;
            itemName.text = item.itemName;
            obj.GetComponent<DropItem>().polaroidholder = PhotoFrameBG.gameObject;

            obj.GetComponent<DropItem>().imageholder = PhotoFrameBGBigChild.gameObject.GetComponent<Image>();
            // Set the item for the DropItem component
            obj.GetComponent<DropItem>().item = item;
        }
    }

    // Check if this inventory contains an item with a given name
    public bool HasItem(string itemName)
    {
        foreach (var item in Items)
        {
            if (item.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }

    // Remove an item from this inventory by its name
    public void RemoveWithName(string itemName)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemName == itemName)
            {
                Items.RemoveAt(i);
                return;
            }
        }
    }
    public float CalculateTotalValue()
    {
        float totalValue = 0f;
        foreach (var item in Items)
        {
            totalValue += item.value;
        }
        return totalValue;
    }
    int i = 1;
    // Add a screenshot item to this inventory
    public void AddScreenshotItem(Sprite screenshotSprite, string prefabTag, float val)
    {

        Item screenshotItem = ScriptableObject.CreateInstance<Item>();
        screenshotItem.itemName = "Screenshot_" + i.ToString();
        i++;
        screenshotItem.icon = screenshotSprite;
        Add(screenshotItem);
        screenshotItem.prefabTag = prefabTag;
        screenshotItem.value = val;
    }
    public int checkplanets()
    {
        int i = 0;
        foreach (var item in Items)
        {
            if (item.prefabTag == "Planet")
            {
                i++;
            }
        }
        return i;
    }
    public int checkcollectedProves()
    {
        int i = 0;
        foreach (var item in Items)
        {

            if(item.prefabTag =="Picture" || item.prefabTag == "Paper")
            {
i++;
            }
        }
        return i;
    }
    public void RemoveItemsWithTag(string tag)
    {
        // Create a list to store the items to be removed
        List<Item> itemsToRemove = new List<Item>();

        // Find items with the specified tag and add them to the list
        foreach (var item in Items)
        {
            if (item.prefabTag == tag)
            {
                itemsToRemove.Add(item);
            }
        }

        // Remove items from the inventory
        foreach (var itemToRemove in itemsToRemove)
        {
            Items.Remove(itemToRemove);
        }
    }


    // Reference to the active instance of InventoryManager
    public static InventoryManager Instance;
}
