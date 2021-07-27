﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LaserBeam))]
public class PulseShooter : MonoBehaviour
{
    [Tooltip("In Rate")]
    [SerializeField] private float timeToOverheat;
    [Tooltip("In Seconds")]
    [SerializeField] private float reloadDelay;

    private LaserBeam laserBeam = null;

    private const int maxIndex = 20;
    private float nextTimeToFire = 0f;
    private int currentIndex = 0;

    protected virtual void Awake()
    {
        laserBeam = GetComponent<LaserBeam>();
    }

    protected virtual void Start()
    {
        laserBeam.Init();

        currentIndex = maxIndex;
    }

    protected virtual void Update()
    {
        if (currentIndex <= 0) {
            StartCoroutine(Reload(reloadDelay));
            laserBeam.UpdateLaser(laserBeam.preciptationLineRenderer);

            return;
        }

        if (currentIndex >= 1) {
            Shoot();
        }

        if (laserBeam.hit.collider.CompareTag("Player")) {
            laserBeam.hit.collider.gameObject.GetComponent<Player>().TakeDamage();
        }
    }

    private void Shoot()
    {
        if (laserBeam.isDisabled)
            laserBeam.EnableLaser();

        laserBeam.UpdateLaser(laserBeam.lineRenderer);

        if (Time.time >= nextTimeToFire) {
            nextTimeToFire = Time.time + 1f / timeToOverheat;

            currentIndex--;
        }
    }

    private IEnumerator Reload(float time)
    {
        if (!laserBeam.isDisabled)
            laserBeam.DisableLaser();

        yield return new WaitForSeconds(time);

        currentIndex = maxIndex;
    }
}
