using UnityEngine;

namespace Match3D
{
    [RequireComponent(typeof(Collider))]
	public class Selectable : MonoBehaviour
	{
        private Vector3 velocity = Vector3.zero;

        [Header("DEPENDENCIES")]
        [SerializeField] MeshRenderer meshRenderer;
        [SerializeField] Rigidbody body;
        [SerializeField] DragData dragData;

        [Header("DEBUG")]
        public Material selectedMat;
        public Material unSelectedMat;

        private void OnMouseDown()
        {
            body.isKinematic = true;
            meshRenderer.material = selectedMat;
        }

        private void OnMouseDrag()
        {
            Vector3 tPos = GetMouseWorldPos() + dragData.Offset;
            tPos.y = dragData.MaxHeight;
            transform.position = Vector3.SmoothDamp(transform.position, tPos, ref velocity, dragData.Duration);
        }

        private void OnMouseUp()
        {
            body.isKinematic = false;
            meshRenderer.material = unSelectedMat;
        }

        private Vector3 GetMouseWorldPos()
        {
            Vector3 sMousePos = Input.mousePosition;
            sMousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(sMousePos);
        }
    }
}
