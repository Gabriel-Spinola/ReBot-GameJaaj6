using System.Collections;
using UnityEngine;
using Pathfinding;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float health = 1f;

    protected Rigidbody2D rb = null ;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void JumpedOn()
    {
        Destroy(rb);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage) => health -= damage;
}