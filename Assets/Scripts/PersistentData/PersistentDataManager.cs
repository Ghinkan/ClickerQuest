using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace ClickerQuest.PersistentData
{
    public class PersistentDataManager : SerializedMonoBehaviour
    {
        [SerializeField] private readonly List<IPersistentData> _persistentDataList = new List<IPersistentData>();
        
        [Button]
        public void Save()
        {
            long startTime = DateTime.Now.Ticks;
            foreach (IPersistentData persistentData in _persistentDataList)
                persistentData.Save();
            
            long saveTime = DateTime.Now.Ticks - startTime;
            Debug.Log($"Time to save: {(saveTime/TimeSpan.TicksPerMillisecond):N4}ms");
        }
        
        public void Load()
        {
            long startTime = DateTime.Now.Ticks;
            foreach (IPersistentData persistentData in _persistentDataList)
                persistentData.Load();
            
            long loadTime = DateTime.Now.Ticks - startTime;
            Debug.Log($"Time to load: {(loadTime/TimeSpan.TicksPerMillisecond):N4}ms");
        }

        public void RestoreData()
        {
            foreach (IPersistentData persistentData in _persistentDataList)
                persistentData.Restore();
            
            ES3.DeleteFile();
        }
    }
}