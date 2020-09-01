using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    private EnemyRespawnPoint respawnPoint = default;

    public void Init(GameObject enemyOnRadar, EnemyRespawnPoint respawnPoint)
    {
        Init(enemyOnRadar);

        this.respawnPoint = respawnPoint;
    }

    protected override void DeathOfEnemy()
    {
        base.DeathOfEnemy();

        EnemyManager.Instance.DeathOfEnemy(this, respawnPoint);
    }
}
