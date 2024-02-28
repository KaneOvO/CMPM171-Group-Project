using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;


public class SettlementScene : MonoBehaviour
{
    [Header("Events Sender: Load Scene")]
    public SceneLoadEventSO loadEventSO;
    public AssetReference saleEndScene;
    public void NextButtonClicked()
    {
        loadEventSO.RaiseEvent(saleEndScene, true);
    }
}
