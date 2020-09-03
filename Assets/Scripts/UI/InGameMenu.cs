using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : Menu
{
    public void Resume()
    {
        AudioSourcePlay();
        GameManager.Instance.Resume();
    }

    public void BackToMainMenu()
    {
        AudioSourcePlay();
        GameManager.Instance.Resume();
        GameManager.Instance.ResetOnLoad();
        GameManager.Instance.InGameScene(false);
        SceneManager.LoadScene(0);
    }

    public void SetMasterVolume(float volume)
    {
        GameManager.Instance.SetMasterVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        GameManager.Instance.SetMusicVolume(volume);
    }

    public void SetSoundsVolume(float volume)
    {
        GameManager.Instance.SetSoundsVolume(volume);
    }
}
