using TMPro;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    // Reference to the item associated with this button
    public Item item;
    public TextMeshProUGUI coinText;

    // Function to handle the buy action when the button is clicked
    public void BuyItem()
    {
        Debug.Log("Buying item: " + item.itemName);

        // Check if the player has enough coins to buy the item
        if (GameManager.Instance.coinCount >= item.price)
        {
            // Subtract the item price from the player's coins
            GameManager.Instance.coinCount -= item.price;

            Debug.Log("Coins after deduction: " + GameManager.Instance.coinCount);

            // Update the coin count text
            CoinUIManager.Instance.UpdateCoinCountUI(GameManager.Instance.coinCount);

            // Add the item to the player's inventory
            InventoryManager.Instance.Add(item);

            // Remove the item from the shop
            shopManager.Instance.RemoveItem(item);

            // Destroy the button (optional)
            Destroy(gameObject);
        }
        else
        {
            // Display a message indicating insufficient funds (optional)
            Debug.Log("Insufficient funds to buy " + item.itemName);
        }
    }
}
