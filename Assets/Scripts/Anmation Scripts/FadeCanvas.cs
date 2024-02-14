using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FadeCanvas : MonoBehaviour
{
    public Image fadeImage;
    public FadeEventSO fadeEventSO;
    private void Awake()
    {
        fadeImage = GetComponentInChildren<Image>();
    }
    public void OnEnable()
    {
        fadeEventSO.onEventRaised += OnFadeEvent;
    }
    public void OnDisable()
    {
        fadeEventSO.onEventRaised -= OnFadeEvent;
    }
    public void OnFadeEvent(Color color, float duration)
    {
        fadeImage.DOBlendableColor(color, duration);
    }
}
