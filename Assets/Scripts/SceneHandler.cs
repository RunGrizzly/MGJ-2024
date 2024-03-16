using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private void Start()
    {

        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

        //LoadScenes(new List<string>() { "Cameras" });
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        // SceneManager.LoadSceneAsync("Leaderboard", LoadSceneMode.Additive);
        // SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
    }

    public void LoadScenes(List<string> sceneNames)
    {
        foreach (string sceneName in sceneNames)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }

    public void UnloadScenes(List<string> sceneNames)
    {
        foreach (string sceneName in sceneNames)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }


}
