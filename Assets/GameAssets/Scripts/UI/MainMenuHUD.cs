using UnityEngine;

namespace Match3D
{
	public class MainMenuHUD : MonoBehaviour
	{
		[Header("LISTENING ON")]
		[SerializeField] VoidEventChannel gameStartEvent;

		[Header("COMPONENTS")]
		[SerializeField] CanvasGroup canvasGroup;

		private void Awake()
		{
			SwitchVisibility(true);
		}

		private void OnEnable()
		{
			gameStartEvent.Event += OnGameStart;
        }

		private void OnDisable()
		{
			gameStartEvent.Event -= OnGameStart;
        }

        public void OnGameStart()
		{
			SwitchVisibility(false);
		}

		public void SwitchVisibility(bool arg)
		{
			canvasGroup.alpha = arg == true ? 1 : 0;
		}
	}
}
