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
    public int currentScene;
    private string id;
    public Image itemImage;
    public TMP_Text costText;
    public TMP_Text counterText;
    public GameObject panelInScene;
    public string itemName;
    public string itemDescription;
    public int count = 0;
    public float itemCost = 0;
    public int maxStack;
    public GameObject selectedBox;
    private bool isSelected;
    private Coroutine fadeCoroutine;
    public float fadeDuration = 0.2f;
    public TMP_Text descriptionAmountText;
    public TMP_Text descriptionPrice;
    public TMP_Text descriptionText;
    public Button increaseCountButton;
    public Button decreaseCOuntButton;
    public Button closeButton;
    public Wholesale wholesale;
    bool isAdding = false;
    bool isDecreasing = false;
    void Start()
    {
        if (currentScene == 0)
        {
            closeButton.gameObject.SetActive(false);
        }
        UpdateButtonState();
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
        isAdding = true;
        InventoryItem item = ItemManager.Instance.inventory.Find(x => x.id == id);
        int amount = item != null ? item.amount : 0;
        if (currentScene == 0 && amount + count < maxStack)
        {
            count++;
            UpdateCounterText();
            // Optionally, update the button's interactability here or in UpdateCounterText
        }
        else if (currentScene == 1 && count < amount)
        {
            count++;
            UpdateCounterText();
        }
        UpdateButtonState();
    }

    public void DecreaseCount()
    {
        isDecreasing = true;
        if (count > 0)
        {
            count--;
            UpdateCounterText();
        }
        UpdateButtonState();
    }

    public void UpdateButtonState()
    {
        InventoryItem item = ItemManager.Instance.inventory.Find(x => x.id == id);
        int amount = item != null ? item.amount : 0;
        if (currentScene == 0)
        {
            increaseCountButton.interactable = amount + count < maxStack;
            decreaseCOuntButton.interactable = count > 0;
        }
        else if (currentScene == 1)
        {

            increaseCountButton.interactable = count < amount;
            if (count == 0)
            {
                panelInScene.transform.parent.GetComponent<WholesaleSellBackpackPanelScript>().removeFromDisplay(id);
            }
        }
    }

    public void setCurrentScene(int scene)
    {
        currentScene = scene;
    }

    public void setItem(Item item)
    {
        SetItemID(item.id);
        SetItemName(item.name[(int)GameManager.Instance.saveData.currentLanguage]);
        SetItemDescription(item.description[(int)GameManager.Instance.saveData.currentLanguage]);
        SetItemImage(item.spriteUrl);
        SetItemCost(item.originalPrice);
        maxStack = item.maxStack;
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
        costText.text = $"${cost:F2}";
    }

    public void UpdateCounterText()
    {
        counterText.text = count.ToString();
        if (currentScene == 0)
        {
            wholesale.CalculateTotalCost();
        }
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
        return (int)itemCost * count;
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
        panelInScene.SetActive(true);
        int playerAmount = ItemManager.Instance.inventory.Find(x => x.id == id) != null ? ItemManager.Instance.inventory.Find(x => x.id == id).amount : 0;
        descriptionAmountText.text = $"You have: <size=40>{playerAmount}</size>";
        descriptionPrice.text = $"$: <color=#FF0>{itemCost:F2}</color>";
        descriptionText.text = itemDescription;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentScene == 0)
        {
            isSelected = false;
            panelInScene.SetActive(false);
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
