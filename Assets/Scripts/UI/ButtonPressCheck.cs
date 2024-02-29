using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonPressCheck : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed;
    public bool isHolding;
    public float interval = 0.25f;
    public float timer = 0f;
    public UnityEvent onButtonDownEvents;
    public UnityEvent onButtonUpEvents;
    public UnityEvent onButtonHoldEvents;
    void Update()
    {
        if (isPressed)
        {
            timer = Mathf.Clamp(timer + Time.deltaTime, 0, interval);
            isHolding = timer >= interval;
            if (isHolding) { onButtonHoldEvents?.Invoke(); }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        onButtonDownEvents?.Invoke();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        onButtonUpEvents?.Invoke();
        isPressed = false;
        isHolding = false;
        timer = 0f;
    }
}
