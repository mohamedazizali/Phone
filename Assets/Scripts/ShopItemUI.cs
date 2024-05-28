using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI priceText;
    public Image iconImage;

    public void SetItem(Item item)
    {
        itemNameText.text = item.itemName;
        priceText.text = item.price.ToString();
        iconImage.sprite = item.icon;
    }
}
