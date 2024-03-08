using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClockAnimation : MonoBehaviour
{
    private RectTransform hourHand;
    private RectTransform minuteHand;

    [Range(0f, 24f)] public float currentHours = 8f;
    [Range(0f, 60f)] public float currentMinutes = 0f;
    private Coroutine countdown;
    [Range(0f, 24f)] public float passingTime = 0;
    [Range(0f, 10f)] public float movingduration = 1f;
    [Range(0f, 10f)] public float headMoveduration = 2f;
    // Use this for initialization
    private void OnEnable()
    {
        minuteHand = transform.GetChild(0).GetComponent<RectTransform>();
        hourHand = transform.GetChild(1).GetComponent<RectTransform>();
        currentHours = GameManager.Instance.saveData.currentStage switch
        {
            Stage.Morning => 8,
            Stage.Afternoon => 13,
            Stage.Night => 18,
            _ => 23
        };
        hourHand.rotation = Quaternion.Euler(0f, 0f, -30 * currentHours);
        minuteHand.rotation = Quaternion.Euler(0f, 0f, -6 * currentMinutes);
        transform.position = new Vector3(Screen.width / 2f, Screen.height * 3f / 2f, 0);
        StartCoroutine(MovingIn());
    }
    private IEnumerator MovingIn()
    {
        float startTime = Time.time;
        while (movingduration > Time.time - startTime)
        {
            float t = (Time.time - startTime) / movingduration;
            transform.position = new Vector3(Screen.width / 2f, Mathf.Lerp(Screen.height * 3f / 2f, Screen.height / 2f - 50, t), 0);
            yield return null;
        }
        transform.position = new Vector3(Screen.width / 2f, Screen.height / 2f - 50, 0);
        startTime = Time.time;
        while (0.2f > Time.time - startTime)
        {
            float t = (Time.time - startTime) / 0.2f;
            transform.position = new Vector3(Screen.width / 2f, Mathf.Lerp(Screen.height / 2f - 50, Screen.height / 2f, t), 0);
            yield return null;
        }
        transform.position = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        StartClockAnimation();
    }
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
        float startHours = this.currentHours;
        float startMinutes = this.currentMinutes;
        passingTime = GameManager.Instance.saveData.currentStage == Stage.Midnight ? Mathf.Abs(24 - this.currentHours) : 3;
        float endHours = this.currentHours + passingTime;
        float endMinutes = this.currentMinutes + passingTime * 60;
        yield return new WaitForSeconds(0.5f);
        while (headMoveduration > Time.time - startTime)
        {
            float t = (Time.time - startTime) / headMoveduration;
            this.currentHours = Mathf.Lerp(startHours, endHours, t);
            this.currentMinutes = Mathf.Lerp(startMinutes, endMinutes, t);
            hourHand.rotation = Quaternion.Euler(0f, 0f, -30 * this.currentHours);
            minuteHand.rotation = Quaternion.Euler(0f, 0f, -6 * this.currentMinutes);
            this.currentHours = this.currentHours - (int)this.currentHours + (int)this.currentHours % 24;
            this.currentMinutes = this.currentMinutes - (int)this.currentMinutes + (int)this.currentMinutes % 60;
            yield return null;
        }
        hourHand.rotation = Quaternion.Euler(0f, 0f, -30 * endHours);
        minuteHand.rotation = Quaternion.Euler(0f, 0f, -6 * endMinutes);
        this.currentHours = endHours - (int)endHours + (int)endHours % 24;
        this.currentMinutes = endMinutes - (int)endMinutes + (int)endMinutes % 60;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MovingOut());
    }
    private IEnumerator MovingOut()
    {
        float startTime = Time.time;
        while (0.2f > Time.time - startTime)
        {
            float t = (Time.time - startTime) / 0.2f;
            transform.position = new Vector3(Screen.width / 2f, Mathf.Lerp(Screen.height / 2f, Screen.height / 2f + 50, t), 0);
            yield return null;
        }
        transform.position = new Vector3(Screen.width / 2f, Screen.height / 2f + 50, 0);
        startTime = Time.time;
        while (movingduration > Time.time - startTime)
        {
            float t = (Time.time - startTime) / movingduration;
            transform.position = new Vector3(Screen.width / 2f, Mathf.Lerp(Screen.height / 2f + 50, -Screen.height * 3f / 2f, t), 0);
            yield return null;
        }
        transform.position = new Vector3(Screen.width / 2f, -Screen.height * 3f / 2f, 0);
        gameObject.SetActive(false);
    }
}
