using UnityEngine;

namespace Match3D
{
    public class Selectable : BaseSelectable
    {
        private void OnMouseDown()
        {
            body.useGravity = false;
            SetMaterial(selectedMat);
        }

        private void OnMouseUp()
        {
            body.useGravity = true;
            SetMaterial(unSelectedMat);
        }

        private void OnMouseDrag()
        {
            OnDragging();
        }
    }
}
