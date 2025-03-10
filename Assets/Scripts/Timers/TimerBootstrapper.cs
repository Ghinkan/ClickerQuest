using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace Timers
{
    public abstract class TimerBootstrapper
    {
        private static PlayerLoopSystem _timerSystem;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        internal static void Initialize()
        {
            PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();

            if (!InsertTimerManager<Update>(ref currentPlayerLoop, 0))
            {
                Debug.LogWarning("Improved Timers not initialized, unable to register TimerManager into the Update loop.");
                return;
            }

            PlayerLoop.SetPlayerLoop(currentPlayerLoop);
            PlayerLoopInterface.PrintPlayerLoop(currentPlayerLoop);
        }

        private static bool InsertTimerManager<T>(ref PlayerLoopSystem loop, int index) 
        {
            _timerSystem = new PlayerLoopSystem() 
            {
                type = typeof(TimerManager),
                updateDelegate = TimerManager.UpdateTimers,
                subSystemList = null,
            };

            PlayerLoopInterface.SystemsCleared += RemoveTimerManager;
            return PlayerLoopInterface.InsertSystem<T>(ref loop, in _timerSystem, index);
        }
        
        private static void RemoveTimerManager() 
        {
            TimerManager.Clear();
            PlayerLoopInterface.SystemsCleared -= RemoveTimerManager;
        }
    }
}
