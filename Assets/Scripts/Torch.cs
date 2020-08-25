using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Interactable
{
    public override void Interact(PlayerInteraction player)
    {
        base.Interact(player);

        player.PickUp(gameObject);
    }
}
