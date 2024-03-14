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

   public void LoadButtonClicked()
   {
      StartCoroutine(LoadCliked());
   }

   public IEnumerator LoadCliked()
   {
      string saveDataFilePath = Path.Combine(Application.streamingAssetsPath, "SaveData.json");
      yield return GameManager.Instance.LoadJsonFileAsync<SaveData>(saveDataFilePath, (data) => GameManager.Instance.saveData = data);
      loadEventSO.RaiseEvent(gameStartScene, true);
   }

   public void CreditButtonClicked()
   {
      loadEventSO.RaiseEvent(creditScene, true);
   }

   public void QuitButtonClicked()
   {
      //TODO: save the game
      GameManager.Instance.Save();
      Application.Quit();
   }
   public void Awake()
   {
      string saveDataFilePath = Path.Combine(Application.streamingAssetsPath, "SaveData.json");

      if (File.Exists(saveDataFilePath))
      {
         LoadButton.SetActive(true);
      }
      else
      {
         LoadButton.SetActive(false);
      }
   }
}
