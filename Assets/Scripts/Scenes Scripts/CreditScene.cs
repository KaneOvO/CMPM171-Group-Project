using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CreditScene : MonoBehaviour
{
    [Header("Events Sender: Load Scene")]
    public SceneLoadEventSO loadEventSO;
    [Header("Scene Script Object: Title Scene")]
    public AssetReference titleScene;

    public void BackButtonClicked()
    {
        loadEventSO.RaiseEvent(titleScene, true);
    }
}
