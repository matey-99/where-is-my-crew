using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Interactable HeldObject { get => heldObject; }
    public enum HandsInUsing { none, one, both }

    [Header("Initialization")]
    [SerializeField] private LayerMask interactableLayer = default; // player can interact with objects with that layer mask
    [SerializeField] private float distanceToInteract = 3f; // minimal distance from player to object to interact

    [Space]
    [Header("Statistics")]
    [SerializeField] private Interactable interactableObject = default; // the object the player is looking at
    [SerializeField] private ToHold heldObject = default; // the object the player is holding
    [SerializeField] private HandsInUsing hands = HandsInUsing.none;
    [SerializeField] private bool canInteract = true;

    private Animator anim = default;
    private Ship ship = default;

    public void Init(GameObject activeModel)
    {
        anim = activeModel.GetComponent<Animator>();

        ship = Ship.Instance;
    }

    public void LookForInteractableObjects()
    {
        Interactable interactable = GetNearest<Interactable>(distanceToInteract, interactableLayer);

        if (interactable)
        {
            if (interactable.CanInteract)
            {
                if (interactableObject && interactable != interactableObject)
                {
                    interactableObject.WhenPlayerIsNotLooking();
                }

                interactableObject = interactable;
                interactableObject.AtThePlayerLook();
            }
            else
            {
                if (interactableObject && interactable != interactableObject)
                {
                    interactableObject.WhenPlayerIsNotLooking();
                }
            }
            
        }
        else if (interactableObject)
        {
            interactableObject.WhenPlayerIsNotLooking();
            interactableObject = null;
        }
    }

    public void Interact()
    {
        if (interactableObject)
        {
            interactableObject.Interact(this);
        }
    }

    public void PickUp(ToHold pickedUpObject, HandsInUsing neededHands)
    {
        heldObject = pickedUpObject;

        HandleHandsAnimations(neededHands);
    }

    public void Drop()
    {
        heldObject.gameObject.transform.parent = null;
        heldObject.Drop();
        heldObject = null;

        HandleHandsAnimations(HandsInUsing.none);
    }

    public void DestroyHeldObject()
    {
        Destroy(heldObject.gameObject);

        HandleHandsAnimations(HandsInUsing.none);
    }

    public void CutRope(Transform placeWithRope)
    {
        ship.FreeUpPlaceForRope(placeWithRope);
    }   

    private T GetNearest<T>(float radius, LayerMask searchedLayer)
    {
        T nearestObject = default;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, searchedLayer);
        
        float minDistance = Mathf.Infinity;

        foreach (Collider hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestObject = hitCollider.GetComponent<T>();
            }
        }

        return nearestObject;
    }

    private bool CanInteract()
    {
        if (heldObject)
        {
            canInteract = false;
            return false;
        }

        canInteract = true;
        return true;
    }

    private void HandleHandsAnimations(HandsInUsing hands)
    {
        switch (this.hands)
        {
            case HandsInUsing.one:
                anim.SetBool("IsHoldingInOneHand", false);
                break;

            case HandsInUsing.both:
                anim.SetBool("IsHoldingInBothHands", false);
                break;
        }

        switch (hands)
        {
            case HandsInUsing.one:
                anim.SetBool("IsHoldingInOneHand", true);
                break;

            case HandsInUsing.both:
                anim.SetBool("IsHoldingInBothHands", true);
                break;
        }

        this.hands = hands;
    }
}
