﻿using UnityEngine;
namespace Timers
{
    /// <summary>
    /// Timer that counts up from zero to infinity.
    /// </summary>
    public class StopwatchTimer : Timer
    {
        public StopwatchTimer(MonoBehaviour owner) : base(0, owner) { }

        public override void Tick() 
        {
            if (IsRunning) 
                CurrentTime += Time.deltaTime;
        }

        public override bool IsFinished => false;
    }
}