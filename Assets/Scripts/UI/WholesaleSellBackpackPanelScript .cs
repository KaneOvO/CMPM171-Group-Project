using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using System;
using TMPro;

public class WholesaleSellBackpackPanelScript : BackpackPanelScript

{
    public int currentScene;
    public GameObject displayItemPrefab;
    public Transform parentForDisplayItem;
    public override void Refresh()
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
            SellBackpackPanelCellScript cellScript = newItemCell.GetComponent<SellBackpackPanelCellScript>();
            newItemCell.name = item.id;
            cellScript.descriptionPanel = descriptionPanel;
            cellScript.descriptionNameText = descriptionNameText;
            cellScript.descriptionAmountText = descriptionAmountText;
            cellScript.descriptionPriceText = descriptionPriceText;
            cellScript.descriptionText = descriptionText;
            cellScript.backpackPanelScript = this;
            cellScript.Refresh(item.id, item.amount);
        }
    }
    public virtual void addToDisplay(Item item, SellBackpackPanelCellScript sellBackpackPanelCellScript)
    {
        GameObject newItemPrefab = Instantiate(displayItemPrefab, parentForDisplayItem);
        newItemPrefab.name = item.id;
        Sprite loadedSprite = Resources.Load<Sprite>(item.spriteUrl);
        PrefabController prefabController = newItemPrefab.GetComponent<PrefabController>();
        prefabController.setItem(item);
        prefabController.sellBackpackPanelCellScript = sellBackpackPanelCellScript;
        prefabController.setPanelInScene(descriptionPanel);
        int count = ItemManager.Instance.inventory.Find(x => x.id == item.id).amount;
        prefabController.count = count;
        prefabController.closeButton.onClick.AddListener(() => removeFromDisplay(item.id, sellBackpackPanelCellScript, count));
        prefabController.UpdateCounterText();
        prefabController.setCurrentScene(currentScene);
        prefabController.Refresh();
        sellBackpackPanelCellScript.itemAmount = 0;
        sellBackpackPanelCellScript.itemAmountText.text = $"{sellBackpackPanelCellScript.itemAmount.ToString()}";
    }
    public virtual void removeFromDisplay(string id, SellBackpackPanelCellScript sellBackpackPanelCellScript, int count)
    {
        sellBackpackPanelCellScript.itemAmount = count;
        sellBackpackPanelCellScript.itemAmountText.text = $"{sellBackpackPanelCellScript.itemAmount.ToString()}";
        Transform childToRemove = parentForDisplayItem.Find(id);
        if (childToRemove != null)
        {
            Destroy(childToRemove.gameObject);
        }

        Transform childInContent = content.transform.Find(id);
        if (childInContent != null)
        {
            SellBackpackPanelCellScript cellScript = childInContent.GetComponent<SellBackpackPanelCellScript>();
            if (cellScript != null)
            {
                cellScript.alreadyAdded = false;
            }
        }
    }
}
