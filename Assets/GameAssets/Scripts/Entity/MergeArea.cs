using UnityEngine;

namespace Match3D
{
    public class MergeArea : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        public Selectable[] currentSelectables = new Selectable[2];
        public int index = 0;

        private void Awake()
        {
            currentSelectables = new Selectable[2];
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Selectable) && index <= 2)
            {
                Vector3 pos = Vector3.zero;

                if (index == 0)
                {
                    pos = transform.TransformPoint(-transform.right);
                }
                else
                {
                    pos = transform.TransformPoint(transform.right);
                }

                var selectable = other.GetComponent<Selectable>();
                selectable.transform.position = pos;
                currentSelectables[index++] = selectable;

                if (index == 2)
                {
                    if (currentSelectables[0].Id == currentSelectables[1].Id)
                    {
                        //Might do operator overloading to check selectables
                        Debug.Log("Merge!");
                    }
                    else
                    {
                        Debug.Log("Fail!");
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tags.Selectable))
            {
                var selectable = other.GetComponent<Selectable>();
                currentSelectables[--index] = null;
            }
        }
    }
}