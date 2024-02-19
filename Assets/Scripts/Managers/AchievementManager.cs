using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The plan has been abandoned.
/// </summary> <summary>
/// 
/// </summary>
public class AchievementManager : MonoBehaviour
{
    public GameManager gameManager => GameManager.Instance;
    public SaveData saveData => gameManager.saveData;
    public static AchievementManager _instance;
    public static AchievementManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AchievementManager>();

                if (_instance == null)
                {
                    GameObject AchievementManagerObject = new GameObject("AchievementManager");
                    _instance = AchievementManagerObject.AddComponent<AchievementManager>();
                    AchievementManagerObject.transform.parent = GameObject.FindWithTag("ManagersContainer").transform;
                }
            }
            return _instance;
        }
    }
}
