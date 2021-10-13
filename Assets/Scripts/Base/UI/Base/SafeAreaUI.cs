using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SaveAreaUI : MonoBehaviour
{
    private void Awake()
    {
        RectTransform rectTr = GetComponent<RectTransform>();
        Rect saveArea = Screen.safeArea;

        Vector2 anchorMin = saveArea.position;
        Vector2 anchorMax = anchorMin + saveArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTr.anchorMin = anchorMin;
        rectTr.anchorMax = anchorMax;

    }
}
