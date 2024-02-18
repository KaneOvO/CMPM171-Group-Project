using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Reflection;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
public class PrefabController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    public TMP_Text costText;
    public TMP_Text counterText;
    public GameObject panelInScene;
    public string itemName;
    public string itemDescription;
    public int count = 0;
    public float itemCost = 0;
    public GameObject selectedBox;
    private bool isSelected;
    private Coroutine fadeCoroutine;
    public float fadeDuration = 0.2f;
    public TMP_Text descriptionAmountText;
    public TMP_Text descriptionPrice;
    public TMP_Text descriptionText;

    void Start()
    {
        
    }

    private void Update()
    {
        if (isSelected)
        {
            Vector3 spawnedPosition = Input.mousePosition + new Vector3(10, 200, 0);
            spawnedPosition.x = Mathf.Clamp(spawnedPosition.x, 300, Screen.width - 300);
            spawnedPosition.y = Mathf.Clamp(spawnedPosition.y, 100, Screen.height - 100);
            panelInScene.transform.position = spawnedPosition;
        }
    }

    public void IncreaseCount()
    {
        count++;
        UpdateCounterText();
        descriptionAmountText.text = $"Amount: <size=40>{count}</size>";
    }

    public void DecreaseCount()
    {
        if (count > 0)
        {
            count--;
            UpdateCounterText();
            descriptionAmountText.text = $"Amount: <size=40>{count}</size>";
        }
    }

    public void setItem(Item item)
    {

        itemName = item.name[(int)GameManager.Instance.saveData.currentLanguage];
        itemDescription = item.description[(int)GameManager.Instance.saveData.currentLanguage];
        SetItemImage(item.spriteUrl);
        itemCost = item.price[(int)Price.Default];
        costText.text = $"${itemCost:F2}";
        
    }

    public void SetItemName(string name)
    {
        itemName = name;
    }

    public void SetItemDescription(string description)
    {
        itemDescription = description;
    }

    public void SetItemImage(string imageName)
    {
        Sprite loadedSprite = Resources.Load<Sprite>(imageName);
        itemImage.sprite = loadedSprite;
    }

    public void SetItemCost(float cost)
    {
        itemCost = cost;
        costText.text = $"${cost:F2}";
    }

    public void UpdateCounterText()
    {
        counterText.text = count.ToString();
        GameObject.Find("Canvas").GetComponent<LoadWhollesale>().CalculateTotalCost();
    }

    public void setPanelInScene(GameObject panel)
    {
        panelInScene = panel;
        descriptionAmountText = panel.transform.Find("Amount Text").GetComponent<TMP_Text>();
        descriptionPrice = panel.transform.Find("Price Text").GetComponent<TMP_Text>();
        descriptionText = panel.transform.Find("Description Text").GetComponent<TMP_Text>();
    }

    public int GetCost()
    {
        return  (int) itemCost * count;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isSelected = true;
        selectedBox.SetActive(true);
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        Image image = selectedBox.GetComponent<Image>();
        float targetAlpha = 1f;
        float startAlpha = image.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            Color color = image.color;
            color.a = newAlpha;
            image.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Color finalColor = image.color;
        finalColor.a = targetAlpha;
        image.color = finalColor;
        panelInScene.SetActive(true);


        descriptionAmountText.text = $"Amount: <size=40>{count}</size>";
        descriptionPrice.text = $"$: <color=#FF0>{itemCost:F2}</color>";
        descriptionText.text = itemDescription;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isSelected = false;
        panelInScene.SetActive(false);
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        Image image = selectedBox.GetComponent<Image>();
        float targetAlpha = 0f;
        float startAlpha = image.color.a;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            Color color = image.color;
            color.a = newAlpha;
            image.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Color finalColor = image.color;
        finalColor.a = targetAlpha;
        image.color = finalColor;
        selectedBox.SetActive(false);
    }

}
