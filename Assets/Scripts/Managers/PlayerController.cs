using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerStateManager = default;
    private PlayerInteraction playerInteraction = default;
    private float vertical = 0f;
    private float horizontal = 0f;
    private bool interactInput = false;
    private bool dropInput = false;

    private void Awake()
    {
        playerStateManager = GetComponent<PlayerMovement>();
        playerStateManager.Init();

        playerInteraction = GetComponent<PlayerInteraction>();
        playerInteraction.Init();
    }

    private void Update()
    {
        GetInput();

        if (interactInput)
        {
            playerInteraction.Interact();
        }
        if (dropInput)
        {
            playerInteraction.Drop();
        }
    }

    private void FixedUpdate()
    {
        playerStateManager.Move(vertical, horizontal);
    }

    private void GetInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        interactInput = Input.GetButtonDown("Interact");
        dropInput = Input.GetButtonDown("Drop");
    }

}
