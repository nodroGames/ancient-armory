using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class TimerController : MonoBehaviour
{
    #region constants
    private const int secondsInHour = 3600;
    private const int secondsInMinute = 60;
    private const int minutesInHour = 60;
    #endregion

    [SerializeField]
    private bool restartImmediately = false;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private float countdownAmount = 5;

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
    public UnityEvent onTimerComplete;

    /// <summary>
    /// The Time when this Timer will fire its event.
    /// </summary>
    private float timerEndTime;

    //coroutine stuff
    private Coroutine coroutine_updateVisuals;

    // Start is called before the first frame update
    void Start()
    {
        StartTimer(countdownAmount);
    }

    // Update is called once per frame
    void Update()
    {
        HandleTimer();
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

        if(minutes > 0)//handle minutes
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

            timerText.text = ParseTimeToString(timeRemaining);//update text
            timerSlider.value = Time.time / timerEndTime;//update slider

            yield return new WaitForSeconds(1 / updatesPerSecond);//limit amount of polling
            yield return new WaitForFixedUpdate();//keep all timers in sync
        }
    }

    private void HandleTimer()
    {
        if (Time.time > timerEndTime)
        {
            if(onTimerComplete != null)
            {
                onTimerComplete.Invoke();//fire event

                OnTimerExpire();//internal stuff
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="givenTime"></param>
    /// <param name="timerMode"></param>
    public void StartTimer(float givenTime, string timerName = "", TimerMode timerMode = TimerMode.Countdown)
    {
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }

        coroutine_updateVisuals = StartCoroutine(UpdateVisuals());

        timerTitleText.text = timerName;

        switch (timerMode)
        {
            case TimerMode.Countdown://fire alarm after this much time passes
                timerEndTime = Time.time + givenTime;
                break;
            case TimerMode.Alarm://fire alarm at this time
                timerEndTime = givenTime;
                break;
        }
    }

    private void OnTimerExpire()
    {
        //restart or disable?
        if (restartImmediately)
        {
            StartTimer(countdownAmount);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void AddTime(float timeToAdd)
    {
        timerEndTime += timeToAdd;
    }
}
