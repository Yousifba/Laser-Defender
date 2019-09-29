using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void WaitAndLoad()
    {
        StartCoroutine(LoadGameOverDelay());
    }

    IEnumerator LoadGameOverDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game Over");
    }
}
