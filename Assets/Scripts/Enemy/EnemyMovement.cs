using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float _leftLimit, _rightLimit;
    [SerializeField] private float _leftLimitRange, _rightLimitRange;

    [SerializeField] private float _speed;

    [SerializeField] [Range(0, 0.8f)] private float _leftOffsetPercent;
    [SerializeField] [Range(0, 0.8f)] private float _rightOffsetPercent;

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
        _leftLimitRange = _leftLimit * _leftOffsetPercent;
        _rightLimitRange = _rightLimit * _rightOffsetPercent;
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
        if(transform.position.x > _rightLimitRange)
        {
            moveRightFirst = false;
        }
        
        if(transform.position.x < _leftLimitRange)
        {
            moveRightFirst = true;
        }
    }
}
