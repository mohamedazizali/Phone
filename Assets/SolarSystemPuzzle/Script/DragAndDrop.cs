using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public float snapThresholdDistance = 0.5f;
    private RectTransform rectTransform;
    private Vector2 initialPosition;

    public PuzzleManager puzzleManager;
    public GameObject snapPoint;
    private bool isSnapped = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SnapToTarget();
    }

    private void SnapToTarget()
    {
        float distance = Vector2.Distance(rectTransform.position, snapPoint.transform.position);

        if (distance < snapThresholdDistance)
        {
            rectTransform.position = snapPoint.transform.position;
            isSnapped = true;
            puzzleManager.CheckPuzzleCompletion();
        }
        else
        {
            rectTransform.position = initialPosition;
            isSnapped = false;
            puzzleManager.ReduceLives();
        }
    }

    public bool IsSnapped()
    {
        return isSnapped;
    }
}
