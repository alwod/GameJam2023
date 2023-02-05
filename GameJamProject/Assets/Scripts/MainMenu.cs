using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Play flamethrower sound effect
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene("Introduction");
    }

    public void ExitGame()
    {
        // Play flamethrower sound effect
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Application.Quit();
    }
}
