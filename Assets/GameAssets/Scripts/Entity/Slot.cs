using DG.Tweening;
using UnityEngine;

namespace Match3D
{
    public class Slot : MonoBehaviour
    {
        [Header("DEBUG")]
        [SerializeField] Selectable selectable;

        public Selectable Selectable { get => selectable; private set => selectable = value; }

        public void PlaceSelectable(Selectable sel)
        {
            selectable = sel;
            sel.transform.DOMove(transform.position, .25f)
                .SetAutoKill(true)
                .SetEase(Ease.OutQuad).Play();
        }

        public void RemoveSelectable()
        {
            selectable = null;
        }

        public bool HasSelectable()
        {
            return selectable is not null;
        }
    }
}
