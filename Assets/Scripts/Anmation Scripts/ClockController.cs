using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClockController : MonoBehaviour
{
    private RectTransform hourHand;
    private RectTransform minuteHand;

    [Range(0f, 24f)] public float currentHours = 8f;
    [Range(0f, 60f)] public float currentMinutes = 0f;
    private Coroutine countdown;
    [Range(0f, 24f)] public float passingTime = 0;
    [Range(0f, 10f)] public float realTime = 0;
    private static ClockController _instance;
    public static ClockController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ClockController>();

                if (_instance == null)
                {
                    GameObject gameManagerObject = new GameObject("ClockController");
                    _instance = gameManagerObject.AddComponent<ClockController>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        // DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        hourHand = transform.GetChild(1).GetComponent<RectTransform>();
        minuteHand = transform.GetChild(0).GetComponent<RectTransform>();
        hourHand.rotation = Quaternion.Euler(0f, 0f, -30 * currentHours);
        minuteHand.rotation = Quaternion.Euler(0f, 0f, -6 * currentMinutes);
    }
    [ContextMenu("Start Clock Animation")]
    public void StartClockAnimation()
    {
        if (countdown != null)
        {
            StopCoroutine(countdown);
        }
        countdown = StartCoroutine(ClockHeadMove());
    }
    public IEnumerator ClockHeadMove()
    {
        float startTime = Time.time;
        float startHours = currentHours;
        float startMinutes = currentMinutes;
        float endHours = currentHours + passingTime;
        float endMinutes = currentMinutes + passingTime * 60;
        while (realTime > Time.time - startTime)
        {
            float t = (Time.time - startTime) / realTime;
            currentHours = Mathf.Lerp(startHours, endHours, t);
            currentMinutes = Mathf.Lerp(startMinutes, endMinutes, t);
            hourHand.rotation = Quaternion.Euler(0f, 0f, -30 * currentHours);
            minuteHand.rotation = Quaternion.Euler(0f, 0f, -6 * currentMinutes);
            currentHours = currentHours - (int)currentHours + (int)currentHours % 24;
            currentMinutes = currentMinutes - (int)currentMinutes + (int)currentMinutes % 60;
            yield return null;
        }
        hourHand.rotation = Quaternion.Euler(0f, 0f, -30 * endHours);
        minuteHand.rotation = Quaternion.Euler(0f, 0f, -6 * endMinutes);
        currentHours = endHours - (int)endHours + (int)endHours % 24;
        currentMinutes = endMinutes - (int)endMinutes + (int)endMinutes % 60;
    }
    public void MorningClockAnimation(float realTime)
    {
        currentHours = 0;
        currentMinutes = 0;
        passingTime = 8;
        this.realTime = realTime;
        StartClockAnimation();
    }
    public void NoonClockAnimation(float realTime)
    {
        passingTime = 3;
        this.realTime = realTime;
        StartClockAnimation();
    }

    public void AfternoonClockAnimation(float realTime)
    {
        passingTime = 3;
        this.realTime = realTime;
        StartClockAnimation();
    }
    public void NightClockAnimation(float realTime)
    {
        passingTime = Mathf.Abs(24 - currentHours);
        this.realTime = realTime;
        StartClockAnimation();
    }
}
