using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
   [Header("Events Sender: Load Scene")]
   public SceneLoadEventSO loadEventSO;
   [Header("Events Sender: Game Start")]
   public VoidEventSO gameStartEvent;
   [Header("Scene Script Object: Game Scene")]
   public GameSceneSO gameStartScene;
   [Header("Scene Script Object: Credit Scene")]
   public GameSceneSO creditScene;

   public void StartButtonClicked()
   {
      loadEventSO.RaiseEvent(gameStartScene, true);
      gameStartEvent.RaiseEvent();
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
