using System.Collections.Generic;
using UnityEngine.EventChannels;
namespace UnityEngine.RuntimeSets
{
   [CreateAssetMenu(menuName = "RuntimeSet/GameObject Runtime Set", fileName = "GameObjectRuntimeSet")]
   public class GameObjectRuntimeSet : ScriptableObject
   {
       // Event for the Editor script 
       public System.Action ItemsChanged;

       [Header("Optional")]
       [Tooltip("Notify other objects this Runtime Set has changed")]
       [SerializeField] private VoidEventChannel _runtimeSetUpdated;
       
       public List<GameObject> Items { get; } = new();

       private void OnEnable()
       {
           ItemsChanged?.Invoke();
       }

       // Adds one GameObject to the Items
       public void Add(GameObject thingToAdd)
       {
           if (!Items.Contains(thingToAdd))
               Items.Add(thingToAdd);

           if (_runtimeSetUpdated)
               _runtimeSetUpdated.RaiseEvent();

           ItemsChanged?.Invoke();
       }

       // Removes one GameObject from the Items
       public void Remove(GameObject thingToRemove)
       {
           if (Items.Contains(thingToRemove))
               Items.Remove(thingToRemove);

           if (_runtimeSetUpdated)
               _runtimeSetUpdated.RaiseEvent();

           ItemsChanged?.Invoke();

       }

       // Reset the list
       public void Clear()
       {
           Items.Clear();

           if (_runtimeSetUpdated)
               _runtimeSetUpdated.RaiseEvent();

           ItemsChanged?.Invoke();
       }

       // Clean up any items after the list is cleared
       public void DestroyItems()
       {
           foreach (GameObject item in Items)
               Destroy(item);
       }
   }
}