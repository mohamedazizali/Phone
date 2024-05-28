using UnityEngine;

public class ShopItemInitialiser : MonoBehaviour
{
    // Reference to the shop item prefab
    public GameObject shopItemPrefab;

    // Reference to the item associated with the shop item prefab
    public Item item;

    // Function to initialize the shop item
    void Start()
    {
        // Instantiate the shop item prefab
        GameObject newItem = Instantiate(shopItemPrefab, transform);

        // Get the BuyButton component attached to the shop item prefab
        BuyButton buyButton = newItem.GetComponent<BuyButton>();

        // Check if the BuyButton component exists
        if (buyButton != null)
        {
            // Set the item reference for the BuyButton
            buyButton.item = item;
        }
        else
        {
            Debug.LogError("BuyButton component not found on shop item prefab.");
        }
    }
}
