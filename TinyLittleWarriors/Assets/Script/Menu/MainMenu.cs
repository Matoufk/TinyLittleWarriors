using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject settings;
    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void SettingsButton()
    {
        settings.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settings.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
