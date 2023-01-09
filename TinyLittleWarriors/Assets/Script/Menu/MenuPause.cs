using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public static bool gameIsPause = false;

    public GameObject pauseMenuUI;

    public GameObject winScreenUI;

    public GameObject looseScreenUI;

    public bool win = false;
    public bool loose = false;

    public string sceneToRestartTo = "mathis_scene";



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(sceneToRestartTo);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Victory()
    {
        winScreenUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Defeat()
    {
        looseScreenUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
