using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityScene : MonoBehaviour
{
    [Header("Events Sender: Load Scene")]
    public SceneLoadEventSO loadEventSO;
    [Header("Events Sender: Backpack Clikcked")]
    public VoidEventSO backpackClickedEvent;
    [Header("Scene Script Object: Shop Scene")]
    public GameSceneSO shopScene;
    public void ShopButtonClicked()
    {
        loadEventSO.RaiseEvent(shopScene, true);
    }

    public void BackpackButtonClicked()
    {
        backpackClickedEvent.RaiseEvent();
    }
}
