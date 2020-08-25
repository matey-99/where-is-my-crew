using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Enemy enemy = default;
    private Image mask = default;
    private float time = 0f;
    private float current = 0f;

    public void Init(Enemy enemy, float time)
    {
        this.time = time;
        this.enemy = enemy;

        mask = GetComponentsInChildren<Image>()[1];
    }

    private void Update()
    {
        if (current < time)
        {
            float timeMultiplier = enemy.GetAmountOfRopes();
            current += Time.deltaTime * timeMultiplier;
            FillBar();
        }
    }

    private void FillBar()
    {
        mask.fillAmount = current / time;
    }
}
