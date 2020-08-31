using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float Health { get => health; }
    public float Damage { get => damage; }

    [SerializeField] [Range(0, 1)] private float health = 1;
    [SerializeField] [Range(0, 1)] private float damage = 0.1f;

    private void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    public void DealDamage(float damage)
    {
        health -= damage;
    }

    public virtual void Death()
    {

    }

    
}
