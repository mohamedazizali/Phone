using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class CoinUIManager : MonoBehaviour
{
    public static CoinUIManager Instance;

    public TextMeshProUGUI coinCountText;
    private int currentCoinCount = 0;
    private int totalPoints = 0;
    public InventoryManager inventoryManager, inventoryManager1;
    void Awake()
    {
        // Ensure there is only one instance of CoinUIManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Subscribe to the event for coin count change
        GameManager.OnCoinCountChanged += UpdateCoinCountUI;
    }

    public void UpdateCoinCountUI(int newCoinCount)
    {
        // Update the displayed coin count
        // Add the new coins to the current coin count
        currentCoinCount += newCoinCount;

        // Update the displayed coin count
        coinCountText.text = "Coins: " + currentCoinCount;
    }
    public void SubtractCoins(int coinsToSubtract)
    {
        // Subtract the coins from the current coin count
        currentCoinCount -= coinsToSubtract;

        // Ensure the coin count does not go below zero
        if (currentCoinCount < 0)
        {
            currentCoinCount = 0;
        }

        // Update the displayed coin count
        coinCountText.text = "Coins: " + currentCoinCount;
    }
    public int AddExponentialPoints()
    {
        int collectedData = 0;
        collectedData += inventoryManager.checkcollectedProves();
        collectedData += inventoryManager1.checkcollectedProves();
        int basePoints = 10; // Adjust the base points as needed
        int exponent = 2; // Adjust the exponent as needed
        int pointsToAdd = (int)(basePoints * Mathf.Pow(collectedData, exponent));
        Debug.Log(pointsToAdd);
        return pointsToAdd;
    }
    public void DirecteurADDpoints()
    {
        int pointstoadd = AddExponentialPoints();
        UpdateCoinCountUI(pointstoadd);


    }
    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        GameManager.OnCoinCountChanged -= UpdateCoinCountUI;
    }
}
