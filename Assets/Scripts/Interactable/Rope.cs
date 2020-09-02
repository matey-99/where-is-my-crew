using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Interactable
{
    private BossAI owner = default;
    private Transform placeForRope = default;
    private Ship ship = default;

    public override void Interact(PlayerController player)
    {
        base.Interact(player);

        if (!player.HeldObject)
        {
            Cut();
            player.CutRope();
        }
    }

    public void Init(BossAI owner, Transform placeForRope)
    {
        this.owner = owner;
        this.placeForRope = placeForRope;

        ship = Ship.Instance;
    }

    private void Cut()
    {
        owner.RollUpRope(this);
        ship.FreeUpPlaceForRope(owner.Side, placeForRope);

        Destroy(gameObject);
    }
}

