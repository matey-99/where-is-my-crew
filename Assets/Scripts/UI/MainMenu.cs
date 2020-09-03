using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    [SerializeField] private GameObject howToPlayPanel = default;
    [SerializeField] private GameObject creditsPanel = default;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (howToPlayPanel.activeSelf)
            {
                howToPlayPanel.SetActive(false);
            }
            else if (creditsPanel.activeSelf)
            {
                creditsPanel.SetActive(false);
            }
        }
    }

    public void StartGame()
    {
        AudioSourcePlay();
        SceneManager.LoadScene(1);
        GameManager.Instance.InGameScene(true);
    }

    public void HowToPlay()
    {
        AudioSourcePlay();
        howToPlayPanel.SetActive(true);
    }

    public void Credits()
    {
        AudioSourcePlay();
        creditsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        AudioSourcePlay();
        Application.Quit();
    }
}
