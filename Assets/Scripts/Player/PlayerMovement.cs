using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _leftLimit, _rightLimit;
    private float playerInputSpeed;
    [SerializeField] float _offset = 1f;

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
        UIManager.Instance.GetPanel<GameplayPanel>().OnHorizontalUpdate += OnPlayerMove;
        playerInputSpeed = UIManager.Instance.GetPanel<GameplayPanel>().Speed;
    }

    private void OnPlayerMove(float horizontal)
    {
        var horizontalPos = QuanMathf.ReMap(horizontal, 0, 1, _leftLimit + _offset, _rightLimit - _offset);

        transform.position += Vector3.right * horizontal * playerInputSpeed * Time.deltaTime;

        //horizontalPos = Mathf.Clamp(transform.position.x, _leftLimit + _offset, _rightLimit - _offset);

        transform.position = new Vector2(horizontalPos, transform.position.y);
    }

    private void OnDestroy()
    {
        //UIManager.Instance.GetPanel<GameplayPanel>().OnHorizontalUpdate -= OnPlayerMove;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetAxis("Horizontal") != 0f)
        {
            var horizontal = Input.GetAxis("Horizontal");
            transform.position += Vector3.right * horizontal * playerInputSpeed * Time.deltaTime;
            var horizontalPos = Mathf.Clamp(transform.position.x, _leftLimit + _offset, _rightLimit - _offset);
            transform.position = new Vector2(horizontalPos, transform.position.y);
        }
#endif
    }

}
