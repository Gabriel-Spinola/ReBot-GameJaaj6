using System.Collections;
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

    private float nextTimeToFire = 0f;
    private int maxIndex = 20;
    private int currentIndex = 0;

    private void Awake()
    {
        laserBeam = GetComponent<LaserBeam>();
    }

    private void Start()
    {
        laserBeam.Init();

        currentIndex = maxIndex;
    }

    private void Update()
    {
        if (currentIndex <= 0) {
            StartCoroutine(Reload(reloadDelay));

            return;
        }

        if (currentIndex >= 1) {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (laserBeam.isDisabled)
            laserBeam.EnableLaser();

        laserBeam.UpdateLaser();

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
