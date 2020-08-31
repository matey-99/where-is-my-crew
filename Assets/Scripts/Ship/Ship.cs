using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public static Ship Instance { get; private set; }
    public Character Owner { get => owner; }
    public Collider MeshCollider { get => meshCollider; }
    public List<Transform> PlacesForRope { get => placesForRope; }

    [SerializeField] private Character owner = default;
    [SerializeField] private Collider meshCollider = default;
    [SerializeField] private List<Transform> placesForRope = new List<Transform>();
    [SerializeField] private List<Transform> takenPlacesForRope = new List<Transform>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public Transform TakePlaceForRope(int index)
    {
        Transform placeForRope = placesForRope[index];
        takenPlacesForRope.Add(placesForRope[index]);
        placesForRope.RemoveAt(index);

        return placeForRope;
    }

    public void FreeUpPlaceForRope(Transform placeForRope)
    {
        placesForRope.Add(placeForRope);
        takenPlacesForRope.Remove(placeForRope); 
    }
}
