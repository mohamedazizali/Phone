using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isPaused = false;

    private void Start()
    {
        // Ensure the pause menu is disabled when the game starts
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        // Toggle pause menu and cursor visibility when the "P" key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        isPaused = !isPaused;

        // Toggle pause menu visibility
        pauseMenu.SetActive(isPaused);
        Time.timeScale = 0;
        // Toggle cursor visibility based on pause state
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    public void ResumeGame()
    {
        // Resume the game by disabling the pause menu
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        // Hide the cursor and lock it back to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene"); 
    }
}
