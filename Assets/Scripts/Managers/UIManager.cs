using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI testText;
    private static UIManager _instance;
    [Header("Events Sender: Bckpack Clikcked")]
    public VoidEventSO backpackClickedEvent;
    public GameObject backpackPanel;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        transform.parent = GameObject.FindWithTag("ManagersContainer").transform;
        testText.gameObject.SetActive(false);
    }
    void OnEnable()
    {
        backpackClickedEvent.onEventRaised += HandleBackpackClickedEvent;
    }
    void OnDisable()
    {
        backpackClickedEvent.onEventRaised -= HandleBackpackClickedEvent;
    }
    private void HandleBackpackClickedEvent()
    {
        if (backpackPanel.activeSelf)
        {
            backpackPanel.SetActive(false);
        }
        else
        {
            backpackPanel.SetActive(true);
        }
    }
}
