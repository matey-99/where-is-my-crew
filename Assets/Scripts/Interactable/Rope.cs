using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Interactable
{
    private EnemyAI enemy = default;
    private Transform placeForRope = default;

    public override void Interact(PlayerController player)
    {
        base.Interact(player);

        Destroy(gameObject);
    }

    public void Init(EnemyAI enemy, Transform placeForRope)
    {
        this.enemy = enemy;
        this.placeForRope = placeForRope;
    }
}

