using UnityEngine.Events;
using UnityEngine;

namespace Match3D
{
    public abstract class TwoArgEventChannel<T0, T1> : ScriptableObject
    {
        public event UnityAction<T0, T1> Event;

        public void Raise(T0 arg0, T1 arg1)
        {
            Event?.Invoke(arg0, arg1);
        }
    }
}
