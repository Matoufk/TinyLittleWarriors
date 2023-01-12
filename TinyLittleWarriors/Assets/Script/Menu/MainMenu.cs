using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void SettingsButton()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
