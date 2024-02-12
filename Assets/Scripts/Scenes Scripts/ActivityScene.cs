using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityScene : MonoBehaviour
{
    [Header("Events Sender: Load Scene")]
    public SceneLoadEventSO loadEventSO;
    [Header("Scene Script Object: Shop Scene")]
    public GameSceneSO shopScene;


    public void ShopButtonClicked()
    {
        loadEventSO.RaiseEvent(shopScene, true);
    }
}
