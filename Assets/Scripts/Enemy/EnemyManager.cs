using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [Header("Init")]
    [SerializeField] private GameObject enemyPrefab = default;
    [SerializeField] private GameObject navalBoardingProgressBarPrefab = default;
    [SerializeField] private GameObject[] healthUIPrefab = default;
    [SerializeField] private GameObject ropePrefab = default;

    [Header("States")]
    [SerializeField] private int enemies = 1;
    [SerializeField] private Enemy enemyOnLeftSide = default;
    [SerializeField] private Enemy enemyOnRightSide = default;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    private void Update()
    {
        if (enemies > 0)
            RespawnEnemy();
    }

    public void RespawnEnemy()
    {
        if (!enemyOnLeftSide)
        {
            enemyOnLeftSide = Instantiate(enemyPrefab, transform, false).GetComponent<Enemy>();
            enemyOnLeftSide.Init(navalBoardingProgressBarPrefab, healthUIPrefab, ropePrefab, 60);
            enemies--;
            return;
        }

        if (!enemyOnRightSide)
        {
            enemyOnRightSide = Instantiate(enemyPrefab, transform, false).GetComponent<Enemy>();
            enemies--;
        }
    }
}
