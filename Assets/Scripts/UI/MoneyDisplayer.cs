using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplayer : MonoBehaviour
{
    public PlayerState playerState => PlayerStateManager.Instance.playerState;
    public TextMeshProUGUI totalMoneyText;

    // Update is called once per frame
    void Update()
    {
        totalMoneyText.text = $"$:{playerState.money.ToString("F1")}";
    }
}
