using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelMenu");
    }

    public void LeaveLevelMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void LoadLevelThree()
    {
        SceneManager.LoadScene("Level 3");
    }
}
