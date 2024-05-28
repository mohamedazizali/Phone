using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUIManager : MonoBehaviour
{
    public List<Quest> Quests = new List<Quest>();

    // References to UI elements
    [SerializeField]
    public Transform QuestContent;
    public GameObject NotificationPrefab;
    public static QuestUIManager Instance;
    private void Start()
    {
        //gameObject.SetActive(false);
    }

    // Add a notification to the list
    public void AddQuest(Quest Quest)
    {
        Quests.Add(Quest);
    }

    // Remove a notification from the list
    public void RemoveQuest(Quest notification)
    {
        Quests.Remove(notification);
    }

    // List all notifications
    public void ListQuests()
    {
        // Destroy existing UI elements
        foreach (Transform notificationItem in QuestContent)
        {
            Destroy(notificationItem.gameObject);
        }

        // Create UI elements for each notification
        foreach (var quest in Quests)
        {
            GameObject obj = Instantiate(NotificationPrefab, QuestContent);
            var notificationTitleText = obj.transform.Find("QuestName").GetComponent<TextMeshProUGUI>();
            var notificationMessageText = obj.transform.Find("QuestDesc").GetComponent<TextMeshProUGUI>();

            // Set notification title and message
            notificationTitleText.text = quest.QuestName;
            notificationMessageText.text = quest.QuestDescription;

            // Additional setup for notification icon, timestamp, etc.
        }
    }
}
