using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image fill = default;
    private Character character = default;

    public void Init(Character character)
    {
        this.character = character;

        fill = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        fill.fillAmount = character.Health;
    }
}
