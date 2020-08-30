using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToHold : Interactable
{
    [SerializeField] private PlayerInteraction.HandsInUsing neededHands = default;
    [SerializeField] private Vector3 offset = Vector3.zero;


    private Rigidbody rb = default;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public override void Interact(PlayerInteraction player)
    {
        if (CanInteract)
        {
            base.Interact(player);

            PickUp(player);
            player.PickUp(this, neededHands);
        }
    }

    public void PickUp(PlayerInteraction player)
    {
        transform.parent = player.transform;
        transform.localPosition = offset;

        rb.isKinematic = true;
        CanInteract = false;
    }

    public void Drop()
    {
        rb.isKinematic = false;
        CanInteract = true;
    }
}
    
