using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballStorage : Interactable
{
    [SerializeField] private GameObject cannonballPrefab = default;


    public override void Interact(PlayerInteraction player)
    {
        base.Interact(player);

        CreateCannonball(player);
    }

    private void CreateCannonball(PlayerInteraction player)
    {
        ToHold cannonball = Instantiate(cannonballPrefab, player.transform, false).GetComponent<Cannonball>();

        cannonball.PickUp(player);
        player.PickUp(cannonball, PlayerInteraction.HandsInUsing.both);
    }
}
