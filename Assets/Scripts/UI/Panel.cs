using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Reflection;
using UnityEngine.Networking;

public class Panel : MonoBehaviour
{
    public PrefabController selectedPrefab;
    public Image detailedItemImage;
    public TMP_Text nameText;
    public TMP_Text detailedDescriptionText;
    public TMP_Text detailedItemCostText;
    public TMP_Text counterText;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    public void UpdateDetailPanel(PrefabController selectItem)
    {
        selectedPrefab = selectItem;
        gameObject.SetActive(true);
        counterText.text = selectedPrefab.count.ToString();
        nameText.text = selectItem.itemName;
        detailedItemImage.sprite = selectItem.itemImage.sprite;
        detailedDescriptionText.text = $"Description: {selectItem.itemDescription}";
        detailedItemCostText.text = $"${selectItem.costText.text:F2}";;
        counterText.text = selectedPrefab.count.ToString();
    }

    public void plusPressed()
    {
        selectedPrefab.IncreaseCount();
    }

    public void minusPressed()
    {
        selectedPrefab.DecreaseCount();
        
    }
}
