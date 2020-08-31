using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballShoot : MonoBehaviour
{
    public Character Owner { get => owner; }
    public float Damage { get => damage; }

    private Character owner = default;
    private float damage = 0.1f;

    public void Init(Character owner)
    {
        this.owner = owner;
        damage = owner.Damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == Ship.Instance.MeshCollider)
        {
            Ship.Instance.Owner.DealDamage(damage);
            Destroy(gameObject);
        }

        
    }
}
