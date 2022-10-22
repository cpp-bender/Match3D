using UnityEngine.Events;
using UnityEngine;

namespace Match3D
{
	public abstract class OneArgEventChannel<T> : ScriptableObject
	{
        public event UnityAction<T> Event;

        public void Raise(T arg)
        {
            Event?.Invoke(arg);
        }
    }
}
