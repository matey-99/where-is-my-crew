using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToHold : Interactable
{
    [SerializeField] private PlayerController.HandsInUsing neededHands = default;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private Quaternion inHandRotation = Quaternion.identity;

    private Rigidbody rb = default;


    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public override void Interact(PlayerController player)
    {
        base.Interact(player);

        PickUp(player, player.RightHand);
    }

    public virtual void PickUp(PlayerController player, Transform playerHand)
    {
        if (!player.HeldObject)
        {
            transform.parent = playerHand.transform;
            transform.localPosition = offset;
            transform.localRotation = inHandRotation;

            rb.isKinematic = true;

            gameObject.layer = LayerMask.NameToLayer("Held");

            player.PickUp(this, neededHands);
        }
    }

    public void Drop()
    {
        transform.parent = null;
        rb.isKinematic = false;

        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}
    
