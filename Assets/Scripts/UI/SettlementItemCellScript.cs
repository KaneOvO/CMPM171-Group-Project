using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class SettlementItemCellScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform icon;
    public TextMeshProUGUI sellItemText;
    public TextMeshProUGUI priceText;
    public GameObject selectedImage;
    [HideInInspector] public Item item;
    public bool isSelected;
    [HideInInspector] public SettlementPanelScript settlementPanelScript;
    public GameObject descriptionPanel => settlementPanelScript.descriptionPanel;
    public TextMeshProUGUI descriptionNameText => settlementPanelScript.descriptionNameText;
    public TextMeshProUGUI descriptionAmountText => settlementPanelScript.descriptionAmountText;
    public TextMeshProUGUI descriptionPriceText => settlementPanelScript.descriptionPriceText;
    public TextMeshProUGUI descriptionText => settlementPanelScript.descriptionText;
    public TMP_FontAsset font => UIManager.Instance.font;
    protected Coroutine fadeCoroutine;
    public float fadeDuration = 0.2f;
    public Language currentLanguage => GameManager.Instance.playerConfig.currentLanguage;

    protected virtual void OnEnable()
    {
        icon = icon ? icon : transform.Find("Container/Icon Mask/Icon").transform;
        sellItemText = sellItemText ? sellItemText : transform.Find("Container/Sell Item Text").GetComponent<TextMeshProUGUI>();
        priceText = priceText ? priceText : transform.Find("Container/Price Text").GetComponent<TextMeshProUGUI>();
        selectedImage = selectedImage ? selectedImage : transform.Find("Selected Image").gameObject;
        selectedImage.SetActive(false);
    }
    public virtual void Refresh(string id, int amount = 1)
    {
        sellItemText.font = font;
        priceText.font = font;
        amount = Math.Abs(amount);
        item = ItemManager.Instance.ID(id);
        Texture2D texture = Resources.Load<Texture2D>(item.spriteUrl);
        icon.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        sellItemText.text = item.name[(int)currentLanguage] + $" × {amount}";
        float price = item.sellPrice * amount;
        priceText.text = $"{price:F1}" + currentLanguage switch
        {
            Language.English => " yuan",
            Language.Chinese => " 元",
            Language.Japanese => " 円",
            _ => " yuan",
        };
    }
    protected virtual void Update()
    {
        if (isSelected)
        {
            Vector3 spawnedPosition = Input.mousePosition + new Vector3(10, 200, 0);
            spawnedPosition.x = Mathf.Clamp(spawnedPosition.x, 300, Screen.width - 300);
            spawnedPosition.y = Mathf.Clamp(spawnedPosition.y, 100, Screen.height - 100);
            descriptionPanel.transform.position = spawnedPosition;
        }
    }
    public virtual void OnPointerEnter(PointerEventData pointerEventData)
    {
        isSelected = true;
        selectedImage.SetActive(true);
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeIn());
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        isSelected = false;
        descriptionPanel.SetActive(false);
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeOut());
    }

    protected virtual IEnumerator FadeIn()
    {
        Image image = selectedImage.GetComponent<Image>();
        float targetAlpha = 0.5f;
        float startAlpha = image.color.a;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration));
            yield return null;
        }
        Color finalColor = image.color;
        finalColor.a = targetAlpha;
        image.color = finalColor;
        descriptionPanel.SetActive(true);
        string itemName = item.name[(int)currentLanguage];
        descriptionNameText.text = itemName;
        int itemAmount = ItemManager.Instance.InventoryAmount(item.id);
        descriptionAmountText.text = currentLanguage switch
        {
            Language.English => "Backpack",
            Language.Chinese => "背包",
            Language.Japanese => "背包",
            _ => "Backpack"
        } + $" {itemAmount}";
        descriptionPriceText.text = currentLanguage switch
        {
            Language.English => $"Sell Price: {item.sellPrice:F1} yuan",
            Language.Chinese => $"售价：{item.sellPrice:F1} 元",
            Language.Japanese => $"販売価格: {item.sellPrice:F1} 円",
            _ => $"Sell Price: {item.sellPrice:F1} yuan",
        };
        descriptionText.text = item.description[(int)currentLanguage];
    }
    protected virtual IEnumerator FadeOut()
    {
        Image image = selectedImage.GetComponent<Image>();
        float targetAlpha = 0f;
        float startAlpha = image.color.a;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration));
            yield return null;
        }
        Color finalColor = image.color;
        finalColor.a = targetAlpha;
        image.color = finalColor;
        selectedImage.SetActive(false);
    }
}
