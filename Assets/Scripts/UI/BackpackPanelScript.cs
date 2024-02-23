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
    public GameObject content;
    public GameObject itemCellPrefab;
    public GameObject descriptionPanel;
    public TextMeshProUGUI descriptionAmountText;
    public TextMeshProUGUI descriptionPrice;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI totalMoneyText;
    public List<InventoryItem> inventory => ItemManager.Instance.inventory;
    public PlayerState playerState => PlayerStateManager.Instance.playerState;
    protected virtual void OnEnable()
    {
        content = content ? content : transform.Find("Scroll View/Viewport/Content").gameObject;
        itemCellPrefab = itemCellPrefab ? itemCellPrefab : Resources.Load<GameObject>("Backpack Item Cell");
        descriptionPanel = descriptionPanel ? descriptionPanel : transform.Find("Description Panel").gameObject;
        descriptionAmountText = descriptionAmountText ? descriptionAmountText : transform.Find("Description Panel/Amount Text").GetComponent<TextMeshProUGUI>();
        descriptionPrice = descriptionPrice ? descriptionPrice : transform.Find("Description Panel/Price Text").GetComponent<TextMeshProUGUI>();
        descriptionText = descriptionText ? descriptionText : transform.Find("Description Panel/Description Text").GetComponent<TextMeshProUGUI>();
        descriptionPanel.SetActive(false);
        totalMoneyText = totalMoneyText ? totalMoneyText : transform.Find("Total Money Text").GetComponent<TextMeshProUGUI>();
        Refresh();
    }
    public virtual void Refresh()
    {
        totalMoneyText.text = $"$:<color=#FF0>{playerState.money.ToString("F1")}</color>";
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
            cellScript.descriptionPanel = descriptionPanel;
            cellScript.descriptionAmountText = descriptionAmountText;
            cellScript.descriptionPrice = descriptionPrice;
            cellScript.descriptionText = descriptionText;
            cellScript.backpackPanelScript = this;
            cellScript.Refresh(item.id, item.amount);
        }
    }
}
