using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 500f;

    private void Update()
    {
        transform.Rotate(transform.forward * rotationSpeed * Time.deltaTime);
    }
}
