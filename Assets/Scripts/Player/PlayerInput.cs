using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPlayerInput
{
    event Action<float> OnHorizontalUpdate;

    event Action OnSpaceUpdate;
    float Speed { get; }
}

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    [SerializeField] private float _speed = 0f;
    public float Speed => _speed;

    public event Action<float> OnHorizontalUpdate;
    public event Action<float> OnVerticalUpdate;
    public event Action OnSpaceUpdate;

    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0f)
        {
            OnHorizontalUpdate?.Invoke(Input.GetAxis("Horizontal"));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpaceUpdate?.Invoke();
        }
    }
}
