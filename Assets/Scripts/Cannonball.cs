using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : Interactable
{
    public override void Interact(PlayerInteraction player)
    {
        base.Interact(player);

        player.PickUp(gameObject);
    }
}
