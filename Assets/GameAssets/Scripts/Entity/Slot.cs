using DG.Tweening;
using UnityEngine;

namespace Match3D
{
    public class Slot : MonoBehaviour
    {
        [Header("DEBUG")]
        [SerializeField] Selectable selectable;

        [Header("BROADCASTING")]
        [SerializeField] IntEventChannel statusChange;

        public Selectable Selectable { get => selectable; private set => selectable = value; }

        public void PlaceSelectable(Selectable sel)
        {
            selectable = sel;
            selectable.transform.DORotate(selectable.PlaceRot, .25f)
                .SetAutoKill(true)
                .SetEase(Ease.OutQuad)
                .Play();
            selectable.transform.DOMove(transform.position, .25f)
                .SetAutoKill(true)
                .SetEase(Ease.OutQuad)
                .Play();

            statusChange.Raise(1);
        }

        public void RemoveSelectable()
        {
            statusChange.Raise(0);
            selectable = null;
        }

        public bool HasSelectable()
        {
            return selectable is not null;
        }
    }
}
