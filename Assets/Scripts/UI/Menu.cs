using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel = default;
    [SerializeField] private GameObject optionsPanel = default;

    [SerializeField] private Slider masterVolumeSlider = default;
    [SerializeField] private Slider musicVolumeSlider = default;
    [SerializeField] private Slider soundsVolumeSlider = default;

    private AudioSource source = default;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        masterVolumeSlider.value = GameManager.Instance.GetMasterVolume();
        musicVolumeSlider.value = GameManager.Instance.GetMusicVolume();
        soundsVolumeSlider.value = GameManager.Instance.GetSoundsVolume();
    }

    public void OpenOptions()
    {
        AudioSourcePlay();
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        AudioSourcePlay();
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    protected void AudioSourcePlay()
    {
        source.Play();
    }
}
