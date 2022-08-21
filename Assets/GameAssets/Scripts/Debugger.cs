using UnityEngine;
using System;

namespace Match3D
{
    public class Debugger : MonoBehaviour
    {
        public MousePos mousePos;
        public double x;
        public double y;

        private void Update()
        {
            Vector3 sPos = Input.mousePosition;

            Vector3 vPos = Camera.main.ScreenToViewportPoint(sPos);

            x = Math.Round(vPos.x, 2);

            y = Math.Round(vPos.y, 2);

            //The most right point
            if (x >= .9f && (y >= .1f && y <= .8f))
            {
                mousePos = MousePos.Right;
            }

            //The most left point
            else if (x <= .1f && (y >= .1f && y <= .8f))
            {
                mousePos = MousePos.Left;
            }

            //The most up point
            else if ((x <= .9f && x >= .1f) && (y >= .9f))
            {
                mousePos = MousePos.Up;
            }

            //The most down point
            else if ((x <= .9f && x >= .1f) && (y <= .1f))
            {
                mousePos = MousePos.Down;
            }
        }
    }

    public enum MousePos
    {
        Right,
        Left,
        Up,
        Down,
    };
}
