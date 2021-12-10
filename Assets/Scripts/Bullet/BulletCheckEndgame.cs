using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCheckEndgame : MonoBehaviour
{
    private float _upPos, _downPos, _leftPos, _rightPos;
    [SerializeField] private float _offset = 1f;


    private bool _inGame = false;

    private void Awake()
    {
        _upPos = Camera.main.orthographicSize;
        _downPos = -Camera.main.orthographicSize;

        var ratio = (float)Screen.width / Screen.height;
        var width = ratio * _upPos;

        _leftPos = -width;
        _rightPos = width;

        _inGame = true;

        UIManager.Instance.GetPanel<PlayAgainPanel>().OnWatchAds += ResetGame;
    }


    private void ResetGame() => _inGame = true;

    private void OnDestroy()
    {
        UIManager.Instance.GetPanel<PlayAgainPanel>().OnWatchAds -= ResetGame;
    }

    void Update()
    {
        if (_inGame)
        {
            if ((transform.position.y > _upPos || transform.position.y < _downPos
                || transform.position.x < _leftPos - _offset || transform.position.x > _rightPos + _offset) 
                && GameplayController.Instance.GetCurrentType() != typeof(Win1GameState))
            {
                GameplayController.Instance.LoseLevelState();
                _inGame = false;
                Pooling.DestroyObject(this.gameObject);
            }
        }
    }
}
