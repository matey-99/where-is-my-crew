using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private AudioMixer audioMixer = default;

    [SerializeField] private GameObject losePanelPrefab = default;
    [SerializeField] private GameObject menuInPausePrefab = default;
    [SerializeField] private bool isPaused = false;

    private GameObject menuInPause = default;
    private bool isLost = default;
    private bool isInGameScene = false;
    private bool isPauseInputClicked = false;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        GetInput();

        if (isInGameScene && !isLost)
        {
            if (isPauseInputClicked)
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Lose()
    {
        Instantiate(losePanelPrefab, FindObjectOfType<Canvas>().transform, false);
        Time.timeScale = 0;
        isLost = true;
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void SetSoundsVolume(float volume)
    {
        audioMixer.SetFloat("soundsVolume", volume);
    }

    public float GetMasterVolume()
    {
        float volume;
        audioMixer.GetFloat("masterVolume", out volume);

        return volume;
    }

    public float GetMusicVolume()
    {
        float volume;
        audioMixer.GetFloat("musicVolume", out volume);

        return volume;
    }

    public float GetSoundsVolume()
    {
        float volume;
        audioMixer.GetFloat("soundsVolume", out volume);

        return volume;
    }

    public void InGameScene(bool status)
    {
        isInGameScene = status;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        isPaused = false;

        if (menuInPause)
        {
            Destroy(menuInPause);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;

        menuInPause = Instantiate(menuInPausePrefab, FindObjectOfType<Canvas>().transform, false);
    }

    public void ResetOnLoad()
    {
        isLost = false;
        isPaused = false;
    }

    private void GetInput()
    {
        isPauseInputClicked = Input.GetButtonDown("Cancel");
    }
}
