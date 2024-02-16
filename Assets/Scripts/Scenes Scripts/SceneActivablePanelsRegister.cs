using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneActivablePanelsRegister : MonoBehaviour
{
    [Header("Activable Panels in Scene")]
    public List<GameObject> panels;
    private void OnEnable()
    {
        UIManager.Instance.panels?.AddRange(panels);
    }
    private void OnDisable()
    {
        UIManager.Instance.panels?.RemoveAll(x => panels.Contains(x));
    }
}
