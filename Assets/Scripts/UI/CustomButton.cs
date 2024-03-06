using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public List<CustomButton> buttons;
    Vector3 startingScale;
    public float ScaleValue = 1.1f;
    public Button CurrentButton;
    public bool usePointerEnter = false;

    private void Start()
    {
        startingScale = transform.localScale;
        CurrentButton = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (usePointerEnter) return;
        Scale();
        UnScaleButtons();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (usePointerEnter) return;
        UnScale();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!usePointerEnter) return;
        Scale();
        UnScaleButtons();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!usePointerEnter) return;
        UnScale();
    }

    public void Scale()
    {
        transform.localScale = new Vector3(startingScale.x * ScaleValue, startingScale.y * ScaleValue);
    }
    public void UnScale()
    {
        transform.localScale = startingScale;
    }
    void UnScaleButtons()
    {
        foreach (var b in buttons)
        {
            b.UnScale();
        }
    }
}