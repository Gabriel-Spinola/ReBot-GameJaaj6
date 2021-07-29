using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [HideInInspector] public float radius = 1f;

    public void DestroyGameObject() => Destroy(gameObject);
}