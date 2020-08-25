using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Interactable
{
    [SerializeField] private GameObject progressBarPrefab = default;
    [SerializeField] private Side side = default;

    private bool isCannonball = false;

    public override void Interact(PlayerInteraction player)
    {
        base.Interact(player);

        if (isCannonball && player.Held is Torch)
        {
            StartCoroutine(Shoot());
        }
        else if (!isCannonball && player.Held is Cannonball)
        {
            LoadAmmo();
            player.Drop();
        }
    }

    private void LoadAmmo()
    {
        isCannonball = true;
    }

    private IEnumerator Shoot()
    {
        isCannonball = false;
        yield return new WaitForSeconds(4);
        //Play animation, throw ball and delete one life of enemy ship

        EnemyManager.Instance.DealDamage(side);

        Debug.Log("shoot!");
    }
}

public enum Side 
{
    left, 
    right 
}