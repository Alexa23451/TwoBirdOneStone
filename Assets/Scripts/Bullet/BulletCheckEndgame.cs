using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCheckEndgame : MonoBehaviour
{
    private float _upPos, _downPos;

    private bool _inGame = false;

    private void Awake()
    {
        _upPos = Camera.main.orthographicSize;
        _downPos = -Camera.main.orthographicSize;

        _inGame = true;
    }

    void Update()
    {
        if (_inGame)
        {
            if(transform.position.y > _upPos || transform.position.y < _downPos)
            {
                GameplayController.Instance.LoseLevelState();
                Debug.Log("LOSE GAME");
                _inGame = false;
            }
        }
    }
}
