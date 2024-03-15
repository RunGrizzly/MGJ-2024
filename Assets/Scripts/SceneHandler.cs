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

        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        // SceneManager.LoadSceneAsync("Leaderboard", LoadSceneMode.Additive);
        // SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
    }


    private void LoadScene(string sceneName)

    {


    }


}
