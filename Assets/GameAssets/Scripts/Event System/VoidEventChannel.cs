using UnityEngine.Events;
using UnityEngine;

namespace Match3D
{
    public class VoidEventChannel : ScriptableObject
    {
        public event UnityAction Event;

        public void Raise()
        {
            Event?.Invoke();
        }
    }
}
