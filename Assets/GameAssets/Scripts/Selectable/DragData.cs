using UnityEngine;

namespace Match3D
{
    [CreateAssetMenu(menuName = "Match3D/Drag Data", fileName = "Drag Data")]
    public class DragData : ScriptableObject
    {
        [SerializeField] Vector3 offset;
        [SerializeField, Range(0f, 10f)] float maxHeight;
        [SerializeField, Range(0f, 1f)] float duration;

        public Vector3 Offset { get => offset; }
        public float MaxHeight { get => maxHeight; }
        public float Duration { get => duration; }
    }
}
