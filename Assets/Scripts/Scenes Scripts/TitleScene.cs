using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.IO;

public class TitleScene : MonoBehaviour
{
   [Header("Events Sender: Load Scene")]
   public SceneLoadEventSO loadEventSO;
   [Header("Events Sender: Game Start")]
   public AssetReference gameStartScene;
   [Header("Scene Script Object: Credit Scene")]
   public AssetReference creditScene;


   public void StartButtonClicked()
   {
      string initSaveDataFilePath = Path.Combine(Application.streamingAssetsPath, "init_SaveData.json");
      StartCoroutine(GameManager.Instance.LoadJsonFileAsync<SaveData>(initSaveDataFilePath, (data) => GameManager.Instance.saveData = data));

      loadEventSO.RaiseEvent(gameStartScene, true);
   }

   public void LoadButtonClicked()
   {
      string saveDataFilePath = Path.Combine(Application.streamingAssetsPath, "SaveData.json");
      GameManager.Instance.LoadJsonFileAsync<SaveData>(saveDataFilePath, (data) => GameManager.Instance.saveData = data);
      loadEventSO.RaiseEvent(gameStartScene, true);
   }

   // public void checkSaveData()
   // {
   //    string saveDataFilePath = Path.Combine(Application.streamingAssetsPath, "SaveData.json");

   //    if (File.Exists(saveDataFilePath))
   //    {
   //       //Load按钮可以点亮
   //    }
   //    else
   //    {
   //       //Load按钮不可以点亮
   //    }
   // }

   public void CreditButtonClicked()
   {
      loadEventSO.RaiseEvent(creditScene, true);
   }

   public void QuitButtonClicked()
   {
      //TODO: save the game
      //GameManager.Instance.SaveGame();
      Application.Quit();
   }
}
