using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : Enemy
{
    [Range(0.1f, 5f)]
    [SerializeField] private float explosionRadius;

    [SerializeField] private Vector2 patrolPosA;
    [SerializeField] private Vector2 patrolPosB;

    private void Update()
    {

    }
}
