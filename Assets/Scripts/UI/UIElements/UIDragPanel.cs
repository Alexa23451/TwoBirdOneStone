using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// It is a panel class for manipulating objects using swipes.
/// </summary>
public class UIDragPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action<bool> OnDragStateChange;

    private float _inputMagnitudeTreshold = 0.1f;

    private bool _isDrag;

    private Vector2 _dragStartPosition;
    private Vector2 _dragInputVector;

    private Vector2 _lastDragPosition;

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!_isDrag)
        {
            _dragInputVector = Vector2.zero;
            _dragStartPosition = _lastDragPosition;
        }

        _isDrag = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnDragStateChange?.Invoke(true);

        _dragStartPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _isDrag = true;

        Vector2 direction = eventData.position - _dragStartPosition;
        _dragInputVector = direction;

        _lastDragPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragStateChange?.Invoke(false);
        _dragInputVector = Vector2.zero;
    }

    public Vector2 GetDragInput() => _dragInputVector.normalized;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_dragStartPosition, 10f);
        Gizmos.DrawLine(_dragStartPosition, _dragStartPosition + (_dragInputVector));
    }
#endif
}