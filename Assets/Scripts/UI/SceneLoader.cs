using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
    public void UnloadThisScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
    public void GoToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void GoToMenuScene()
    {
        Debug.Log("GoToMenuScene");
        SceneManager.LoadScene(0);
    }
    public void GoToGameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToNextLeveScene(float wait)
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1, wait));
    }
    public void GoToLooseScene(float wait)
    {
        StartCoroutine(LoadScene("Loose", wait));
    }

    public void GoToWinScene(float wait)
    {
        StartCoroutine(LoadScene("Win", wait));
    }

    IEnumerator LoadScene(int index, float wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene(index);
    }
    IEnumerator LoadScene(string name, float wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene(name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
