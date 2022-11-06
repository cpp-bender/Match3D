using UnityEngine;
using System;

namespace Match3D
{
    public class Selectable : BaseSelectable
    {
        private void OnMouseDown()
        {
            MouseDownSetup();
        }

        private void OnMouseUp()
        {
            MouseUpSetup();
        }

        private void OnMouseDrag()
        {
            OnDragging();
        }
    }
}
