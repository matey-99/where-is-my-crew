using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    private AudioSource source = default;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void TryAgain()
    {
        source.Play();
        SceneManager.LoadScene(1);
        GameManager.Instance.ResetOnLoad();
        GameManager.Instance.Resume();
        Destroy(gameObject);
    }

    public void BackToMenu()
    {
        source.Play();
        SceneManager.LoadScene(0);
        GameManager.Instance.ResetOnLoad();
        GameManager.Instance.InGameScene(false);
        Destroy(gameObject);
    }
}
