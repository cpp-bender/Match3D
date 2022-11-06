using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Match3D
{
	public class MainMenuHUD : MonoBehaviour
	{
		[Header("LISTENING ON")]
		[SerializeField] VoidEventChannel gameStartEvent;

		[Header("COMPONENTS")]
		[SerializeField] CanvasGroup canvasGroup;

		[Header("DEPENDENCIES")]
		[SerializeField] TextMeshProUGUI levelCount;
		[SerializeField] RectTransform playButton;

		private void Awake()
		{
			DOAnimation();
			levelCount.text = 1.ToString();
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
			DOTween.Kill(transform);
		}

		private void DOAnimation()
		{
			playButton.DOScale(.15f, 1.5f)
				.SetRelative(true)
				.SetEase(Ease.InOutQuad)
				.SetLoops(-1, LoopType.Yoyo)
				.SetId(transform)
				.Play();
		}

		public void SwitchVisibility(bool arg)
		{
			float t = arg == true ? 1 : 0;
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, t, .5f).Play();
        }
	}
}
