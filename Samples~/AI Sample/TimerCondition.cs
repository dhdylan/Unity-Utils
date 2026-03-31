using System;
using UnityEngine;
using DRS.Core.AI;

[Serializable]
public class TimerCondition : Condition
{
    [Tooltip("This is an amount of time that will pass before this timer condition expires." +
        " Once a timer condition expires, the condition will evaluate true.")]
    public float TimerTime = 5f;

    private bool _started = false;
    private float _startedTime = 0f;

    public override bool Evaluate(StateGraph owningStateGraph)
    {
        bool success = false;

        if(!_started)
        {
            _startedTime = Time.time;
            _started = true;
        }

        success = _started && Time.time > _startedTime + TimerTime;

        return Negate ? !success : success;

    }

    public override void Reset()
    {
        _started = false;
    }

    public override Condition DeepCopy()
    {
        TimerCondition copy = new TimerCondition();
        copy.TimerTime = TimerTime;
        copy.Negate = Negate;
        return copy;
    }
}