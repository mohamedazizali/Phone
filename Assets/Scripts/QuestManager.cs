using UnityEngine;
using DialogueEditor;

public class QuestManager : MonoBehaviour
{
    public GameObject backpackPrefab;
    public Transform backpackSpawnPoint;

    private bool questActive = false;
    public bool quest1Completed = false; // Added variable to track quest completion status
    public bool quest2Completed = false;
    public NPCConversation dialoguetest;
    public InventoryManager InventoryManager;
    [SerializeField]
    public bool planetsCollected = false;

    // Method to accept the quest
    public void AcceptQuest()
    {
        questActive = true;
        quest1Completed = false; // Reset quest completion status when accepting the quest
        //EnableBackpack();
    }
    private void Update()
    {
        CanStartSolar();
    }
    // Method to enable the backpack
    void EnableBackpack()
    {
        backpackPrefab.SetActive(true);
        backpackPrefab.transform.position = backpackSpawnPoint.position;
    }

    // Method to handle player interaction with the backpack
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player interacted with the backpack!");
        if (other.CompareTag("Player") && questActive && !quest1Completed)
        {
            // Perform interaction logic
            CompleteQuest1(); // Complete the quest
        }
    }

    // Method to complete the quest
    public void CompleteQuest1()
    {
        questActive = false;
        quest1Completed = true; // Mark the quest as completed
        // Display completion message or trigger other events
        ConversationManager.Instance.SetBool("completedQuest", true);

    }
    public void CompleteQuest2()
    {
        questActive = false;
        quest2Completed = true; // Mark the quest as completed
        // Display completion message or trigger other events
        //ConversationManager.Instance.SetBool("completedQuest", true);

    }

    // Method to check if the quest is completed
    //public bool IsQuest1Completed()
    //{
    //  return quest1Completed;
    //}
    //public bool IsQuest2Completed()
    //{
    //  return quest2Completed;
    //}
    public bool IsQuestActive()
    {
        return questActive;
    }
    public bool CanStartSolar()
    {
        if (InventoryManager.Instance.checkplanets() == 5)
        {
            planetsCollected = true;
        }
        return planetsCollected;

    }
}
