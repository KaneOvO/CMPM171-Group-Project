using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplayer : MonoBehaviour
{
    public PlayerState playerState => PlayerStateManager.Instance.playerState;
    private TextMeshProUGUI totalMoneyText;

    void OnEnable()
    {
        totalMoneyText = gameObject.GetComponent<TextMeshProUGUI>();
        UIManager.Instance.onLanguageChange.AddListener(Refresh);
        Refresh();
    }
    void OnDisable()
    {
        UIManager.Instance.onLanguageChange.RemoveListener(Refresh);
    }
    public void Refresh()
    {
        totalMoneyText.font = UIManager.Instance.font;
        totalMoneyText.text = GameManager.Instance.saveData.currentLanguage switch
        {
            Language.English => $"{playerState.money:F1} yuan",
            Language.Chinese => $"{playerState.money:F1} 元",
            Language.Japanese => $"{playerState.money:F1} 円",
            _ => $"{playerState.money:F1} yuan",
        };
    }
}
