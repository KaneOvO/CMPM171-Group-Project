using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using System;
using TMPro;

public class SaleBackpackPanelScript : BackpackPanelScript

{
    public GameObject displayItemPrefab;
    public Transform parentForDisplayItem;
    public override void Refresh()
    {
        descriptionNameText.font = font;
        descriptionAmountText.font = font;
        descriptionPriceText.font = font;
        descriptionText.font = font;
        totalMoneyText.font = font;
        totalMoneyText.text = $"$:<color=#FF0>{playerState.money.ToString("F1")}</color>";
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        if (inventory == null) return;
        foreach (var item in inventory)
        {
            GameObject newItemCell = Instantiate(itemCellPrefab, content.transform);
            SaleBackpackCellScript cellScript = newItemCell.GetComponent<SaleBackpackCellScript>();
            newItemCell.name = item.id;
            cellScript.backpackPanelScript = this;
            cellScript.saleBackpackPanelScript = this;
            cellScript.Refresh(item.id, item.amount);
        }
    }
    public virtual void addToDisplay(Item item, SaleBackpackCellScript saleBackpackCellScript)
    {
        GameObject newItemPrefab = Instantiate(displayItemPrefab, parentForDisplayItem);
        newItemPrefab.name = item.id;
        PrefabController prefabController = newItemPrefab.GetComponent<PrefabController>();
        prefabController.saleBackpackPanelScript = this;
        prefabController.SetItem(item);
        prefabController.SetPanelInScene(descriptionPanel);
        prefabController.saleBackpackCellScript = saleBackpackCellScript;
        int count = ItemManager.Instance.inventory.Find(x => x.id == item.id).amount;
        prefabController.count = count;
        prefabController.closeButton.onClick.AddListener(() => removeFromDisplay(item.id, saleBackpackCellScript, prefabController.count));
        prefabController.UpdateCounterText();
        prefabController.SetCurrentScene(1);
        prefabController.Refresh();
        saleBackpackCellScript.itemAmount = 0;
        saleBackpackCellScript.itemAmountText.text = $"{saleBackpackCellScript.itemAmount.ToString()}";
    }
    public virtual void removeFromDisplay(string id, SaleBackpackCellScript saleBackpackCellScript, int count)
    {
        saleBackpackCellScript.itemAmount += count;
        saleBackpackCellScript.itemAmountText.text = $"{saleBackpackCellScript.itemAmount.ToString()}";
        Transform childToRemove = parentForDisplayItem.Find(id);
        if (childToRemove != null)
        {
            Destroy(childToRemove.gameObject);
        }

        Transform childInContent = content.transform.Find(id);
        if (childInContent != null)
        {
            SaleBackpackCellScript cellScript = childInContent.GetComponent<SaleBackpackCellScript>();
            if (cellScript != null)
            {
                cellScript.alreadyAdded = false;
            }
        }
    }
}
