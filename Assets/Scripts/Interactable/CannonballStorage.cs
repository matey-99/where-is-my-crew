using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballStorage : Interactable
{
    [SerializeField] private GameObject cannonballPrefab = default;


    public override void Interact(PlayerController player)
    {
        base.Interact(player);

        if (!player.HeldObject)
        {
            CreateCannonball(player);
        }
    }

    private void CreateCannonball(PlayerController player)
    {
        ToHold cannonball = Instantiate(cannonballPrefab, player.transform, false).GetComponent<Cannonball>();

        cannonball.PickUp(player, player.RightHand);
    }
}
