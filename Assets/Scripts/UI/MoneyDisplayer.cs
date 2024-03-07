using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplayer : MonoBehaviour
{
    public PlayerState playerState => PlayerStateManager.Instance.playerState;
    private TextMeshProUGUI totalMoneyText;

    // Update is called once per frame
    void Awake()
    {
        totalMoneyText = gameObject.GetComponent<TextMeshProUGUI>();
        totalMoneyText.text = $"$:{playerState.money.ToString("F1")}";
    }
}
