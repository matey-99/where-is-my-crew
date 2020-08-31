using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float Health { get => health; }

    [Header("Init")]
    [SerializeField] private GameObject healthBarPrefab = default;
    [SerializeField] private GameObject hud = default;

    [Header("Stats")]
    [SerializeField] [Range(0, 1)] private float health = 1;

    private HealthBar healthBar = default;

    public void Init()
    {
        healthBar = Instantiate(healthBarPrefab, hud.transform, false).GetComponent<HealthBar>();
        healthBar.Init(this);
    }
}
