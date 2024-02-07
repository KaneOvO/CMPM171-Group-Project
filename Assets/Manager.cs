using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }
    public int DangerLevel = 50;
    public TextMeshProUGUI text;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    void Update()
    {
        text.text = "Danger Level: " + DangerLevel.ToString();
    }

    public void DangerLevelUp()
    {
        DangerLevel += 50;
        Debug.Log(DangerLevel);

    }

    public void DangerLevelDown()
    {
        DangerLevel -= 50;
        Debug.Log(DangerLevel);
    }

    public void ResetDangerLevel()
    {
        DangerLevel = 50;
    }
    
}
