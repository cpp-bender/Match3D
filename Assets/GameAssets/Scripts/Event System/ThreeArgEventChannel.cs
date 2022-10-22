using UnityEngine.Events;
using UnityEngine;

namespace Match3D
{
    public class ThreeArgEventChannel<T0, T1, T2> : ScriptableObject
    {
        public event UnityAction<T0, T1, T2> Event;

        public void Raise(T0 arg0, T1 arg1, T2 arg2)
        {
            Event?.Invoke(arg0, arg1, arg2);
        }
    }
}
