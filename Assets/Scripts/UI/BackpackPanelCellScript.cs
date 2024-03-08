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
    public BackpackPanelScript backpackPanelScript;
    public TMP_FontAsset font => UIManager.Instance.font;
    public GameObject descriptionPanel => backpackPanelScript.descriptionPanel;
    public TextMeshProUGUI descriptionNameText => backpackPanelScript.descriptionNameText;
    public TextMeshProUGUI descriptionAmountText => backpackPanelScript.descriptionAmountText;
    public TextMeshProUGUI descriptionPriceText => backpackPanelScript.descriptionPriceText;
    public TextMeshProUGUI descriptionText => backpackPanelScript.descriptionText;
    [HideInInspector] public Item item;
    public float fadeDuration = 0.2f;
    [HideInInspector] public int itemAmount;
    protected bool isSelected;
    protected Coroutine fadeCoroutine;

    protected virtual void OnEnable()
    {
        icon = icon ? icon : transform.Find("Icon Mask/Icon").transform;
        itemAmountText = itemAmountText ? itemAmountText : transform.Find("Bottom BG/Amount Text").GetComponent<TextMeshProUGUI>();
        selectedBox = selectedBox ? selectedBox : transform.Find("Selected Box").gameObject;
        selectedBox.SetActive(false);
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

    public virtual void Refresh(string id, int amount = 0)
    {
        item = ItemManager.Instance.ID(id);
        itemAmount = amount;
        icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.spriteUrl);
        itemAmountText.font = font;
        itemAmountText.text = $"{amount.ToString()}";
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        isSelected = true;
        selectedBox.SetActive(true);
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeIn());
    }

    protected virtual IEnumerator FadeIn()
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
        string itemName = item.name[(int)GameManager.Instance.saveData.currentLanguage];
        descriptionNameText.text = $"{itemName}";
        descriptionAmountText.text = GameManager.Instance.saveData.currentLanguage switch
        {
            Language.English => "Amount: ",
            Language.Chinese => "数量: ",
            Language.Japanese => "個数: ",
            _ => "Amount: ",
        }
        + $"{itemAmount.ToString()}";
        descriptionPriceText.text = GameManager.Instance.saveData.currentLanguage switch
        {
            Language.English => $"{item.originalPrice:F1} yuan",
            Language.Chinese => $"{item.originalPrice:F1} 元",
            Language.Japanese => $"{item.originalPrice:F1} 円",
            _ => $"{item.originalPrice:F1} yuan",
        };
        descriptionText.text = item.description[(int)GameManager.Instance.saveData.currentLanguage];
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        isSelected = false;
        descriptionPanel.SetActive(false);
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeOut());
    }
    protected virtual IEnumerator FadeOut()
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
