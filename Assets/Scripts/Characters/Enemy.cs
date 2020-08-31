using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private GameObject enemyOnRadar = default;
    private EnemyRespawnPoint respawnPoint = default;

    public void Init(GameObject enemyOnRadar, EnemyRespawnPoint respawnPoint)
    {
        this.enemyOnRadar = enemyOnRadar;
        this.respawnPoint = respawnPoint;
    }

    public override void Death()
    {
        base.Death();

        EnemyManager.Instance.DeathOfEnemy(this, respawnPoint);
        Destroy(enemyOnRadar);
        Destroy(gameObject);
    }
}
