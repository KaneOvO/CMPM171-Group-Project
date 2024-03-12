using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
[RequireComponent(typeof(Button))]
public class BackTitleButton : MonoBehaviour
{
    [Header("Events Sender: Load Scene")]
    public SceneLoadEventSO loadEventSO;
    [Header("Scene Script Object: Title Scene")]
    public AssetReference titleScene;
    [HideInInspector] Button button;
    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(BackButtonClicked);
    }
    public void BackButtonClicked()
    {
        loadEventSO.RaiseEvent(titleScene, true);
    }
    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }
}
