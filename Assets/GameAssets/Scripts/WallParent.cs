using UnityEngine;

namespace Match3D
{
    public class WallParent : MonoBehaviour
    {
        public Transform[] walls;

        private void OnDrawGizmos()
        {
            foreach (Transform wall in walls)
            {
                Collider col = wall.GetComponent<Collider>();
                Gizmos.color = Color.black;
                Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
            }
        }
    }
}
