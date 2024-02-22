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
    public List<InventoryItem> inventory => ItemManager.Instance.inventory;


    public int currentScene;
    public GameObject displayItemPrefab;
    public Transform parentForDisplayItem;
    private void OnEnable()
    {
        content = content ? content : transform.Find("Scroll View/Viewport/Content").gameObject;
        itemCellPrefab = itemCellPrefab ? itemCellPrefab : Resources.Load<GameObject>("Backpack Item Cell");
        descriptionPanel = descriptionPanel ? descriptionPanel : transform.Find("Description Panel").gameObject;
        descriptionAmountText = descriptionAmountText ? descriptionAmountText : transform.Find("Description Panel/Amount Text").GetComponent<TextMeshProUGUI>();
        descriptionPrice = descriptionPrice ? descriptionPrice : transform.Find("Description Panel/Price Text").GetComponent<TextMeshProUGUI>();
        descriptionText = descriptionText ? descriptionText : transform.Find("Description Panel/Description Text").GetComponent<TextMeshProUGUI>();
        descriptionPanel.SetActive(false);
        Refresh();
    }
    public void Refresh()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        if (inventory == null) { Debug.Log("inventory is null"); return; }
        foreach (var item in inventory)
        {
            GameObject newItemCell = Instantiate(itemCellPrefab, content.transform);
            BackpackPanelCellScript cellScript = newItemCell.GetComponent<BackpackPanelCellScript>();
            newItemCell.name = item.id;
            cellScript.currentScene = currentScene;
            cellScript.descriptionPanel = descriptionPanel;
            cellScript.descriptionAmountText = descriptionAmountText;
            cellScript.descriptionPrice = descriptionPrice;
            cellScript.descriptionText = descriptionText;
            cellScript.backpackPanelScript = this;
            cellScript.Refresh(item.id, item.amount);
        }
    }
    public void addToDisplay(Item item)
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
    public void removeFromDisplay(string id)
    {
        Transform childToRemove = parentForDisplayItem.Find(id);
        if (childToRemove != null)
        {
            Destroy(childToRemove.gameObject);
        }

        Transform childInContent = content.transform.Find(id);
        if (childInContent != null)
        {
            BackpackPanelCellScript cellScript = childInContent.GetComponent<BackpackPanelCellScript>();
            if (cellScript != null)
            {
                cellScript.alreadyAdded = false;
            }
        }
    }
}
