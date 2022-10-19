using DG.Tweening;
using UnityEngine;

namespace Match3D
{
    public class MergeArea : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        public Collider col;
        public Transform mergeSlot;
        public Slot leftSlot;
        public Slot rightSlot;

        private void Awake()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Selectable))
            {
                DropDownSelectable(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tags.Selectable))
            {
                MoveUpSelectable(other.gameObject);
            }
        }

        private void DropDownSelectable(GameObject obj)
        {
            Selectable selectable = obj.GetComponent<Selectable>();

            if (!leftSlot.HasSelectable())
            {
                leftSlot.PlaceSelectable(selectable);
            }
            else if (!rightSlot.HasSelectable())
            {
                rightSlot.PlaceSelectable(selectable);

                DOVirtual.DelayedCall(.25f, TryToMerge).Play();
            }
        }

        private void MoveUpSelectable(GameObject obj)
        {
            Selectable selectable = obj.GetComponent<Selectable>();

            if (leftSlot.Selectable == selectable)
            {
                leftSlot.RemoveSelectable();
            }
            else if (rightSlot.Selectable == selectable)
            {
                rightSlot.RemoveSelectable();
            }
        }

        private void TryToMerge()
        {
            if (leftSlot.Selectable == rightSlot.Selectable)
            {
                Merge();
            }
            else
            {
                RemoveLastSelectable();
            }

            void Merge()
            {
                leftSlot.Selectable.transform.DOMove(mergeSlot.position, .25f)
                    .OnStart(() =>
                    {
                        leftSlot.Selectable.MergeSetup();
                    })
                    .OnComplete(() =>
                    {
                        Destroy(leftSlot.Selectable.gameObject);
                        leftSlot.RemoveSelectable();
                    }).Play();

                rightSlot.Selectable.transform.DOMove(mergeSlot.position, .25f)
                    .OnStart(() =>
                    {
                        rightSlot.Selectable.MergeSetup();
                    })
                    .OnComplete(() =>
                    {
                        Destroy(rightSlot.Selectable.gameObject);
                        rightSlot.RemoveSelectable();
                    }).Play();
            }

            void RemoveLastSelectable()
            {
                rightSlot.Selectable.GetComponent<Rigidbody>().AddForce(Vector3.forward * 20f, ForceMode.Impulse);
                rightSlot.Selectable.GetComponent<Rigidbody>().AddTorque(Vector3.forward * 20f, ForceMode.Impulse);
                rightSlot.RemoveSelectable();
            }
        }
    }
}