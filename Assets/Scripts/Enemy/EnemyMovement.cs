using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float _leftLimit, _rightLimit;
    private float _leftLimitRange, _rightLimitRange;

    [SerializeField] private float _speed;

    [SerializeField] [Range(0, 6f)] private float _leftOffset;
    [SerializeField] [Range(0, 6f)] private float _rightOffset;

    public bool moveRightFirst;

    private void Awake()
    {
        var height = Camera.main.orthographicSize;
        var ratio = (float)Screen.width / Screen.height;
        var width = ratio * height;

        _leftLimit = -width;
        _rightLimit = width;
    }

    void Start()
    {
        _leftLimitRange = transform.position.x - _leftOffset;
        _rightLimitRange = transform.position.x + _rightOffset;
    }

    void Update()
    {
        if (moveRightFirst)
        {
            if(transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(1,1,1);
            }

            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
        else
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            transform.position -= Vector3.right * Time.deltaTime * _speed;
        }

        //range check
        if(transform.position.x > _rightLimitRange || transform.position.x > _rightLimit)
        {
            moveRightFirst = false;
        }
        
        if(transform.position.x < _leftLimitRange || transform.position.x < _leftLimit)
        {
            moveRightFirst = true;
        }
    }
}
