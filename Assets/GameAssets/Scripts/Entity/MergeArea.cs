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
                DropDownSelectable(other.GetComponent<Selectable>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tags.Selectable))
            {
                MoveUpSelectable(other.GetComponent<Selectable>());
            }
        }

        private void DropDownSelectable(Selectable selectable)
        {
            float delay = .5f;

            if (!leftSlot.HasSelectable())
            {
                leftSlot.PlaceSelectable(selectable);
            }
            else if (!rightSlot.HasSelectable())
            {
                rightSlot.PlaceSelectable(selectable);

                DOVirtual.DelayedCall(delay, TryToMerge).Play();
            }
        }

        private void MoveUpSelectable(Selectable selectable)
        {
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
                float delay = .25f;
                rightSlot.Selectable.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10F, ForceMode.Impulse);
                rightSlot.Selectable.GetComponent<Rigidbody>().AddTorque(Vector3.forward * 10F, ForceMode.Impulse);
                DOVirtual.DelayedCall(delay, rightSlot.RemoveSelectable);
            }
        }
    }
}