using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : EnemyAI
{
    [SerializeField] private GameObject ropePrefab = default;

    [SerializeField] private float timeToCompleteNavalBoarding = 10f;
    [SerializeField] private float minTimeBetweenRopeThrow = 5f;
    [SerializeField] private float maxTimeBetweenRopeThrow = 10f;

    private Boss boss = default;

    public void Init(GameObject bossOnRadar, HealthBar healthBar, ProgressBar progressBar, BossRespawnPoint point)
    {
        Init();

        boss = GetComponent<Boss>();
        boss.Init(bossOnRadar, healthBar, progressBar, point, timeToCompleteNavalBoarding);

        StartCoroutine(ThrowRope(Random.Range(minTimeBetweenRopeThrow, maxTimeBetweenRopeThrow)));
    }

    private IEnumerator ThrowRope(float time)
    {
        yield return new WaitForSeconds(time);


    }
}
