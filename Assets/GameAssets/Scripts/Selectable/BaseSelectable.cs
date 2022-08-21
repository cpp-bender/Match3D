using UnityEngine;
using System;

namespace Match3D
{
    public abstract class BaseSelectable : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        [SerializeField] MeshRenderer meshRenderer;
        [SerializeField] Rigidbody body;
        [SerializeField] DragData dragData;

        [Header("DEBUG")]
        [SerializeField] protected Material selectedMat;
        [SerializeField] protected Material unSelectedMat;
        [SerializeField] protected Material stopMat;
        [SerializeField] MousePos mousePos;

        private Vector3 velocity = Vector3.zero;

        private MousePos GetDir()
        {
            //TODO: Refactor
            Vector3 sPos = Input.mousePosition;

            Vector3 vPos = Camera.main.ScreenToViewportPoint(sPos);

            double x = Math.Round(vPos.x, 2);

            double y = Math.Round(vPos.y, 2);

            //Safe Area Constraints
            float minX = .2f;
            float maxX = .8f;
            float minY = .2f;
            float maxY = .8f;

            //The most right point
            if (x >= maxX && (y >= minY && y <= maxY))
            {
                mousePos = MousePos.Right;
                return MousePos.Right;
            }

            //The most left point
            else if (x <= minX && (y >= minY && y <= maxY))
            {
                mousePos = MousePos.Left;
                return MousePos.Left;
            }

            //The most up point
            else if ((x <= maxX && x >= minX) && (y >= maxY))
            {
                mousePos = MousePos.Up;
                return MousePos.Up;
            }

            //The most down point
            else if ((x <= maxX && x >= minX) && (y <= minY))
            {
                mousePos = MousePos.Down;
                return MousePos.Down;
            }
            else
            {
                return 0; 
            }
        }

        private Vector3 GetMouseWorldPos()
        {
            Vector3 sMousePos = Input.mousePosition;
            sMousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(sMousePos);
        }

        protected void DragInsideSafeArea()
        {
            //TODO: Refactor
            Vector3 tPos = GetMouseWorldPos() + dragData.Offset;
            tPos = new Vector3(tPos.x, dragData.MaxHeight, tPos.z);
            transform.position = Vector3.SmoothDamp(transform.position, tPos, ref velocity, dragData.Duration);
        }

        protected void DragOutsideSafeArea()
        {
            //TODO: Refactor
            Vector3 tPos = GetMouseWorldPos() + dragData.Offset;
            tPos = GetDir() == (MousePos.Up | MousePos.Down) ?
                new Vector3(transform.position.x, dragData.MaxHeight, GetMouseWorldPos().z)
                : new Vector3(GetMouseWorldPos().x, dragData.MaxHeight, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, tPos, ref velocity, dragData.Duration);
        }

        protected bool IsInsideSafeArea()
        {
            //TODO: Refactor
            Vector3 wPos = GetMouseWorldPos();

            Vector3 selfPos = transform.position;

            Vector3 vPos = Camera.main.WorldToViewportPoint(wPos);

            Vector3 vPos2 = Camera.main.WorldToViewportPoint(selfPos);

            //Safe Area Constraints
            float minX = .2f;
            float maxX = .8f;
            float minY = .2f;
            float maxY = .8f;

            if ((vPos.x >= minX && vPos.x <= maxX) && (vPos.y >= minY && vPos.y < maxY))
            {
                SetMat(selectedMat);
                return true;
            }

            else if ((vPos2.x >= minX && vPos2.x <= maxX) && (vPos2.y >= minY && vPos2.y < maxY))
            {
                SetMat(selectedMat);
                return true;
            }

            SetMat(stopMat);
            return false;
        }

        protected void SetMat(Material material)
        {
            meshRenderer.material = material;
        }

        protected void SetRigidbody(bool isKinematic)
        {
            body.isKinematic = isKinematic;
        }
    }
}
