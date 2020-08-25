using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Interactable Held { get; private set; }

    [SerializeField] private LayerMask interactableLayer = default;
    [SerializeField] private float interactableRadarRadius = 3f;

    private GameObject heldObject = default;
    private Ship ship = default;

    public void Init()
    {
        ship = Ship.Instance;
    }

    public void Interact()
    {
        Interactable interactable = GetNearest<Interactable>(interactableRadarRadius, interactableLayer);

        if (!interactable)
            return;

        interactable.Interact(this);
    }

    public void Drop()
    {
        if (Held)
        {
            Destroy(heldObject);
        }
    }

    public void PickUp(GameObject pickedUpObject)
    {
        if (Held)
            return;

        heldObject = Instantiate(pickedUpObject, transform, false);
        heldObject.transform.position = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);

        Held = heldObject.GetComponent<Interactable>();

        Destroy(pickedUpObject);
    }

    public void CutRope(Transform placeWithRope)
    {
        ship.FreeUpPlaceForRope(placeWithRope);
    }

    public void Fire()
    {

    }

    public void Load() { }

    

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

}
