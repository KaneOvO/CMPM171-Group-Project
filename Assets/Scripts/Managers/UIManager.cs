using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public List<GameObject> panels;
    private static UIManager _instance;
    [Header("Events Listener: Panel Called")]
    public StringParameterEventSO PanelCalledEvent;

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
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject openedPanel = panels.Find(x => x.activeSelf);
            if (openedPanel) { openedPanel.SetActive(false); } else { HandlePanelCalledEvent("SettingsPanel"); }
        }
    }
    void OnEnable()
    {
        PanelCalledEvent.onEventRaised += HandlePanelCalledEvent;
    }
    void OnDisable()
    {
        PanelCalledEvent.onEventRaised -= HandlePanelCalledEvent;
    }
    private void HandlePanelCalledEvent(string tag)
    {
        GameObject targetPanel = panels.Find(x => x.tag == tag);
        if (!targetPanel) return;
        bool tempBool = targetPanel.activeSelf;
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        targetPanel.SetActive(!tempBool);
    }
}
