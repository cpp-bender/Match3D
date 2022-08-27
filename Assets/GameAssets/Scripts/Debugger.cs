using UnityEngine;

namespace Match3D
{
    public class Debugger : MonoBehaviour
    {
        public new Camera camera;
        public Vector3 testVec;

        private void Update()
        {
            testVec =  camera.ScreenToWorldPoint(testVec);
        }
    }
}