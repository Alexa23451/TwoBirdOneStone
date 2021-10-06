using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : BaseManager<TimerManager>
{
    private List<Timer> _timers;

    public override void Init()
    {
        _timers = new List<Timer>();
    }

    public Timer AddTimer(float duration, Action callback)
    {
        Timer timer = new Timer(duration, callback);
        _timers.Add(timer); //Adding new timer

        ClearTimers();

        return timer;
    }

    private void ClearTimers()
    {
        _timers.RemoveAll(delegate (Timer s) { return s == null; });
        _timers.RemoveAll(delegate (Timer s) { return s.IsTick == false; }); //Deleting timers if not tick
    }

    public void Update()
    {
        for (int i = 0; i < _timers.Count; i++)
        {
            if (_timers[i] == null)
                continue;

            if (_timers[i].IsTick)
                _timers[i].Update();
        }
    }
}
