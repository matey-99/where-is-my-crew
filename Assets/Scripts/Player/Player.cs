﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Init")]
    [SerializeField] private GameObject healthBarPrefab = default;
    [SerializeField] private GameObject hud = default;

    private HealthBar healthBar = default;

    public void Init()
    {
        healthBar = Instantiate(healthBarPrefab, hud.transform, false).GetComponent<HealthBar>();
        healthBar.Init(this);
    }

    public override void Death()
    {
        base.Death();

        GameManager.Instance.Lose();
    }
}
