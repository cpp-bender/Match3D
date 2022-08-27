using System.Collections;
using UnityEngine;
using System;
using TMPro;

namespace Match3D.UI
{
    public class TimerHUD : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        public TextMeshProUGUI timerText;

        private void Start()
        {
            StartCoroutine(StartCountDown(1));
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
    }
}
