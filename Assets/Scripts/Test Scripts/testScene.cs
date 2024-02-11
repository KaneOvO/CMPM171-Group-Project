using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScene : MonoBehaviour
{
    public VoidEventSO onJsonLoad;
    private void OnEnable()
    {
        onJsonLoad.onEventRaised += setStage;
    }
    private void OnDisable()
    {
        onJsonLoad.onEventRaised -= setStage;
    }

    private void setStage()
    {
        if (GameManager.Instance.saveData.currentStage == Stage.Default)
        {
            StageManager.Instance.LoadStage(StageManager.Instance.DetermineNextStage());
        };
    }
}
