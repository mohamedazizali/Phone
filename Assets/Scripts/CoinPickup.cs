using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Increment coin count
            GameManager.Instance.UpdateCoinCount(1);

            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}
