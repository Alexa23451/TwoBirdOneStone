using System;
using UnityEngine;

public class Timer
{
    public Action OnTimerFinish;

    public bool IsTick => _isTick;

    private float _timer = 0;
    private float _duration;
    private bool _isTick;

    public Timer(float duration = 1, Action callback = null)
    {
        _duration = duration;
        OnTimerFinish = callback;
        _isTick = true;
    }

    public void Update()
    {
        if (_isTick)
        {
            _timer += Time.deltaTime;
            if (_timer >= _duration)
            {
                OnTimerFinishHandler();
            }
        }
    }

    private void OnTimerFinishHandler()
    {
        _timer = 0;
        _isTick = false;
        if (OnTimerFinish != null)
            OnTimerFinish();

        OnTimerFinish = null;
    }
}
