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
			this.Selectable = sel;
		}

        public void RemoveSelectable()
        {
			this.Selectable = null;
        }

		public bool HasSelectable()
		{
			return selectable is not null;
		}
    }
}
