using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [Header("Init")]
    [SerializeField] private GameObject enemyPrefab = default;
    [SerializeField] private GameObject bossPrefab = default;
    [SerializeField] private GameObject healthBarPrefab = default;
    [SerializeField] private GameObject progressBarPrefab = default;
    [SerializeField] private GameObject enemyOnRadarPrefab = default;

    [SerializeField] private Transform enemyRespawnPointsTransform = default;
    [SerializeField] private Transform hudTransform = default;
    [SerializeField] private Transform radarTransform = default;
    [SerializeField] private List<EnemyRespawnPoint> enemyRespawnPoints = new List<EnemyRespawnPoint>();
    [SerializeField] private List<BossRespawnPoint> bossRespawnPoints = new List<BossRespawnPoint>();
    [SerializeField] private float enemiesInWave = 8f;
    [SerializeField] private float minTimeBetweenEnemiesRespawn = 8f;
    [SerializeField] private float maxTimeBetweenEnemiesRespawn = 25f;

    [Header("States")]
    [SerializeField] private List<EnemyAI> enemies = new List<EnemyAI>();
    [SerializeField] private List<BossAI> bosses = new List<BossAI>();
    [SerializeField] private List<EnemyRespawnPoint> freeEnemyRespawnPoints = new List<EnemyRespawnPoint>();
    [SerializeField] private List<EnemyRespawnPoint> takenEnemyRespawnPoints = new List<EnemyRespawnPoint>();
    [SerializeField] private List<BossRespawnPoint> freeBossRespawnPoints = new List<BossRespawnPoint>();
    [SerializeField] private List<BossRespawnPoint> takenBossRespawnPoints = new List<BossRespawnPoint>();

    private float enemyRespawnPointsCount = 0;
    private float enemiesInThisWave = 0;
    private bool isBossRespawning = false;
    private bool isBossRespawned = false;

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        enemyRespawnPointsCount = enemyRespawnPoints.Count;
        freeEnemyRespawnPoints = enemyRespawnPoints;

        freeBossRespawnPoints = bossRespawnPoints;

        StartCoroutine(RespawnEnemy(Random.Range(minTimeBetweenEnemiesRespawn, maxTimeBetweenEnemiesRespawn)));
    }

    private void Update()
    {
        if (enemiesInThisWave >= enemiesInWave && freeEnemyRespawnPoints.Count == enemyRespawnPointsCount && !isBossRespawning && !isBossRespawned)
        {
            StartCoroutine(RespawnBoss(Random.Range(minTimeBetweenEnemiesRespawn, maxTimeBetweenEnemiesRespawn)));
            isBossRespawning = true;
        }

        if (isBossRespawned && bosses.Count == 0)
        {
            enemiesInThisWave = 0;
            isBossRespawned = false;
        }

        ClearLists();
    }

    public void DeathOfEnemy(EnemyRespawnPoint respawnPoint)
    {
        takenEnemyRespawnPoints.Remove(respawnPoint);
        freeEnemyRespawnPoints.Add(respawnPoint);
    }

    public void DeathOfBoss(BossRespawnPoint respawnPoint)
    {
        takenBossRespawnPoints.Remove(respawnPoint);
        freeBossRespawnPoints.Add(respawnPoint);
    }

    // this method deleting "Missing" objects of list
    public void ClearLists()
    {
        enemies = enemies.Where(enemy => enemy != null).ToList();
        bosses = bosses.Where(boss => boss != null).ToList();
    }

    private IEnumerator RespawnEnemy(float time)
    {
        yield return new WaitForSeconds(time);

        if (freeEnemyRespawnPoints.Count > 0 && enemiesInThisWave < enemiesInWave)
        {
            int index = Random.Range(0, freeEnemyRespawnPoints.Count);
            EnemyRespawnPoint point = freeEnemyRespawnPoints[index];

            NormalEnemyAI normalEnemyAI = Instantiate(enemyPrefab, enemyRespawnPointsTransform, false).GetComponent<NormalEnemyAI>();
            normalEnemyAI.transform.localPosition = point.positionInWorld;

            GameObject enemyOnRadar = Instantiate(enemyOnRadarPrefab, radarTransform, false);
            enemyOnRadar.transform.localPosition = point.positionOnRadar;

            enemies.Add(normalEnemyAI);
            normalEnemyAI.Init(enemyOnRadar, point);

            freeEnemyRespawnPoints.Remove(point);
            takenEnemyRespawnPoints.Add(point);

            if (maxTimeBetweenEnemiesRespawn > minTimeBetweenEnemiesRespawn + 2)
            {
                maxTimeBetweenEnemiesRespawn--;
            }

            enemiesInThisWave++;
        }

        StartCoroutine(RespawnEnemy(Random.Range(minTimeBetweenEnemiesRespawn, maxTimeBetweenEnemiesRespawn)));
    }

    private IEnumerator RespawnBoss(float time)
    {
        yield return new WaitForSeconds(time);

        int index = Random.Range(0, freeBossRespawnPoints.Count);
        BossRespawnPoint point = freeBossRespawnPoints[index];

        BossAI bossAI = Instantiate(bossPrefab, enemyRespawnPointsTransform, false).GetComponent<BossAI>();
        bossAI.transform.localPosition = point.positionInWorld;

        GameObject bossOnRadar = Instantiate(enemyOnRadarPrefab, radarTransform, false);
        bossOnRadar.transform.localPosition = point.positionOnRadar;

        HealthBar bossHealthBar = Instantiate(healthBarPrefab, hudTransform, false).GetComponent<HealthBar>();
        bossHealthBar.transform.localPosition = point.positionOnHUD;

        ProgressBar bossProgressBar = Instantiate(progressBarPrefab, hudTransform, false).GetComponent<ProgressBar>();
        bossProgressBar.transform.localPosition = point.positionOnHUD - new Vector2(0, 45);

        bosses.Add(bossAI);
        bossAI.Init(bossOnRadar, bossHealthBar, bossProgressBar, point);

        freeBossRespawnPoints.Remove(point);
        takenBossRespawnPoints.Add(point);

        isBossRespawning = false;
        isBossRespawned = true;
    }
}


[System.Serializable]
public struct EnemyRespawnPoint
{
    public Vector3 positionInWorld;
    public Vector2 positionOnRadar;
}

[System.Serializable]
public struct BossRespawnPoint
{
    public Ship.Side side;
    public Vector3 positionInWorld;
    public Vector2 positionOnRadar;
    public Vector2 positionOnHUD; // position of health and progress bar
}