using UnityEngine;

namespace Match3D
{
    public class MergeArea : MonoBehaviour
    {
        [Header("COMPONENTS")]
        public Collider col;

        [Header("DEBUG")]
        public Selectable[] currentSelectables;
        public Vector3[] selectablePos;
        public int index;

        private void Awake()
        {
            currentSelectables = new Selectable[2];
            selectablePos = new Vector3[]
                { transform.TransformPoint(-transform.right),
                transform.TransformPoint(transform.right) };
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Selectable))
            {
                Selectable selectable = other.GetComponent<Selectable>();
                selectable.transform.position = selectablePos[index];
                currentSelectables[index++] = selectable;

                if (index == 2)
                {
                    col.enabled = false;
                    CheckMerge();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tags.Selectable))
            {
                Selectable selectable = other.GetComponent<Selectable>();
                currentSelectables[--index] = null;

                if (index == 0)
                {
                    col.enabled = true;
                }
            }
        }

        private void CheckMerge()
        {
            //Might turn this to a coroutine
            if (currentSelectables[0] == currentSelectables[1])
            {
                Debug.Log("Merge!");
                Destroy(currentSelectables[0].gameObject);
                Destroy(currentSelectables[1].gameObject);
            }
            else
            {
                Debug.Log("Fail!");
            }
        }
    }
}