using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class startconvo : MonoBehaviour
{
    [SerializeField] private NPCConversation myConv;
    [SerializeField] private NPCConversation myConv2;
    [SerializeField] private NPCConversation myConv3;
    [SerializeField] private NPCConversation MissionCompletedDialogue;
    [SerializeField] private NPCConversation MissionFailedDialogue;
    [SerializeField] public bool Quest;
    public QuestManager QuestManager;
    public bool quest1completed;
    public bool quest2completed;
    public QuestUIManager Quests;
    private bool inRange = false;
    [Header("Quest SO")]
    public Quest backpack1, backpack2, Polaroid1, Polaroid2, Solar1;
    public GameObject mainMenuCanvas;
    public GameObject puzzleCanvas;
    private bool isMainMenuActive = true; // Track the current state
    public PuzzleManager PuzzleManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("enter");
        }
    }

    public void setActiveQest()
    { Quest = true; }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    private void Update()
    {

        // Example condition to toggle the canvas (you can replace this with your own condition)
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleCanvas();
        }
        if (inRange && Input.GetKeyUp(KeyCode.E))
        {
            if (QuestManager != null)
            {
                quest1completed = QuestManager.quest1Completed; //on dialogue start yekhou value 
                quest2completed = QuestManager.quest2Completed; //on dialogue start yekhou value 
            }
            else
            {
                Debug.LogWarning("QuestManager reference is not set.");
            }
            if (!quest1completed && !quest2completed && QuestManager.planetsCollected == false)
            {
                ConversationManager.Instance.StartConversation(myConv);
                //Quests.AddQuest(backpack1);
                //Quests.ListQuests();
            }
            else if (!quest1completed && !quest2completed && QuestManager.planetsCollected == true)
            {
                //Quests.RemoveQuest(backpack1);

                ConversationManager.Instance.StartConversation(myConv2);

                //if (PuzzleManager.missionCompleted == true)
                //{
                //    ConversationManager.Instance.StartConversation(MissionCompletedDialogue);
                //}else if (PuzzleManager.missionFailed == true)
                //{
                //    ConversationManager.Instance.StartConversation(MissionFailedDialogue);
                //}
                //Quests.AddQuest(Solar1);
                //Quests.ListQuests();
            }
            else if (quest1completed && !quest2completed)
            {

                Quests.RemoveQuest(backpack1);
                Quests.RemoveQuest(backpack2);

                ConversationManager.Instance.StartConversation(myConv2);

                Quests.AddQuest(Polaroid1);
                Quests.ListQuests();


            }
            else
            {
                ConversationManager.Instance.StartConversation(myConv3);
            }

            //ConversationManager.Instance.SetBool("quest", Quest);
            //ConversationManager.Instance.SetBool("completedQuest", questcompleted);
        }
        if (quest1completed)
        {
            if (InventoryManager.Instance.HasItem("backpack") == true)
            {
                InventoryManager.Instance.RemoveWithName("backpack");
                InventoryManager.Instance.ListItems();
            }

        }
        if (quest2completed)
        {
            Quests.RemoveQuest(Polaroid2);
            Quests.ListQuests();
        }
    }
    public void ToggleCanvas()
    {
        isMainMenuActive = !isMainMenuActive; // Toggle the state
        mainMenuCanvas.SetActive(!mainMenuCanvas.activeSelf);
        puzzleCanvas.SetActive(!puzzleCanvas.activeSelf);
    }
    public void OnPuzzleCompleted()
    {
        ConversationManager.Instance.StartConversation(MissionCompletedDialogue);
    }

    // Called when the puzzle fails
    public void OnPuzzleFailed()
    {
        ConversationManager.Instance.StartConversation(MissionFailedDialogue);
    }
}