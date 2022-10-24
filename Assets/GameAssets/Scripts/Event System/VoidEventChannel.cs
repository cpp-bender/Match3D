using UnityEngine.Events;
using UnityEngine;

namespace Match3D
{
    [CreateAssetMenu(menuName = "Event System/Void Event Channel", fileName = "Void Event Channel")]
    public class VoidEventChannel : ScriptableObject
    {
        public event UnityAction Event;

        public void Raise()
        {
            Event?.Invoke();
        }
    }
}
