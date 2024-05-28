
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public float value;
    public Sprite icon;
    public GameObject prefab;
    public string prefabTag;
    public int price;

    // Constructor with parameters
    public Item(int id, string itemName, int value, Sprite icon,int price, GameObject prefab)
    {
        this.id = id;
        this.itemName = itemName;
        this.value = value;
        this.icon = icon;
        this.prefab = prefab;
        this.prefabTag = prefabTag; // Assign the prefab tag
        this.price = price;
    }

    // Constructor without parameters (default constructor)
    public Item()
    {
        // Default values
        id = 0;
        itemName = "New Item";
        value = 0;
        icon = null;
        prefab = null;
        prefabTag = ""; // Initialize the prefab tag
        price = 0;
    }
}