using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using System;
using TMPro;
using Fungus;

public class WholesaleSellBackpackPanelScript : BackpackPanelScript

{
    public int currentScene;
    public GameObject displayItemPrefab;
    public Transform parentForDisplayItem;
    public Flowchart flowchart;

    [Header("Events Sender: Load Scene")]
    public SceneLoadEventSO loadEventSO;
    public AssetReference SaleEndScene;
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
            cellScript.currentScene = currentScene;
            cellScript.descriptionPanel = descriptionPanel;
            cellScript.descriptionAmountText = descriptionAmountText;
            cellScript.descriptionPrice = descriptionPrice;
            cellScript.descriptionText = descriptionText;
            cellScript.backpackPanelScript = this;
            cellScript.currentScene = currentScene;
            cellScript.Refresh(item.id, item.amount);
        }
    }
    public virtual void addToDisplay(Item item)
    {
        GameObject newItemPrefab = Instantiate(displayItemPrefab, parentForDisplayItem);
        newItemPrefab.name = item.id;
        Sprite loadedSprite = Resources.Load<Sprite>(item.spriteUrl);
        PrefabController prefabController = newItemPrefab.GetComponent<PrefabController>();
        prefabController.setCurrentScene(currentScene);
        prefabController.setItem(item);
        prefabController.setPanelInScene(descriptionPanel);
        prefabController.closeButton.onClick.AddListener(() => removeFromDisplay(item.id));
        prefabController.count = ItemManager.Instance.inventory.Find(x => x.id == item.id).amount;
        prefabController.UpdateCounterText();

    }
    public virtual void removeFromDisplay(string id)
    {
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
    public void sellAll()
    {
        foreach (Transform child in parentForDisplayItem)
        {
            PrefabController sellItem = child.GetComponent<PrefabController>();
            flowchart.SetIntegerVariable(sellItem.itemName.Replace(" ", "_"), sellItem.count);
            ItemManager.Instance.AddItemAmount(child.name, -sellItem.count);
            PlayerStateManager.Instance.playerState.money += ItemManager.Instance.ID(child.name).originalPrice * sellItem.count;
        }
        //跳场景
        loadEventSO.RaiseEvent(SaleEndScene, true);
    }

}
