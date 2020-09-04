using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    protected Ship PlayerShip { get => playerShip; }

    [SerializeField] private GameObject cannonballPrefab = default;
    [SerializeField] private float minTimeBetweenCannonShooting = 6f;
    [SerializeField] private float maxTimeBetweenCannonShooting = 10f;

    private Enemy enemy = default;
    private AudioSource source = default;
    private Ship playerShip = default;

    protected void Init()
    {
        enemy = GetComponent<Enemy>();
        source = GetComponent<AudioSource>();
        playerShip = Ship.Instance;

        StartCoroutine(CannonShoot(Random.Range(minTimeBetweenCannonShooting, maxTimeBetweenCannonShooting)));
    }

    private IEnumerator CannonShoot(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject cannonball = Instantiate(cannonballPrefab, transform.position, Quaternion.identity, null);
        Vector3 cannonballDestination = (playerShip.transform.position - transform.position).normalized;
        cannonball.GetComponent<Rigidbody>().AddForce(cannonballDestination * 50, ForceMode.Impulse);
        cannonball.GetComponent<CannonballShoot>().Init(enemy);
        source.Play();

        StartCoroutine(CannonShoot(Random.Range(minTimeBetweenCannonShooting, maxTimeBetweenCannonShooting)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CannonballShoot cannonball) && cannonball.Owner != enemy)
        {
            enemy.DealDamage(cannonball.Damage);
            Destroy(other.gameObject);

        }
    }
}
