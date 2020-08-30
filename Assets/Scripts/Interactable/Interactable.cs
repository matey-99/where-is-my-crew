using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    public bool CanInteract { get => canInteract; protected set => canInteract = value; }

    [SerializeField] private GameObject interactNotificationPrefab = default;
    [SerializeField] private bool canInteract = true;

    private Camera cam = default;
    private Canvas canvas = default;
    private Image interactNotification = default;

    private void Start()
    {
        cam = Camera.main;
        canvas = FindObjectOfType<Canvas>();
    }

    public virtual void Interact(PlayerInteraction player)
    {

    }

    // when player is close enough to interact with this object
    public void AtThePlayerLook()
    {
        ShowNotification();
    }

    public void WhenPlayerIsNotLooking()
    {
        HideNotification();
    }

    private void ShowNotification()
    {
        if (!interactNotification)
        {
            interactNotification = Instantiate(interactNotificationPrefab, canvas.transform, false).GetComponent<Image>();
        }

        interactNotification.transform.position = cam.WorldToScreenPoint(transform.position);

    }

    private void HideNotification()
    {
        Destroy(interactNotification.gameObject);
    }
}
