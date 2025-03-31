using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIRaycaster : MonoBehaviour
{
    private static UIRaycaster instance;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    private PointerEventData pointerEventData;

    private void Awake()
    {
        instance = this;
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    public static bool IsPointerOverUI(Vector2 screenPosition)
    {
        if (instance == null || instance.raycaster == null || instance.eventSystem == null)
            return false;

        instance.pointerEventData = new PointerEventData(instance.eventSystem)
        {
            position = screenPosition
        };

        var results = new List<RaycastResult>();
        instance.raycaster.Raycast(instance.pointerEventData, results);

        return results.Count > 0; 
    }
}

