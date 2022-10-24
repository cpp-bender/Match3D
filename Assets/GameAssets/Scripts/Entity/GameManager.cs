using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Match3D
{
	[DefaultExecutionOrder(-100)]
	public class GameManager : MonoBehaviour
	{
		[Header("BROADCASTING")]
		public VoidEventChannel gameStartEvent;

		private void Awake()
		{
			DOTween.Init(this);
		}

		private IEnumerator Start()
		{
			yield return new WaitForMouseDown();
			gameStartEvent.Raise();
		}
    }
}
