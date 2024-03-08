using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class SettlementPanelScript : MonoBehaviour
{
    [Header("Description Panel Settings")]
    public GameObject descriptionPanel;
    public TextMeshProUGUI descriptionNameText;
    public TextMeshProUGUI descriptionAmountText;
    public TextMeshProUGUI descriptionPriceText;
    public TextMeshProUGUI descriptionText;
    [Header("Total Money")]
    public float totalMoney = 0f;
    public TextMeshProUGUI totalMoneyText;
    [Header("Container Settings")]
    public GameObject content;
    [Header("Sell Item Cell Prefab")]
    public GameObject settlementItemCellPrefab;
    public List<InventoryItem> sellInventory => ItemManager.Instance.sellInventory;
    public TMP_FontAsset font => UIManager.Instance.font;

    private void OnEnable()
    {
        descriptionPanel = descriptionPanel == null ? transform.Find("Description Panel").gameObject : descriptionPanel;
        descriptionNameText = descriptionNameText == null ? transform.Find("Description Panel/TopPart/Name Text").GetComponent<TextMeshProUGUI>() : descriptionNameText;
        descriptionNameText.font = font;
        descriptionAmountText = descriptionAmountText == null ? transform.Find("Description Panel/TopPart/Amount Text").GetComponent<TextMeshProUGUI>() : descriptionAmountText;
        descriptionAmountText.font = font;
        descriptionPriceText = descriptionPriceText == null ? transform.Find("Description Panel/TopPart/Price Text").GetComponent<TextMeshProUGUI>() : descriptionPriceText;
        descriptionPriceText.font = font;
        descriptionText = descriptionText == null ? transform.Find("Description Panel/Description Text").GetComponent<TextMeshProUGUI>() : descriptionText;
        descriptionText.font = font;
        descriptionPanel.SetActive(false);
        totalMoney = 0f;
        totalMoneyText = totalMoneyText == null ? transform.Find("Scroll View/Toatal Money Text").GetComponent<TextMeshProUGUI>() : totalMoneyText;
        totalMoneyText.font = font;
        content = content == null ? transform.Find("Scroll View/Viewport/Content").gameObject : content;
        StartCoroutine(Refresh());
    }

    protected IEnumerator Refresh()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        if (sellInventory.Count == 0) { yield break; }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < sellInventory.Count; i++)
        {
            string id = sellInventory[i].id;
            int amount = Math.Abs(sellInventory[i].amount);
            GameObject settlementItemCell = Instantiate(settlementItemCellPrefab, content.transform);
            settlementItemCell.name = id;
            SettlementItemCellScript cellScript = settlementItemCell.GetComponent<SettlementItemCellScript>();
            cellScript.settlementPanelScript = this;
            cellScript.Refresh(id, amount);
            yield return AddMoneyAnimation(id, amount);
        }
    }
    private IEnumerator AddMoneyAnimation(string id, int amount)
    {
        Item item = ItemManager.Instance.ID(id);
        float start = totalMoney;
        float end = start + amount * item.sellPrice;
        float elapsedTime = 0f;
        string displayString = GameManager.Instance.saveData.currentLanguage switch
        {
            Language.English => $"Total:{totalMoney:F1} yuan",
            Language.Chinese => $"共计:{totalMoney:F1} 元",
            Language.Japanese => $"合計:{totalMoney:F1} 円",
            _ => $"Total:{totalMoney:F1} yuan",
        };
        while (elapsedTime < 1f && totalMoney < end)
        {
            totalMoney = Mathf.Lerp(start, end, elapsedTime);
            displayString = GameManager.Instance.saveData.currentLanguage switch
            {
                Language.English => $"Total:{totalMoney:F1} yuan",
                Language.Chinese => $"共计:{totalMoney:F1} 元",
                Language.Japanese => $"合計:{totalMoney:F1} 円",
                _ => $"Total:{totalMoney:F1} yuan",
            };
            totalMoneyText.text = displayString;
            elapsedTime += Time.deltaTime;
            yield return null;
            if (Input.GetMouseButtonDown(0))
            {
                totalMoney = end;
            }
        }
        totalMoney = end;
        displayString = GameManager.Instance.saveData.currentLanguage switch
        {
            Language.English => $"Total:{totalMoney:F1} yuan",
            Language.Chinese => $"共计:{totalMoney:F1} 元",
            Language.Japanese => $"合計:{totalMoney:F1} 円",
            _ => $"Total:{totalMoney:F1} yuan",
        };
        totalMoneyText.text = displayString;
    }
}
