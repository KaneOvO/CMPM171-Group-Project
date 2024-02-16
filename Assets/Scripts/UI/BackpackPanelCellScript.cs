using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class BackpackPanelCellScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform icon;
    public TextMeshProUGUI itemAmountText;
    public GameObject selectedBox;
    public GameObject descriptionPanel;
    public TextMeshProUGUI descriptionAmountText;
    public TextMeshProUGUI descriptionPrice;
    public TextMeshProUGUI descriptionText;
    public BackpackPanelScript backpackPanelScript;
    public Item item;
    public float fadeDuration = 0.2f;
    private int itemAmount;
    private bool isSelected;
    private Coroutine fadeCoroutine;

    private void OnEnable()
    {
        icon = icon ? icon : transform.Find("Icon Mask/Icon").transform;
        itemAmountText = itemAmountText ? itemAmountText : transform.Find("Bottom BG/Amount Text").GetComponent<TextMeshProUGUI>();
        selectedBox = selectedBox ? selectedBox : transform.Find("Selected Box").gameObject;
        selectedBox.SetActive(false);
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

    public void Refresh(string id, int amount = 0)
    {
        item = ItemManager.Instance.ID(id);
        itemAmount = amount;
        Texture2D texture = Resources.Load<Texture2D>(item.spriteUrl);
        icon.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        itemAmountText.text = $"{amount.ToString()}";
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
        descriptionPanel.SetActive(true);
        descriptionAmountText.text = $"Amount: <size=40>{itemAmount.ToString()}</size>";
        descriptionPrice.text = $"$: <color=#FF0>{item.price[(int)Price.Default].ToString("F1")}</color>";
        descriptionText.text = item.description[(int)GameManager.Instance.saveData.currentLanguage];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isSelected = false;
        descriptionPanel.SetActive(false);
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