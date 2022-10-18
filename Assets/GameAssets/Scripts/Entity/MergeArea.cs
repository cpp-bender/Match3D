using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Match3D
{
    public class MergeArea : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        public Collider col;
        public Transform mergeSlot;
        public Transform slot0;
        public Transform slot1;

        [Header("DEBUG")]
        public Selectable[] selectables;
        public int slotIndex = 0;

        private Transform activeSlot
        {
            get
            {
                return slotIndex == 0 ? slot0 : slot1;
            }
            set
            {

            }
        }

        private void Awake()
        {
            selectables = new Selectable[2];
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Selectable))
            {
                StartCoroutine(SelectableDropDownRoutine(other.gameObject));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tags.Selectable))
            {
                StartCoroutine(SelectableMoveUpRoutine(other.gameObject));
            }
        }

        private IEnumerator SelectableDropDownRoutine(GameObject obj)
        {
            Selectable selectable = obj.GetComponent<Selectable>();
            selectables[slotIndex] = selectable;
            Tween move = selectable.transform.DOMove(activeSlot.position, .25f)
                .SetAutoKill(true)
                .SetEase(Ease.OutQuad);

            yield return move.Play().WaitForCompletion();

            if (slotIndex < 1)
            {
                slotIndex++;
                yield break;
            }
            else
            {
                yield return new WaitForSeconds(.25f);
                TryToMerge();
            }
        }

        private IEnumerator SelectableMoveUpRoutine(GameObject obj)
        {
            Selectable selectable = obj.GetComponent<Selectable>();

            if (slotIndex > 0)
            {
                slotIndex--;
            }

            yield return null;
        }

        private void TryToMerge()
        {
            if (selectables[0] == selectables[1])
            {
                Merge();
            }
            else
            {
                RemoveLastSelectable();
            }

            void Merge()
            {
                selectables[0].transform.DOMove(mergeSlot.position, .25f)
                    .OnStart(() =>
                    {
                        selectables[0].MergeSetup();
                    })
                    .OnComplete(() =>
                    {
                        Destroy(selectables[0].gameObject);
                    }).Play();

                selectables[1].transform.DOMove(mergeSlot.position, .25f)
                    .OnStart(() =>
                    {
                        selectables[1].MergeSetup();
                    })
                    .OnComplete(() =>
                    {
                        Destroy(selectables[1].gameObject);
                    }).Play();
            }

            void RemoveLastSelectable()
            {
                selectables[1].GetComponent<Rigidbody>().AddForce(Vector3.forward * 20f, ForceMode.Impulse);
                selectables[1].GetComponent<Rigidbody>().AddTorque(Vector3.forward * 20f, ForceMode.Impulse);
                selectables[slotIndex] = null;
            }
        }
    }
}