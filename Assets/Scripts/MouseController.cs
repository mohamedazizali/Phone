using UnityEngine;
using DialogueEditor;

public class MouseController : MonoBehaviour
{
    private void OnEnable()
    {
        ConversationManager.OnConversationStarted += EnableMouse;
        ConversationManager.OnConversationEnded += DisableMouse;
    }

    private void OnDisable()
    {
        ConversationManager.OnConversationStarted -= EnableMouse;
        ConversationManager.OnConversationEnded -= DisableMouse;
    }

    private void EnableMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void DisableMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
