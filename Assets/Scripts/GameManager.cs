using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int coinCount = 0;

    // Define delegate and event for coin count change
    public delegate void CoinCountChanged(int newCoinCount);
    public static event CoinCountChanged OnCoinCountChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if(!targetGameObject.activeInHierarchy)
            ActivateForDuration(4);
        }
    }

    // Function to update the coin count
    public void UpdateCoinCount(int amount)
    {
        coinCount += amount;

        // Trigger the event when coin count changes
        if (OnCoinCountChanged != null)
        {
            OnCoinCountChanged(coinCount);
        }
    }
    public GameObject targetGameObject;

    public void ActivateForDuration(float duration)
    {
        // Start the coroutine to handle the activation
        StartCoroutine(ActivateTemporarily(targetGameObject, duration));
    }

    private IEnumerator ActivateTemporarily(GameObject gameObject, float duration)
    {
        // Activate the GameObject
        gameObject.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Deactivate the GameObject
        gameObject.SetActive(false);
    }
}
