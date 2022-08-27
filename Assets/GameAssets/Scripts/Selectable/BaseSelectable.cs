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
                mouseScreenPos.z = mainCam.WorldToScreenPoint(transform.position).z;
                return Camera.main.ScreenToWorldPoint(mouseScreenPos);
            }
        }
        private Vector3 MouseViewPos
        {
            get
            {
                return mainCam.ScreenToViewportPoint(Input.mousePosition);
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
            Vector3 tPos = MouseWorldPos;

            if ((IsDown() || IsUp()) && (IsRight() || IsLeft()))
            {
                tPos = new Vector3(transform.position.x, dragData.MaxHeight, transform.position.z);
            }

            else if (IsLeft() || IsRight())
            {
                tPos = new Vector3(transform.position.x, dragData.MaxHeight, MouseWorldPos.z);
            }

            else if (IsUp() || IsDown())
            {
                tPos = new Vector3(MouseWorldPos.x, dragData.MaxHeight, transform.position.z);
            }

            transform.position = Vector3.SmoothDamp(transform.position, tPos, ref velocity, dragData.Duration);
        }

        private bool IsLeft()
        {
            float vMinx = dragData.MinX;

            Vector3 temp = Camera.main.WorldToViewportPoint(transform.position);

            if (temp.x <= vMinx)
            {
                return true;
            }

            return false;
        }

        private bool IsRight()
        {
            float vMaxX = dragData.MaxX;

            Vector3 temp = Camera.main.WorldToViewportPoint(transform.position);

            if (temp.x >= vMaxX)
            {
                return true;
            }

            return false;
        }

        private bool IsUp()
        {
            float vMaxY = dragData.MaxY;

            Vector3 temp = Camera.main.WorldToViewportPoint(transform.position);

            if (temp.y >= vMaxY)
            {
                return true;
            }

            return false;
        }

        private bool IsDown()
        {
            float vMinY = dragData.MinY;

            Vector3 temp = Camera.main.WorldToViewportPoint(transform.position);

            if (temp.y <= vMinY)
            {
                return true;
            }

            return false;
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
