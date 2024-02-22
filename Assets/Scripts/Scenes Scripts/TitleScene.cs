using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
      loadEventSO.RaiseEvent(gameStartScene, true);
   }

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
