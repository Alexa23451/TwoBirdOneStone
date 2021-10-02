using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The base class of the mini-game control UI.
/// </summary>
public abstract class BaseTaskControlPanel : BasePanel, ITouchscreenPanel
{
    public Action OnExitButton;

    /// <summary>
    /// Called when the rotation changes
    /// </summary>
    public Action<Vector2> OnInput { get; set; }

    public bool IsDrag => _isDrag;

    //EDITOR FIELDS
    [SerializeField]
    private UIJoystick joystick;
    [SerializeField]
    private UIDragPanel dragPanel;

    [SerializeField] protected Button _exitButton;

    internal Vector2 _inputVector;

    private bool _isDrag;

    private void Start()
    {
        _exitButton.onClick.AddListener(ExitClick);
        dragPanel.OnDragStateChange += OnDragStateChange;
    }

    private void OnDragStateChange(bool isDrag) => _isDrag = isDrag;

    private void ExitClick()
    {
        OnExitButton?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }

    // Update rotate input
    internal virtual void UpdateInput()
    {
        _inputVector = new Vector2(joystick.Input.x, joystick.Input.y);

#if UNITY_EDITOR
        EditorInputs();        
#endif

        if (_inputVector!=Vector2.zero)
            OnInput?.Invoke(_inputVector);
    }

#if UNITY_EDITOR
    private void EditorInputs()
    {
        _inputVector = new Vector2(_inputVector.x + Input.GetAxis("Horizontal"),
                                   _inputVector.y + Input.GetAxis("Vertical"));
    }
#endif

    // Gets input from button.
    protected float GetButtonInput(UIButton button) => (button == null) ? 0f : button.input;

    public Vector2 GetInput() => _inputVector;

    public Vector2 GetTouchscreenInput() => dragPanel.GetDragInput();
}