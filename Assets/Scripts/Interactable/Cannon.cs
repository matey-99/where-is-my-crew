using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Interactable
{
    private bool isLoaded = false;

    public override void Interact(PlayerInteraction player)
    {
        base.Interact(player);

        if (isLoaded && player.HeldObject is Torch)
        {
            StartCoroutine(Shoot());
        }
        else if (!isLoaded && player.HeldObject is Cannonball)
        {
            LoadAmmo();
            player.DestroyHeldObject();
        }
    }

    private void LoadAmmo()
    {
        isLoaded = true;
    }

    private IEnumerator Shoot()
    {
        isLoaded = false;
        yield return new WaitForSeconds(4);
        //Play animation, throw ball and delete one life of enemy ship

        Debug.Log("shoot!");
    }
}