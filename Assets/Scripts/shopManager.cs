using System.Collections.Generic;
using UnityEngine;

public class shopManager : MonoBehaviour
{
    // Static reference to the shopManager instance
    public static shopManager Instance;

    // List of items for this shop
    public List<Item> shopItems = new List<Item>();

    // References to UI elements
    public Transform shopItemContent;
    public GameObject shopItemPrefab;

    void Awake()
    {
        // Ensure only one instance of shopManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PopulateShop();
    }

    // Populate the shop with items
    void PopulateShop()
    {
        foreach (var item in shopItems)
        {
            GameObject shopItemObject = Instantiate(shopItemPrefab, shopItemContent);
            BuyButton buyButton = shopItemObject.GetComponent<BuyButton>();

            if (buyButton != null)
            {
                buyButton.item = item;
            }
            else
            {
                Debug.LogError("BuyButton component not found on shop item prefab.");
            }

            ShopItemUI shopItemUI = shopItemObject.GetComponent<ShopItemUI>();
            shopItemUI.SetItem(item);
        }
    }

    // Add an item to the shop
    public void AddItem(Item item)
    {
        shopItems.Add(item);
    }

    // Remove an item from the shop
    public void RemoveItem(Item item)
    {
        shopItems.Remove(item);
    }
}
