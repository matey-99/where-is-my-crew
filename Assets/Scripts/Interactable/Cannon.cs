using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Interactable
{
    [SerializeField] private GameObject cannonballPrefab = default;
    [SerializeField] private Transform cannonBarrel = default;
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>(); // first must be fire fusing sound, second cannon shooting
    private AudioSource source = default;

    private bool isLoaded = false;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public override void Interact(PlayerController player)
    {
        base.Interact(player);

        if (isLoaded && player.HeldObject is Torch)
        {
            player.FireFuse();
            source.clip = audioClips[0];
            source.Play();

            StartCoroutine(Shoot(player.PlayerStats));
        }
        else if (!isLoaded && player.HeldObject is Cannonball)
        {
            LoadAmmo();

            player.LoadCannon();
        }
    }

    private void LoadAmmo()
    {
        isLoaded = true;
    }

    private IEnumerator Shoot(Player player)
    {
        isLoaded = false;
        yield return new WaitForSeconds(4);

        source.clip = audioClips[1];
        source.Play();

        GameObject cannonball = Instantiate(cannonballPrefab, cannonBarrel.position, Quaternion.identity);
        cannonball.GetComponent<CannonballShoot>().Init(player);

        cannonball.GetComponent<Rigidbody>().AddForce(transform.right * 50, ForceMode.Impulse);
        StartCoroutine(CannonballDisappearance(cannonball));
    }

    private IEnumerator CannonballDisappearance(GameObject cannonball)
    {
        yield return new WaitForSeconds(10);

        if (cannonball)
        {
            Destroy(cannonball);
        }
    }
}