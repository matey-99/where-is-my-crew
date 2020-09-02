using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Boss boss = default;
    private Image fill = default;
    private int multiplier = 0;
    private float time = 0f;
    private float current = 0f;

    public void Init(Boss boss, float time)
    {
        this.time = time;
        this.boss = boss;

        fill = GetComponentInChildren<Image>();
    }

    public void SetMultiplier(int multiplier)
    {
        this.multiplier = multiplier;
    }

    private void Update()
    {
        if (multiplier == 0)
        {
            current -= Time.deltaTime * 1;
        }
        else
        {
            if (current < time)
            {
                current += Time.deltaTime * multiplier;
            }
            else
            {
                boss.NavalBoardingCompleted();
            }
        }
        current = Mathf.Clamp(current, 0, time);

        FillBar();
    }

    private void FillBar()
    {
        fill.fillAmount = current / time;
    }
}
