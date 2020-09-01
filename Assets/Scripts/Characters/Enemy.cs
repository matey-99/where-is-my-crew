using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private GameObject enemyOnRadar = default;

    public void Init(GameObject enemyOnRadar)
    {
        this.enemyOnRadar = enemyOnRadar;
    }

    public override void Death()
    {
        base.Death();

        DeathOfEnemy();

        Destroy(enemyOnRadar);
        Destroy(gameObject);
    }

    protected virtual void DeathOfEnemy()
    {

    }
}
