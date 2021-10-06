using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheDeveloper.AdvancedObjectPool;

public class PlayerMovement : MonoBehaviour
{
    private IPlayerInput playerInput;

    private float _leftLimit, _rightLimit;
    [SerializeField] float _offset = 0.6f;

    private void Awake()
    {
        var height = Camera.main.orthographicSize;
        var ratio = (float)Screen.width / Screen.height;
        var width = ratio * height;

        _leftLimit = -width;
        _rightLimit = width;

    }

    private void Start()
    {
        playerInput = GetComponent<IPlayerInput>();
        playerInput.OnHorizontalUpdate += OnPlayerMove;
    }

    private void OnPlayerMove(float horizontal)
    {
        transform.position += Vector3.right * horizontal * playerInput.Speed * Time.deltaTime;

        var horizontalPos = Mathf.Clamp(transform.position.x, _leftLimit + _offset, _rightLimit - _offset);

        transform.position = new Vector2(horizontalPos, transform.position.y);
    }

    private void OnDestroy()
    {
        playerInput.OnHorizontalUpdate -= OnPlayerMove;
    }

}
