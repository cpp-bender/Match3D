using UnityEngine;
using DG.Tweening;

namespace Match3D
{
	[DefaultExecutionOrder(-100)]
	public class GameManager : MonoBehaviour
	{
		private void Awake()
		{
			DOTween.Init(this);
		}
	}
}
