using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 3;
    
    private Ship playerShip = default;
    private ProgressBar progress = default;
    private GameObject healthText = default;
    private Text healthAmount = default;
    private GameObject ropePrefab = default;
    private int ropesInUse = 0;

    private void Start()
    {
        StartCoroutine(Shoot(Random.Range(7, 10)));
        StartCoroutine(ThrowRope(Random.Range(6, 20)));
    }

    private void Update()
    {
        healthAmount.text = health.ToString();

        if (health <= 0)
        {
            Death();
        }
    }

    public void Init(GameObject progressBarPrefab, GameObject[] healthPrefabs, GameObject ropePrefab, float navalBoardingTime)
    {
        progress = Instantiate(progressBarPrefab, FindObjectOfType<Canvas>().transform, false).GetComponent<ProgressBar>();
        progress.Init(this, navalBoardingTime);

        healthText = Instantiate(healthPrefabs[0], FindObjectOfType<Canvas>().transform, false);
        healthAmount = Instantiate(healthPrefabs[1], FindObjectOfType<Canvas>().transform, false).GetComponent<Text>();

        this.ropePrefab = ropePrefab;

        playerShip = Ship.Instance;
    }

    public void GetDamage()
    {
        health--;
    }

    public float GetAmountOfRopes()
    {
        return ropesInUse;
    }

    public void LoseRope()
    {
        ropesInUse--;
    }

    private IEnumerator Shoot(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("Shot");
        StartCoroutine(Shoot(Random.Range(7, 11)));
    }

    private IEnumerator ThrowRope(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("threw");

        if (playerShip.PlacesForRope.Count > 0)
        {
            Debug.Log("rope");
            int index = Random.Range(0, playerShip.PlacesForRope.Count - 1);
            Transform placeForRope = playerShip.TakePlaceForRope(index);
            Rope rope = Instantiate(ropePrefab, placeForRope.position, Quaternion.identity, placeForRope).GetComponent<Rope>();
            rope.Init(this, placeForRope);
            ropesInUse++;
        }

        StartCoroutine(ThrowRope(Random.Range(6, 20)));
    }

    private void Death()
    {
        Debug.Log("Death of " + gameObject.name);
        Destroy(healthText);
        Destroy(healthAmount);
        Destroy(progress);
        Destroy(gameObject);
    }
}
