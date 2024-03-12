using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.Events;
using System;
using TMPro;

public class BackpackPanelScript : MonoBehaviour
{
    public TMP_FontAsset[] fonts;
    public TMP_FontAsset font => UIManager.Instance.font;
    public GameObject content;
    public GameObject itemCellPrefab;
    public GameObject descriptionPanel;
    public TextMeshProUGUI descriptionNameText;
    public TextMeshProUGUI descriptionAmountText;
    public TextMeshProUGUI descriptionPriceText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI totalMoneyText;
    public List<InventoryItem> inventory => ItemManager.Instance.inventory;
    public PlayerState playerState => PlayerStateManager.Instance.playerState;
    public Language currentLanguage => GameManager.Instance.playerConfig.currentLanguage;
    protected virtual void OnEnable()
    {
        UIManager.Instance.onLanguageChange.AddListener(Refresh);
        content = content ? content : transform.Find("Scroll View/Viewport/Content").gameObject;
        itemCellPrefab = itemCellPrefab ? itemCellPrefab : Resources.Load<GameObject>("Backpack Item Cell");
        descriptionPanel = descriptionPanel ? descriptionPanel : transform.Find("Description Panel").gameObject;
        descriptionNameText = descriptionNameText ? descriptionNameText : transform.Find("Description Panel/Name Text").GetComponent<TextMeshProUGUI>();
        descriptionAmountText = descriptionAmountText ? descriptionAmountText : transform.Find("Description Panel/Amount Text").GetComponent<TextMeshProUGUI>();
        descriptionPriceText = descriptionPriceText ? descriptionPriceText : transform.Find("Description Panel/Price Text").GetComponent<TextMeshProUGUI>();
        descriptionText = descriptionText ? descriptionText : transform.Find("Description Panel/Description Text").GetComponent<TextMeshProUGUI>();
        descriptionPanel.SetActive(false);
        totalMoneyText = totalMoneyText ? totalMoneyText : transform.Find("Total Money Text").GetComponent<TextMeshProUGUI>();
        Refresh();
    }
    protected virtual void OnDisable()
    {
        UIManager.Instance.onLanguageChange.RemoveListener(Refresh);
    }
    public virtual void Refresh()
    {
        descriptionNameText.font = font;
        descriptionAmountText.font = font;
        descriptionPriceText.font = font;
        descriptionText.font = font;
        totalMoneyText.font = font;
        totalMoneyText.text = currentLanguage switch
        {
            Language.English => $"{playerState.money:F1} yuan",
            Language.Chinese => $"{playerState.money:F1} 元",
            Language.Japanese => $"{playerState.money:F1} 円",
            _ => $"{playerState.money:F1} yuan",
        };
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        if (inventory == null) return;
        foreach (var item in inventory)
        {
            GameObject newItemCell = Instantiate(itemCellPrefab, content.transform);
            BackpackPanelCellScript cellScript = newItemCell.GetComponent<BackpackPanelCellScript>();
            newItemCell.name = item.id;
            cellScript.backpackPanelScript = this;
            cellScript.Refresh(item.id, item.amount);
        }
    }
}
