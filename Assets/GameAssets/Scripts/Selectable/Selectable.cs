using UnityEngine;

namespace Match3D
{
    [RequireComponent(typeof(Collider))]
    public class Selectable : BaseSelectable
    {
        private void OnMouseDown()
        {
            SetRigidbody(true);
            SetMaterial(selectedMat);
        }

        private void OnMouseUp()
        {
            SetRigidbody(false);
            SetMaterial(unSelectedMat);
        }

        private void OnMouseDrag()
        {
            if (IsInsideSafeArea())
            {
                OnDraggingInsideSafeArea();
            }
            else
            {
                OnDraggingOutsideSafeArea();
            }
        }
    }
}
