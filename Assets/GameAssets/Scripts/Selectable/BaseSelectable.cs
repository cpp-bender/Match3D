using UnityEngine;

namespace Match3D
{
    public abstract class BaseSelectable : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        [SerializeField] DragData dragData;
        [SerializeField] int id;

        [Header("DEBUG")]
        [SerializeField] protected Material selectedMat;
        [SerializeField] protected Material unSelectedMat;
        [SerializeField] protected Material stopMat;

        private MeshRenderer meshRenderer;
        protected Rigidbody body;
        private Camera mainCam;
        private Vector3 velocity = Vector3.zero;

        private Vector3 MouseWorldPos
        {
            get
            {
                Vector3 mouseScreenPos = Input.mousePosition;
                mouseScreenPos.z = mainCam.WorldToScreenPoint(transform.position).z;
                return Camera.main.ScreenToWorldPoint(mouseScreenPos);
            }
        }

        public int Id { get => id; }

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            body = GetComponent<Rigidbody>();
            mainCam = Camera.main;
        }

        protected void OnDragging()
        {
            //TODO: Find a way to calc these magic ints. relative to the screen boundaries
            Vector3 tPos = MouseWorldPos + dragData.Offset;

            tPos.x = Mathf.Clamp(tPos.x, -1f, 1f);
            tPos.y = dragData.MaxHeight;
            tPos.z = Mathf.Clamp(tPos.z, -6f, 1f);

            transform.position = Vector3.SmoothDamp(transform.position, tPos, ref velocity, dragData.Duration);
        }

        protected void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }
    }
}
