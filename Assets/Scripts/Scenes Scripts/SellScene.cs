using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using System;
using TMPro;

public class SellScene : MonoBehaviour
{
    public Transform parentForDisplayItem;
    [Header("Events Sender: Load Scene")]
    public SceneLoadEventSO loadEventSO;
    public AssetReference saleEndScene;
    public AssetReference settlementScene;
    private void Start()
    {
        ItemManager.Instance.sellInventory.Clear();
        if (parentForDisplayItem == null) parentForDisplayItem = GameObject.Find("Canvas/Display").transform;
    }
    public void sellAll()
    {
        if (parentForDisplayItem.childCount == 0) loadEventSO.RaiseEvent(saleEndScene, true);
        foreach (Transform child in parentForDisplayItem)
        {
            PrefabController sellItem = child.GetComponent<PrefabController>();
            ItemManager.Instance.sellInventory.Add(new InventoryItem(child.name, -sellItem.count));
            PlayerStateManager.Instance.playerState.money += ItemManager.Instance.ID(child.name).originalPrice * sellItem.count;
        }
        ItemManager.Instance.AddItemAmount(ItemManager.Instance.sellInventory);
        loadEventSO.RaiseEvent(settlementScene, true);
    }
}
