using ClickerQuest.Characters;
using UnityEditor;
namespace UnityEngine.EventChannels.Editor
{
    [CustomEditor(typeof(CharacterEventChannel))]
    public class CharacterEventChannelEditor : GenericEventChannelEditor<Character> { }
}