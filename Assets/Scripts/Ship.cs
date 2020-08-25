using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public static Ship Instance { get; private set; }
    public List<Transform> PlacesForRope { get => placesForRope; }

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
