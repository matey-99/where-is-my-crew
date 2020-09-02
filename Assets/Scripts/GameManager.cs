using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Text pointsText = default;

    [SerializeField] private int points = 0;
    [SerializeField] private bool isPaused = false;

    private bool pauseInput = false;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        GetInput();

        if (pauseInput)
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

    public void AddPoint()
    {
        points++;
        pointsText.text = points.ToString();
    }

    private void Resume()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    private void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    private void GetInput()
    {
        pauseInput = Input.GetButtonDown("Cancel");
    }
}
