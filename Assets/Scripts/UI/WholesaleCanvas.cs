using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WholesaleCanvas : MonoBehaviour
{
    [Header("Functional Script Setting")]
    public Wholesale wholesale;
    [Header("Canvas Setting")]
    public Transform displayGrid;
    public GameObject descriptionPanel;
    public GameObject itemPrefab;
    public List<Item> items;
    public TMP_FontAsset font => UIManager.Instance.font;
    public Language currentLanguage => GameManager.Instance.playerConfig.currentLanguage;
    [Header("Player Info Display")]
    public TMP_Text totalCostText;
    public TMP_Text moneyText;

    public void Start()
    {
        descriptionPanel = GameObject.Find("Description Panel");
        descriptionPanel.SetActive(false);
        totalCostText.font = font;
        moneyText.font = font;

        string content = currentLanguage switch
        {
            Language.English => $"Money:{GameManager.Instance.saveData.playerState.money:F1} yuan",
            Language.Chinese => $"资金：{GameManager.Instance.saveData.playerState.money:F1} 元",
            Language.Japanese => $"資金:{GameManager.Instance.saveData.playerState.money:F1} 円",
            _ => $"Money:{GameManager.Instance.saveData.playerState.money:F1} yuan",
        };
        moneyText.text = content;
    }

    public void SetTotalCost(float totalCost)
    {
        string content = currentLanguage switch
        {
            Language.English => $"Total: {totalCost:F1} yuan",
            Language.Chinese => $"合计：{totalCost:F1} 元",
            Language.Japanese => $"合計額：{totalCost:F1} 円",
            _ => $"Total: {totalCost:F1} yuan"
        };
        totalCostText.text = content;
    }

    public void CreateItemPrefabs()
    {
        foreach (Item item in items)
        {
            GameObject newItemPrefab = Instantiate(itemPrefab, displayGrid);
            newItemPrefab.name = item.id;
            PrefabController prefabController = newItemPrefab.GetComponent<PrefabController>();
            if (prefabController != null)
            {
                prefabController.SetItem(item);
                prefabController.wholesaleCanvas = this;
                prefabController.wholesale = wholesale;
                prefabController.SetCurrentScene(0);
                prefabController.SetPanelInScene(descriptionPanel);
                prefabController.Refresh();
            }
        }
    }
}
