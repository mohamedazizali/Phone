using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject missionCompletePanel;
    public GameObject missionFailedPanel;
    public GameObject[] planets;
    public int maxLives = 3;
    public GameObject[] hearts;
    public Canvas PuzzleCanvas, MainMenu;

    private int lives;
    [SerializeField]
    public bool missionCompleted = false;
    [SerializeField]
    public bool missionFailed = false;
    public startconvo startconvo;

    private void Start()
    {
        lives = maxLives;
        UpdateHeartsUI();
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < lives)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
    private void Update()
    {
        if (missionCompleted == true)
            // Trigger the corresponding conversation in startconvo
            startconvo.OnPuzzleCompleted();
    else if (missionFailed==true)
            startconvo.OnPuzzleFailed();

    }
    private void PuzzleCompleted()
    {
        if(missionCompleted==true)
        // Trigger the corresponding conversation in startconvo
        startconvo.OnPuzzleCompleted();
    }

    // Call this method when the puzzle fails
    private void PuzzleFailed()
    {
        
        // Trigger the corresponding conversation in startconvo
        startconvo.OnPuzzleFailed();
    }
    public void CheckPuzzleCompletion()
    {
        foreach (GameObject planet in planets)
        {
            bool planetSnap = planet.GetComponent<DragAndDrop>().IsSnapped();
            if (!planetSnap)
            {
                return;
            }
        }

        missionCompleted = true;
        missionCompletePanel.SetActive(true);
        StartCoroutine(DeactivateCanvasAfterDelay(4f));
    }

    private IEnumerator DeactivateCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PuzzleCanvas.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
    }

    public void ReduceLives()
    {
        lives--;
        UpdateHeartsUI();

        if (lives <= 0)
        {
            missionFailed = true;
            missionFailedPanel.SetActive(true);
            StartCoroutine(DeactivateCanvasAfterDelay(4f));
        }
    }
}
