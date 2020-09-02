using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : EnemyAI
{
    public Ship.Side Side { get => side; }

    [SerializeField] private GameObject ropePrefab = default;

    [SerializeField] private float timeToCompleteNavalBoarding = 10f;
    [SerializeField] private float minTimeBetweenRopeThrow = 5f;
    [SerializeField] private float maxTimeBetweenRopeThrow = 10f;

    [Header("States")]
    [SerializeField] private List<Transform> placesForRope = new List<Transform>();
    [SerializeField] private List<Rope> actualRopes = new List<Rope>();

    private Boss boss = default;
    private Ship.Side side = default;

    private void Update()
    {
        boss.CalculateNavalBoardingProgressBar(actualRopes.Count);
    }

    public void Init(GameObject bossOnRadar, HealthBar healthBar, ProgressBar progressBar, BossRespawnPoint point)
    {
        Init();

        side = point.side;

        switch (side)
        {
            case Ship.Side.left:
                placesForRope = PlayerShip.FreePlacesForRopeLeft;
                break;
            case Ship.Side.right:
                placesForRope = PlayerShip.FreePlacesForRopeRight;
                break;
        }

        boss = GetComponent<Boss>();
        boss.Init(bossOnRadar, healthBar, progressBar, point, timeToCompleteNavalBoarding);

        StartCoroutine(ThrowRope(Random.Range(minTimeBetweenRopeThrow, maxTimeBetweenRopeThrow)));
    }

    public void RollUpRope(Rope rope)
    {
        actualRopes.Remove(rope);
    }

    private IEnumerator ThrowRope(float time)
    {
        yield return new WaitForSeconds(time);

        if (placesForRope.Count > 0)
        {
            Transform placeForRope = PlayerShip.TakePlaceForRope(side, Random.Range(0, placesForRope.Count));

            Rope rope = Instantiate(ropePrefab, placeForRope, false).GetComponent<Rope>();

            if (side == Ship.Side.right)
            {
                rope.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, -90));
            }

            rope.Init(this, placeForRope);
            actualRopes.Add(rope);
        }

        StartCoroutine(ThrowRope(Random.Range(minTimeBetweenRopeThrow, maxTimeBetweenRopeThrow)));
    }
}
