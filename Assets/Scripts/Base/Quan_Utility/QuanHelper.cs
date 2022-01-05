using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class QuanHelper
{
    private static Camera _camera;
    public static Camera Camera
    {
        get
        {
            if (_camera == null) _camera = Camera.main;
            return _camera;
        }
    }

    private static PointerEventData _eventDataCurrentPosition;
    private static List<RaycastResult> _result;

    public static bool IsOverUI()
    {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current) 
        { position = Input.mousePosition };

        _result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPosition, _result);
        return _result.Count > 0;
    }

    //for find world pos in canvas
    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
        return result;
    }


    public static void DeleteChildren(this Transform transform)
    {
        foreach(Transform t in transform)
        {
            Object.Destroy(t.gameObject);
        }
    }



}
