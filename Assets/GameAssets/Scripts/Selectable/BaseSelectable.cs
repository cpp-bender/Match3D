using UnityEngine;
using System;

namespace Match3D
{
    public abstract class BaseSelectable : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        [SerializeField] DragData dragData;

        [Header("DEBUG")]
        [SerializeField] protected Material selectedMat;
        [SerializeField] protected Material unSelectedMat;
        [SerializeField] protected Material stopMat;

        private MeshRenderer meshRenderer;
        private Rigidbody body;
        private Camera mainCam;
        private Vector3 velocity = Vector3.zero;

        private Vector3 MouseWorldPos
        {
            get
            {
                Vector3 mouseScreenPos = Input.mousePosition;
                mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
                return Camera.main.ScreenToWorldPoint(mouseScreenPos);
            }
        }
        private Vector3 MouseViewPos
        {
            get
            {
                return Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
        }

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            body = GetComponent<Rigidbody>();
            mainCam = Camera.main;
        }

        protected void OnDraggingInsideSafeArea()
        {
            Vector3 tPos = MouseWorldPos + dragData.Offset;
            tPos = new Vector3(tPos.x, dragData.MaxHeight, tPos.z);
            transform.position = Vector3.SmoothDamp(transform.position, tPos, ref velocity, dragData.Duration);
        }

        protected void OnDraggingOutsideSafeArea()
        {
            //TODO: Complete
        }

        protected bool IsMouseInsideSafeArea()
        {
            //TODO: Refactor

            //Safe Area Constraints
            float minX = dragData.MinX;
            float maxX = dragData.MaxX;
            float minY = dragData.MinY;
            float maxY = dragData.MaxY;

            //Check if mouse is outside of safe area
            if ((MouseViewPos.x >= minX && MouseViewPos.x <= maxX) && (MouseViewPos.y >= minY && MouseViewPos.y < maxY))
            {
                SetMaterial(selectedMat);
                return true;
            }

            SetMaterial(stopMat);
            return false;
        }

        protected void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }

        protected void SetRigidbody(bool isKinematic)
        {
            body.isKinematic = isKinematic;
        }
    }
}
