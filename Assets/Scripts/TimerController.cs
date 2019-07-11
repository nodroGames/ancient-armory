using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace AncientArmory
{
    public class TimerController : MonoBehaviour
    {
        #region constants
        private const int secondsInHour = 3600;
        private const int secondsInMinute = 60;
        private const int minutesInHour = 60;
        #endregion

        [SerializeField]
        private bool restartAfterTimerFinish = false;

        [Header("---UI Elements---")]
        [SerializeField]
        private int updatesPerSecond = 2;

        [SerializeField]
        private TextMeshProUGUI timerText;

        [SerializeField]
        private TextMeshProUGUI timerTitleText;

        [SerializeField]
        private Slider timerSlider;

        //event stuff
        [HideInInspector]
        public UnityEvent onTimerComplete;

        /// <summary>
        /// The Time when this Timer will fire its event.
        /// </summary>
        private float timerEndTime;

        //coroutine stuff
        private Coroutine coroutine_updateVisuals;

        private float cachedTimerDuration;

        private bool timerIsActive = false;

        // Start is called before the first frame update
        void Start()
        {
            //StartTimer(countdownAmount);
            if (onTimerComplete == null)
            {
                onTimerComplete = new UnityEvent();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(timerIsActive) HandleTimer();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
        
        private void OnValidate()
        {
            updatesPerSecond = Mathf.Clamp(updatesPerSecond, 1, 30);
        }

        private static string ParseTimeToString(float timeRemaining)
        {
            var outputString = new System.Text.StringBuilder();
            //calculate hours, mins, seconds
            var hours = (int)(timeRemaining / secondsInHour);
            timeRemaining -= hours * secondsInHour;

            var minutes = (int)(timeRemaining / secondsInMinute);
            timeRemaining -= minutes * secondsInMinute;

            var seconds = (int)timeRemaining;

            if (hours > 0)//handle hours
            {
                outputString.Append(hours);
                outputString.Append("h ");
            }

            if (minutes > 0)//handle minutes
            {
                outputString.Append(minutes);
                outputString.Append("m ");
            }

            //handle seconds
            {
                outputString.Append(seconds);
                outputString.Append("s");
            }

            return outputString.ToString();
        }

        /// <summary>
        /// Handles updating the visuals performantly (not every frame).
        /// </summary>
        /// <returns></returns>
        private IEnumerator UpdateVisuals()
        {
            while (true)
            {
                var timeRemaining = timerEndTime - Time.time;

                if (timerText) timerText.text = ParseTimeToString(timeRemaining);//update text
                if (timerSlider) timerSlider.value = 1 - (timeRemaining / cachedTimerDuration);//update slider  

                yield return new WaitForSeconds(1 / updatesPerSecond);//limit amount of polling
                yield return new WaitForFixedUpdate();//keep all timers in sync
            }
        }

        private void HandleTimer()
        {
            if (Time.time > timerEndTime)
            {
                if (onTimerComplete != null)
                {
                    OnTimerExpire();//internal stuff
                }
                else
                {
                    Debug.Log("event is null");
                }
            }
        }

        private void OnTimerExpire()
        {
            onTimerComplete.Invoke();//fire event

            //restart or disable?
            if (restartAfterTimerFinish)
            {
                StartTimer(cachedTimerDuration);
            }
            else
            {
                //reset timer
                timerIsActive = false;
                onTimerComplete.RemoveAllListeners();
                StopAllCoroutines();
                ToggleVisuals(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="givenTime"></param>
        /// <param name="timerMode"></param>
        public void StartTimer(float givenTime, string timerName = "", TimerMode timerMode = TimerMode.Countdown, bool restartAfterEnd = false)
        {
            cachedTimerDuration = givenTime;//cache time in case restarts
            restartAfterTimerFinish = restartAfterEnd;//cache

            timerIsActive = true;
            ToggleVisuals(true);
            
            switch (timerMode)
            {
                case TimerMode.Countdown://fire alarm after this much time passes
                    timerEndTime = Time.time + givenTime;
                    break;
                case TimerMode.Alarm://fire alarm at this time
                    timerEndTime = givenTime;
                    break;
            }

            //handle visuals
            if (timerTitleText) timerTitleText.text = timerName;
            coroutine_updateVisuals = StartCoroutine(UpdateVisuals());
        }

        public void AddTime(float timeToAdd)
        {
            timerEndTime += timeToAdd;
        }

        public void ToggleVisuals(bool active)
        {
            timerSlider.gameObject.SetActive(active);
            timerTitleText.gameObject.SetActive(active);
            timerText.gameObject.SetActive(active);
        }
    }
}