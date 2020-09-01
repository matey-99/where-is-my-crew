using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Boss boss = default;
    private Image fill = default;
    private float time = 0f;
    private float current = 0f;

    public void Init(Boss boss, float time)
    {
        this.time = time;
        this.boss = boss;

        fill = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        if (current < time)
        {
            current += Time.deltaTime * 0;
            FillBar();
        }
    }

    private void FillBar()
    {
        fill.fillAmount = current / time;
    }
}
