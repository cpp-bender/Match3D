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

            var seq = DOTween.Sequence();

            Tween rotate = selectable.transform.DORotate(selectable.PlaceRot, .5f)
                .SetAutoKill(true)
                .SetEase(Ease.OutQuad);
            Tween move = selectable.transform.DOMove(transform.position, .5f)
                .SetAutoKill(true)
                .SetEase(Ease.OutQuad);

            seq.Append(move).Join(rotate)
                .OnStart(() =>
                {
                    selectable.DoPlaceSetup();
                })
                .OnComplete(() =>
                {
                })
                .Play();
        }

        public void RemoveSelectable()
        {
            selectable.DoUnPlaceSetup();
            selectable = null;
        }

        public bool HasSelectable()
        {
            return selectable is not null;
        }
    }
}
