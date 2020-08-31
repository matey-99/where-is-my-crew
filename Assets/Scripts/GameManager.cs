using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Balance")]
    [SerializeField] [Range(0, 1)] private float playerDamage = 0.2f;
    [SerializeField] [Range(0, 1)] private float enemyDamage = 0.1f;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
}
