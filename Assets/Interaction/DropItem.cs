using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class DropItem : MonoBehaviour
{
    public Transform DropPoint;
    public Item item;
    public ScreenCapture capture;
    public Image imageholder;
    public GameObject polaroidholder;
    private Transform polaroidholderChild;


    public void Start()
    {


        //polaroidholderChild= polaroidholder.transform.Find("PhotoDisplayArea");
        //imageholder = polaroidholderChild.gameObject.GetComponent<Image>();

    }

    public void DropIt()
    {
        if (item.prefabTag == "Picture")
        {

            polaroidholder.SetActive(true);




            imageholder.sprite = item.icon;

        }
        else
        {
            DropPoint = GameObject.FindGameObjectWithTag("Drop").transform;
            GameObject droppedItem = Instantiate(item.prefab, DropPoint.position, Quaternion.identity);

            // Apply force to make the item jump out of the player
            Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(DropPoint.forward * 10f, ForceMode.Impulse);
                InventoryManager.Instance.Remove(item);
                Destroy(gameObject);
            }
        }
    }



    public void showwTof()
    {
        capture.ShowPhoto();
    }
}
