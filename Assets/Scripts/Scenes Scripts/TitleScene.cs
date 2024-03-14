using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using System.IO;

public class TitleScene : MonoBehaviour
{
   [Header("Events Sender: Load Scene")]
   public SceneLoadEventSO loadEventSO;
   [Header("Events Sender: Game Start")]
   public AssetReference gameStartScene;
   [Header("Scene Script Object: Intro Scene")]
   public AssetReference IntroScene;
   [Header("Scene Script Object: Credit Scene")]
   public AssetReference creditScene;
   public GameObject LoadButton;


   public void StartButtonClicked()
   {
      StartCoroutine(StartCliked());
   }

   public IEnumerator StartCliked()
   {
      string initSaveDataFilePath = Path.Combine(Application.streamingAssetsPath, "init_SaveData.json");
      yield return GameManager.Instance.LoadJsonFileAsync<SaveData>(initSaveDataFilePath, (data) => GameManager.Instance.saveData = data);
      loadEventSO.RaiseEvent(IntroScene, true);
   }

   // public void LoadButtonClicked()
   // {
   //    StartCoroutine(LoadCliked());
   // }

   // public IEnumerator LoadCliked()
   // {
   //    string saveDataFilePath = Path.Combine(Application.streamingAssetsPath, "SaveData.json");
   //    yield return GameManager.Instance.LoadJsonFileAsync<SaveData>(saveDataFilePath, (data) => GameManager.Instance.saveData = data);
   //    loadEventSO.RaiseEvent(gameStartScene, true);
   // }

   public void LoadButtonClicked()
   {
      LoadCliked();
   }

   private void LoadCliked()
   {
      string saveDataFilePath = PlayerPrefs.GetString("SaveData");
      GameManager.Instance.saveData = JsonUtility.FromJson<SaveData>(saveDataFilePath);
      loadEventSO.RaiseEvent(gameStartScene, true);
   }

   public void CreditButtonClicked()
   {
      loadEventSO.RaiseEvent(creditScene, true);
   }

   public void QuitButtonClicked()
   {
      GameManager.Instance.Save();
      Application.Quit();
   }
   public void Awake()
   {
      if (PlayerPrefs.HasKey("SaveData"))
      {
         LoadButton.SetActive(true);
      }
      else
      {
         LoadButton.SetActive(false);
      }
   }
}
