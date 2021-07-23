using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Stats")]
    [SerializeField] protected float health = 1f; 

    public virtual void JumpedOn() { }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage) => health -= damage;
}