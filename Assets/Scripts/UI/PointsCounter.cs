using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour
{
    public static PointsCounter Instance { get; private set; }

    [SerializeField] private int points = 0;

    private Text pointsText = default;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        pointsText = GetComponent<Text>();
    }

    public void AddPoint()
    {
        points++;
        pointsText.text = points.ToString();
    }
}
