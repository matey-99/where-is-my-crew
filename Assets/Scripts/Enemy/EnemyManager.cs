using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [System.Serializable]
    private struct EnemyRespawnPoint
    {
        public Vector3 positionInWorld;
        public Vector2 positionOnRadar;
    }

    [Header("Init")]
    [SerializeField] private GameObject enemyPrefab = default;
    [SerializeField] private GameObject enemyOnRadarPrefab = default;

    [SerializeField] private Transform enemyRespawnPointsTransform = default;
    [SerializeField] private Transform radarTransform = default;
    [SerializeField] private List<EnemyRespawnPoint> enemyRespawnPoints = new List<EnemyRespawnPoint>();
    [SerializeField] private float minTimeBetweenEnemiesRespawn = 8f;
    [SerializeField] private float maxTimeBetweenEnemiesRespawn = 25f;

    [Header("States")]
    [SerializeField] private List<EnemyAI> enemies = new List<EnemyAI>();
    [SerializeField] private List<EnemyRespawnPoint> freeEnemyRespawnPoints = new List<EnemyRespawnPoint>();
    [SerializeField] private List<EnemyRespawnPoint> takenEnemyRespawnPoints = new List<EnemyRespawnPoint>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        freeEnemyRespawnPoints = enemyRespawnPoints;

        StartCoroutine(RespawnEnemy(Random.Range(minTimeBetweenEnemiesRespawn, maxTimeBetweenEnemiesRespawn)));
    }

    public IEnumerator RespawnEnemy(float time)
    {
        yield return new WaitForSeconds(time);

        if (freeEnemyRespawnPoints.Count > 0)
        {
            int index = Random.Range(0, freeEnemyRespawnPoints.Count);
            EnemyRespawnPoint point = freeEnemyRespawnPoints[index];

            EnemyAI enemy = Instantiate(enemyPrefab, enemyRespawnPointsTransform, false).GetComponent<EnemyAI>();
            enemy.transform.localPosition = point.positionInWorld;

            GameObject enemyOnRadar = Instantiate(enemyOnRadarPrefab, radarTransform, false);
            enemyOnRadar.transform.localPosition = point.positionOnRadar;

            enemies.Add(enemy);
            enemy.Init();

            freeEnemyRespawnPoints.Remove(point);
            takenEnemyRespawnPoints.Add(point);

            if (maxTimeBetweenEnemiesRespawn > minTimeBetweenEnemiesRespawn + 2)
            {
                maxTimeBetweenEnemiesRespawn--;
            }
        }

        StartCoroutine(RespawnEnemy(Random.Range(minTimeBetweenEnemiesRespawn, maxTimeBetweenEnemiesRespawn)));
    }
}
