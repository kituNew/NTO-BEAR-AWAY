using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenyController : MonoBehaviour
{
    [SerializeField] GameObject menyShield;
    bool isMenyOn = false;

    public void OnAndOffMeny()
    {
        if (isMenyOn)
        {
            menyShield.SetActive(false);
            Time.timeScale = 1f;
            isMenyOn = false;
        }
        else
        {
            menyShield.SetActive(true);
            Time.timeScale = 0f;
            isMenyOn = true;
        }
    }

    public void Resume()
    {
        menyShield.SetActive(false);
        Time.timeScale = 1f;
        isMenyOn = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadEducationLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMeny()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
