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
        public bool insideSafeArea;

        private void OnMouseDown()
        {
            body.isKinematic = true;
            meshRenderer.material = selectedMat;
        }

        private void OnMouseDrag()
        {
            if (IsInsideSafeArea())
            {
                MoveInsideSafeArea();
            }
            else
            {
                MoveOutsideSafeArea();
            }
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

        private bool IsInsideSafeArea()
        {
            Vector3 wPos = GetMouseWorldPos();

            Vector3 vPos = Camera.main.WorldToViewportPoint(wPos);

            if ((vPos.x >= 0f && vPos.x <= 1f) && (vPos.y >= .1f && vPos.y < .9f))
            {
                insideSafeArea = true;
                return true;
            }

            insideSafeArea = false;
            return false;
        }

        private void MoveInsideSafeArea()
        {
            Vector3 tPos = GetMouseWorldPos() + dragData.Offset;
            tPos.y = dragData.MaxHeight;
            transform.position = Vector3.SmoothDamp(transform.position, tPos, ref velocity, dragData.Duration);
        }

        private void MoveOutsideSafeArea()
        {
            Vector3 dir = transform.InverseTransformPoint(GetMouseWorldPos()).z <= 0 ? Vector3.back : Vector3.forward;

            Vector3 tPos = GetMouseWorldPos() - (dir);
            tPos.y = dragData.MaxHeight;
            transform.position = Vector3.SmoothDamp(transform.position, tPos, ref velocity, dragData.Duration);
        }
    }
}
