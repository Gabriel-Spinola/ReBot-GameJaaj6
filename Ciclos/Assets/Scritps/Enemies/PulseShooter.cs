using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseShooter : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform firePoint;

    [SerializeField] private GameObject startVFX;
    [SerializeField] private GameObject endVFX;

    [SerializeField] private Vector2 laserDirection;
    [SerializeField] private float laserMaxDistance;

    [SerializeField] private float timeToShoot;

    private List<ParticleSystem> particles = new List<ParticleSystem>();

    private void Start()
    {
        FillList();
        DisableLaser();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            EnableLaser(); 

        UpdateLaser();
    }

    private void EnableLaser()
    {
        particles.ForEach((ParticleSystem ps) => ps.Play());

        lineRenderer.enabled = true;  
    }

    private void UpdateLaser()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, laserDirection);

        startVFX.transform.position = firePoint.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, laserDirection, laserMaxDistance);

        if (hit) {
            lineRenderer.SetPosition(1, hit.point);
        }

        endVFX.transform.position = lineRenderer.GetPosition(1);
    }

    private void DisableLaser()
    {
        lineRenderer.enabled = false;

        particles.ForEach((ParticleSystem ps) => ps.Stop());
    }

    private void FillList()
    {
        for (int i = 0; i < startVFX.transform.childCount; i++) {
            ParticleSystem ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();

            if (ps != null)
                particles.Add(ps);
        }
        
        for (int i = 0; i < endVFX.transform.childCount; i++) {
            ParticleSystem ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();

            if (ps != null)
                particles.Add(ps);
        }
    }
}
