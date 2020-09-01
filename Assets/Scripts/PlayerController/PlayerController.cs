using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player PlayerStats { get => player; }
    public Transform RightHand { get => rightHand; }
    public Interactable HeldObject { get => heldObject; }
    public enum HandsInUsing { none, one, both }

    [Header("Initialization")]
    [SerializeField] private GameObject activeModel = default;
    [SerializeField] private Transform rightHand = default;
    [SerializeField] private LayerMask interactableLayer = default; // player can interact with objects with that layer mask
    [SerializeField] private float distanceToInteract = 3f; // minimal distance from player to object to interact

    [Space]
    [Header("Statistics")]
    [SerializeField] private Interactable interactableObject = default; // the object the player is looking at
    [SerializeField] private ToHold heldObject = default; // the object the player is holding
    [SerializeField] private HandsInUsing hands = HandsInUsing.none;

    private Player player = default;
    private PlayerMovement playerMovement = default;
    private Animator anim = default;
    private float vertical = 0f;
    private float horizontal = 0f;
    private bool interactInput = false;
    private bool dropInput = false;

    private void Awake()
    {
        player = GetComponent<Player>();
        player.Init();

        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.Init(activeModel);

        anim = activeModel.GetComponent<Animator>();
    }

    private void Update()
    {
        GetInput();

        Interacting();
    }

    private void FixedUpdate()
    {
        playerMovement.Move(vertical, horizontal);
    }

    public void PickUp(ToHold pickedUpObject, HandsInUsing neededHands)
    {
        heldObject = pickedUpObject;

        HandleHandsAnimations(neededHands);
    }

    public void LoadCannon()
    {
        Destroy(heldObject.gameObject);

        HandleHandsAnimations(HandsInUsing.none);
    }

    public void FireFuse()
    {
        anim.SetTrigger("Attack");
    }

    private void Interacting()
    {
        Interactable interactable = GetNearest<Interactable>(distanceToInteract, interactableLayer);

        if (interactable)
        {
            if (interactableObject)
            {
                interactableObject.NoInteraction();
                interactableObject = null;
            }

            interactable.ToInteraction();
            interactableObject = interactable;

            if (interactInput)
            {
                interactableObject.Interact(this);
            }
        }
        else
        {
            if (interactableObject)
            {
                interactableObject.NoInteraction();
                interactableObject = null;
            }
        }

        if (heldObject && dropInput)
        {
            Drop();
        }
    }

    private void Drop()
    {
        heldObject.Drop();
        heldObject = null;

        HandleHandsAnimations(HandsInUsing.none);
    }

    private void GetInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        interactInput = Input.GetButtonDown("Interact");
        dropInput = Input.GetButtonDown("Drop");
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
