using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Interactable
{
    private Enemy enemy = default;
    private Transform placeForRope = default;

    public override void Interact(PlayerInteraction player)
    {
        base.Interact(player);

        player.CutRope(placeForRope);
        enemy.LoseRope();
        Destroy(gameObject);
    }

    public void Init(Enemy enemy, Transform placeForRope)
    {
        this.enemy = enemy;
        this.placeForRope = placeForRope;
    }
}
