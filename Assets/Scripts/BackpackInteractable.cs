using UnityEngine;

public class BackpackInteractable : MonoBehaviour
{
    private QuestManager questManager;

    private void Start()
    {
        // Find the QuestManager component in the scene
        questManager = FindObjectOfType<QuestManager>();
    }

   // private void OnTriggerEnter(Collider other)
   // {
    //    Debug.Log("Player interacted with the backpack!");
     //   if (other.CompareTag("Player") && questManager != null && questManager.IsQuestActive() && !questManager.IsQuestCompleted())
    //    {
            // Perform interaction logic
     //       questManager.CompleteQuest(); // Complete the quest
    //        gameObject.SetActive(false); // Deactivate the backpack
    //    }
 //   }
}
