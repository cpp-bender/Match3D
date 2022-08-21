using UnityEngine;

namespace Match3D
{
    [RequireComponent(typeof(Collider))]
    public class Selectable : BaseSelectable
    {
        private void OnMouseDown()
        {
            SetRigidbody(true);
            SetMat(selectedMat);
        }

        private void OnMouseUp()
        {
            SetRigidbody(false);
            SetMat(unSelectedMat);
        }

        private void OnMouseDrag()
        {
            if (IsInsideSafeArea())
            {
                DragInsideSafeArea();
            }
            else
            {
                DragOutsideSafeArea();
            }
        }
    }
}
