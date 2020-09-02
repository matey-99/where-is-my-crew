using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private HealthBar healthBar = default;
    private ProgressBar progressBar = default;

    private BossRespawnPoint respawnPoint = default;

    public void Init(GameObject bossOnRadar, HealthBar healthBar, ProgressBar progressBar, BossRespawnPoint respawnPoint, float timeToCompleteNavalBoarding)
    {
        Init(bossOnRadar);

        this.healthBar = healthBar;
        this.healthBar.Init(this);

        this.progressBar = progressBar;
        this.progressBar.Init(this, timeToCompleteNavalBoarding);

        this.respawnPoint = respawnPoint;
    }

    public void CalculateNavalBoardingProgressBar(int amountOfRopes)
    {
        progressBar.SetMultiplier(amountOfRopes);
    }

    public void NavalBoardingCompleted()
    {

    }

    protected override void DeathOfEnemy()
    {
        base.DeathOfEnemy();

        EnemyManager.Instance.DeathOfBoss(respawnPoint);

        Destroy(healthBar.gameObject);
        Destroy(progressBar.gameObject);
    }
}
