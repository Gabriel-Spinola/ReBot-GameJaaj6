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
    [SerializeField] private bool shootWhenCollidedWithlayer;

    private LaserBeam laserBeam = null;

    private const int maxIndex = 20;
    private float nextTimeToFire = 0f;
    private int currentIndex = 0;

    private bool startShooting = false;

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
            laserBeam.StopParticle(1);

            return;
        }

        if (laserBeam.hit && shootWhenCollidedWithlayer) {
            if (laserBeam.hit.collider.CompareTag("Player")) {
                startShooting = true;
            }

            if (currentIndex >= 1 && startShooting) {
                StartCoroutine(Shoot(true));
            }
            else {
                laserBeam.DisableLaser();

                laserBeam.UpdateLaser(laserBeam.preciptationLineRenderer);
                laserBeam.StopParticle(1);
            }
        }
        else {
            if (currentIndex >= 1) {
                Shoot();
            }
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

    private IEnumerator Shoot(bool shootWhenCollidedWithlayer = false)
    {
        yield return new WaitForSeconds(.5f);

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

        startShooting = false;

        yield return new WaitForSeconds(time);

        currentIndex = maxIndex;
    }

    protected virtual void ResetValues()
    {
        nextTimeToFire = 0;
        currentIndex = 0;
        startShooting = false;
        laserBeam.DisableLaser();
    }
}
