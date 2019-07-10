using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private TimerMode timerMode;

    [SerializeField]
    private float timerEndTime;
    
    [Header("---UI Elements---")]
    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private Slider timerSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleTimer();
    }

    private void HandleTimer()
    {
        if (Time.time > timerEndTime)
        {
            //fire event
        }

    }

    public void StartTimer(float givenTime, TimerMode timerMode = TimerMode.Countdown)
    {
        this.timerMode = timerMode;
        switch (timerMode)
        {
            case TimerMode.Countdown:
                break;
            case TimerMode.Alarm:
                break;
        }
    }

    public void AddTime()
    {

    }

    public void SubtractTime(float reduceTime)
    {

    }
}
