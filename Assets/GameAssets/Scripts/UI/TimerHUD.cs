using System.Collections;
using UnityEngine;
using System;
using TMPro;

namespace Match3D.UI
{
    public class TimerHUD : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        [SerializeField] TextMeshProUGUI timerText;

        [Header("COMPONENTS")]
        [SerializeField] CanvasGroup canvasGroup;

        [Header("LISTENING ON")]
        [SerializeField] VoidEventChannel gameStartEvent;

        private void Awake()
        {
            SwitchVisibility(false);
        }

        private void OnEnable()
        {
            gameStartEvent.Event += OnGameStart;
        }

        private void OnDisable()
        {
            gameStartEvent.Event -= OnGameStart;
        }

        private IEnumerator StartCountDown(double minutes)
        {
            TimeSpan timeSpan = TimeSpan.FromMinutes(minutes);

            double currentSeconds = Math.Round(timeSpan.TotalSeconds, 0);

            timerText.SetText(TimeSpan.FromSeconds(currentSeconds).ToString(@"mm\:ss"));

            while (currentSeconds > 0)
            {
                currentSeconds--;
                timerText.SetText(TimeSpan.FromSeconds(currentSeconds).ToString(@"mm\:ss"));
                yield return new WaitForSecondsRealtime(1f);
            }

            Debug.Log("Time is up!");
            yield return null;
        }

        public void OnGameStart()
        {
            SwitchVisibility(true);
            StartCoroutine(StartCountDown(10));
        }

        public void SwitchVisibility(bool arg)
        {
            canvasGroup.alpha = arg == true ? 1 : 0;
        }
    }
}
