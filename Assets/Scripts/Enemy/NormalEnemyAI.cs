using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyAI : EnemyAI
{
    private NormalEnemy normalEnemy = default;

    public void Init(GameObject enemyOnRadar, EnemyRespawnPoint point)
    {
        Init();

        normalEnemy = GetComponent<NormalEnemy>();
        normalEnemy.Init(enemyOnRadar, point);
    }
}
