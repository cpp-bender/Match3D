using UnityEngine;

namespace Match3D
{
    [CreateAssetMenu(menuName = "Match3D/Drag Data", fileName = "Drag Data")]
    public class DragData : ScriptableObject
    {
        [SerializeField] Vector3 offset;
        [SerializeField, Range(0f, 10f)] float maxHeight;
        [SerializeField, Range(0f, 1f)] float duration;

        [Header("AREA CONSTRAINTS")]
        [SerializeField, Range(0f, 1f)] float minX = .2f;
        [SerializeField, Range(0f, 1f)] float maxX = .8f;
        [SerializeField, Range(0f, 1f)] float minY = .2f;
        [SerializeField, Range(0f, 1f)] float maxY = .8f;

        public Vector3 Offset { get => offset; }
        public float MaxHeight { get => maxHeight; }
        public float Duration { get => duration; }
        public float MinX { get => minX; }
        public float MaxX { get => maxX; }
        public float MinY { get => minY; }
        public float MaxY { get => maxY; }
    }
}
