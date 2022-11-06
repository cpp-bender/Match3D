using UnityEngine;

namespace Match3D
{
    [RequireComponent(typeof(Collider)), SelectionBase]
    public abstract class BaseSelectable : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        [SerializeField] DragData dragData;
        [SerializeField] Type type;
        [SerializeField] Vector3 placeRot = Vector3.zero;

        [Header("DEBUG")]
        [SerializeField] protected Material selectedMat;
        [SerializeField] protected Material unSelectedMat;
        [SerializeField] protected Material stopMat;

        [Header("COMPONENTS")]
        [SerializeField] MeshRenderer meshRenderer;
        [SerializeField] protected Rigidbody body;
        [SerializeField] Collider col;

        private Camera mainCam;
        private Vector3 velocity = Vector3.zero;

        private Vector3 MouseWorldPos
        {
            get
            {
                Vector3 mouseScreenPos = Input.mousePosition;
                mouseScreenPos.z = mainCam.WorldToScreenPoint(transform.position).z;
                return mainCam.ScreenToWorldPoint(mouseScreenPos);
            }
        }

        private Vector3 ScreenWorldPos
        {
            get
            {
                Vector3 pos = new Vector3(Screen.width - 100, Screen.height - 100, 0f);
                pos.z = mainCam.WorldToScreenPoint(transform.position).z;
                return mainCam.ScreenToWorldPoint(pos);
            }
        }

        public Vector3 PlaceRot { get => placeRot; private set => placeRot = value; }

        private void Awake()
        {
            mainCam = Camera.main;
        }

        protected void OnDragging()
        {
            //TODO: Find a way to calc these magic ints. relative to the screen boundaries
            Vector3 tPos = MouseWorldPos + dragData.Offset;

            //tPos.x = Mathf.Clamp(tPos.x, -1f, 1f);
            tPos.z = Mathf.Clamp(tPos.z, -6f, 10f);
            //tPos.y = dragData.MaxHeight;

            tPos.x = Mathf.Clamp(tPos.x, -ScreenWorldPos.x, ScreenWorldPos.x);
            tPos.y = dragData.MaxHeight;
            //tPos.z = Mathf.Clamp(tPos.z, -ScreenWorldPos.y, ScreenWorldPos.y);

            transform.position = Vector3.SmoothDamp(transform.position, tPos, ref velocity, dragData.Duration);
        }

        protected void MouseDownSetup()
        {
            body.useGravity = false;
            body.isKinematic = false;
            body.velocity = Vector3.zero;
            SetMaterial(selectedMat);
        }

        protected void MouseUpSetup()
        {
            body.useGravity = true;
            body.isKinematic = false;
            SetMaterial(unSelectedMat);
        }

        protected void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }

        public static bool operator ==(BaseSelectable s1, BaseSelectable s2)
        {
            return (s1.type == s2.type);
        }

        public static bool operator !=(BaseSelectable s1, BaseSelectable s2)
        {
            return (s1.type != s2.type);
        }

        public void MergeSetup()
        {
            col.enabled = false;
            body.isKinematic = true;
        }

        public void DoPlaceSetup()
        {
            body.constraints = RigidbodyConstraints.FreezeAll;
        }

        public void DoUnPlaceSetup()
        {
            body.constraints = RigidbodyConstraints.None;
        }

        public enum Type
        {
            Cube,
            Sphere,
            Capsule,
            None,
        };
    }
}
