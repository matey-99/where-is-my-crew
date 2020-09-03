using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public static Ship Instance { get; private set; }
    public enum Side { left, right }
    public Character Owner { get => owner; }
    public Collider MeshCollider { get => meshCollider; }
    public List<Transform> FreePlacesForRopeLeft { get => freePlacesForRopeLeft; }
    public List<Transform> FreePlacesForRopeRight { get => freePlacesForRopeRight; }

    [SerializeField] private Character owner = default;
    [SerializeField] private Collider meshCollider = default;

    [SerializeField] private GameObject placesForRopeLeftParent = default;
    [SerializeField] private GameObject placesForRopeRightParent = default;

    [SerializeField] private List<Transform> freePlacesForRopeLeft = new List<Transform>();
    [SerializeField] private List<Transform> takenPlacesForRopeLeft = new List<Transform>();

    [SerializeField] private List<Transform> freePlacesForRopeRight = new List<Transform>();
    [SerializeField] private List<Transform> takenPlacesForRopeRight = new List<Transform>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        freePlacesForRopeLeft = placesForRopeLeftParent.GetComponentsInChildren<Transform>().ToList();
        freePlacesForRopeLeft.RemoveAt(0); // removing parent object from list

        freePlacesForRopeRight = placesForRopeRightParent.GetComponentsInChildren<Transform>().ToList();
        freePlacesForRopeRight.RemoveAt(0); // removing parent object from list
    }

    public Transform TakePlaceForRope(Side side, int index)
    {
        Transform placeForRope = default;

        switch (side)
        {
            case Side.left:
                placeForRope = freePlacesForRopeLeft[index];
                takenPlacesForRopeLeft.Add(freePlacesForRopeLeft[index]);
                freePlacesForRopeLeft.RemoveAt(index);
                break;
            case Side.right:
                placeForRope = freePlacesForRopeRight[index];
                takenPlacesForRopeRight.Add(freePlacesForRopeRight[index]);
                freePlacesForRopeRight.RemoveAt(index);
                break;
        }

        return placeForRope;
    }

    public void FreeUpPlaceForRope(Side side, Transform placeForRope)
    {
        switch (side)
        {
            case Side.left:
                freePlacesForRopeLeft.Add(placeForRope);
                takenPlacesForRopeLeft.Remove(placeForRope);
                break;
            case Side.right:
                freePlacesForRopeRight.Add(placeForRope);
                takenPlacesForRopeRight.Remove(placeForRope);
                break;
        }
    }
}
