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
    [Header("Script Settings")]
    public WholesaleCanvas wholesaleCanvas;
    public Wholesale wholesale;
    public SaleBackpackCellScript saleBackpackCellScript;
    public SaleBackpackPanelScript saleBackpackPanelScript;
    public int currentScene;

    [Header("Display Item Info")]
    public string id;
    public string itemName;
    public string itemDescription;
    private InventoryItem inventoryItem;
    public Image itemImage;
    private int inventoryItemAmount;
    public TMP_Text costText;
    public TMP_Text counterText;
    [HideInInspector] public int count = 0;
    [HideInInspector] public float itemCost = 0;
    [HideInInspector] public int maxStack;
    public GameObject selectedBox;
    private bool isSelected;
    private Coroutine fadeCoroutine;
    public float fadeDuration = 0.2f;
    [Header("Description Panel Setting")]
    public GameObject descriptionPanel;
    [HideInInspector] public TMP_Text descriptionNameText;
    [HideInInspector] public TMP_Text descriptionAmountText;
    [HideInInspector] public TMP_Text descriptionPriceText;
    [HideInInspector] public TMP_Text descriptionText;
    [Header("Button Setting")]
    public Button increaseCountButton;
    public Button decreaseCountButton;
    public Button closeButton;
    public TMP_FontAsset font => UIManager.Instance.font;
    public Language currentLanguage => GameManager.Instance.playerConfig.currentLanguage;
    public void Refresh()
    {
        if (currentScene == 0)
        {
            closeButton.gameObject.SetActive(false);
        }
        decreaseCountButton.interactable = true;
        UpdateButtonState();
    }

    private void Update()
    {
        if (isSelected)
        {
            Vector3 spawnedPosition = Input.mousePosition + new Vector3(10, 200, 0);
            spawnedPosition.x = Mathf.Clamp(spawnedPosition.x, 300, Screen.width - 300);
            spawnedPosition.y = Mathf.Clamp(spawnedPosition.y, 100, Screen.height - 100);
            descriptionPanel.transform.position = spawnedPosition;
        }
    }

    public void IncreaseCount()
    {
        inventoryItemAmount = inventoryItem != null ? inventoryItem.amount : 0;
        if (currentScene == 0 && inventoryItemAmount + count < maxStack)
        {
            count++;
        }
        else if (currentScene == 1 && count < inventoryItemAmount)
        {
            count++;
        }
        UpdateCounterText();
        UpdateButtonState();
    }

    public void DecreaseCount()
    {
        if (count > 0)
        {
            count--;
        }
        UpdateCounterText();
        UpdateButtonState();
    }

    public void UpdateButtonState()
    {
        inventoryItemAmount = inventoryItem != null ? inventoryItem.amount : 0;
        if (currentScene == 0)
        {
            increaseCountButton.interactable = inventoryItemAmount + count < maxStack;
            decreaseCountButton.interactable = count > 0;
        }
        else if (currentScene == 1)
        {
            increaseCountButton.interactable = count < inventoryItemAmount;
            if (count == 0)
            {
                saleBackpackPanelScript.removeFromDisplay(id, saleBackpackCellScript, saleBackpackCellScript.itemAmount);
            }
        }
    }

    public void SetCurrentScene(int scene)
    {
        this.currentScene = scene;
    }

    public void SetItem(Item item)
    {
        SetItemID(item.id);
        SetItemName(item.name[(int)currentLanguage]);
        SetItemDescription(item.description[(int)currentLanguage]);
        SetItemImage(item.spriteUrl);
        SetItemCost(item.originalPrice);
        maxStack = item.maxStack;
        inventoryItem = ItemManager.Instance.inventory.Find(x => x.id == item.id);
    }

    public void SetItemID(string id)
    {
        this.id = id;
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
        costText.text = $"$:{cost:F1}";
    }

    public void UpdateCounterText()
    {
        counterText.text = count.ToString();
        if (currentScene == 0)
        {
            wholesale?.CalculateTotalCost();
        }
        else if (currentScene == 1)
        {
            inventoryItemAmount = inventoryItem != null ? inventoryItem.amount : 0;
            saleBackpackCellScript.itemAmount = Math.Clamp(inventoryItemAmount - count, 0, maxStack);
            saleBackpackCellScript.itemAmountText.text = $"{saleBackpackCellScript.itemAmount.ToString()}";
        }
    }

    public void SetPanelInScene(GameObject panel)
    {
        descriptionPanel = panel;
        descriptionNameText = panel.transform.Find("TopPart/Name Text").GetComponent<TMP_Text>();
        descriptionAmountText = panel.transform.Find("TopPart/Amount Text").GetComponent<TMP_Text>();
        descriptionPriceText = panel.transform.Find("TopPart/Price Text").GetComponent<TMP_Text>();
        descriptionText = panel.transform.Find("Description Text").GetComponent<TMP_Text>();
    }

    public float GetCost()
    {
        return itemCost * count;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentScene == 0)
        {
            isSelected = true;
            selectedBox.SetActive(true);
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeIn());
        }
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
        descriptionPanel.SetActive(true);
        descriptionAmountText.font = font;
        descriptionNameText.font = font;
        descriptionPriceText.font = font;
        descriptionText.font = font;
        descriptionNameText.text = itemName;
        int playerAmount = ItemManager.Instance.inventory.Find(x => x.id == id) != null ? ItemManager.Instance.inventory.Find(x => x.id == id).amount : 0;
        descriptionAmountText.text = currentLanguage switch
        {
            Language.English => "You have: ",
            Language.Chinese => "你有：",
            Language.Japanese => "あなたは：",
            _ => "You have: ",
        } + $"<size=30>{playerAmount}</size>";
        descriptionPriceText.text = $"{itemCost:F1}" + currentLanguage switch
        {
            Language.English => " yuan",
            Language.Chinese => " 元",
            Language.Japanese => " 円",
            _ => " yuan",
        };
        descriptionText.text = itemDescription;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentScene == 0)
        {
            isSelected = false;
            descriptionPanel.SetActive(false);
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeOut());
        }
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
