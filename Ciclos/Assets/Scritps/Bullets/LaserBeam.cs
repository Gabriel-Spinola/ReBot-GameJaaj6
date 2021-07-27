using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [Header("Cofig")]
    [SerializeField] private bool isDirectionStatic = true;

    [Header("Line Renderers")]
    public LineRenderer lineRenderer = null;
    public LineRenderer preciptationLineRenderer = null;

    [Header("Line Position Config")]
    [SerializeField] private Transform firePoint = null;

    [Header("VFXs")]
    [SerializeField] private GameObject startVFX = null;
    [SerializeField] private GameObject endVFX = null;
    [SerializeField] private GameObject preciptationVFX = null;

    [Header("Direction & Distance")]
    [SerializeField] private Vector2 laserDirection = Vector2.zero;
    [Range(5f, 80f)]
    [SerializeField] private float laserMaxDistance = 30f;

    [HideInInspector] public RaycastHit2D hit;
    [HideInInspector] public bool isDisabled = false;

    private Transform direction = null;

    private List<ParticleSystem> particles = new List<ParticleSystem>();
    private List<ParticleSystem> preciptationParticles = new List<ParticleSystem>();

    public void Init()
    {
        FillList();
        DisableLaser();

        if (!isDirectionStatic) {
            direction = transform.Find("Direction").transform;
        }
    }

    public void EnableLaser()
    {
        particles.ForEach((ParticleSystem ps) => ps.Play());
        preciptationParticles.ForEach((ParticleSystem ps) => ps.Stop());

        lineRenderer.enabled = true;
        preciptationLineRenderer.enabled = false;
        isDisabled = false;
    }

    public void UpdateLaser(LineRenderer laser)
    {
        laser.SetPosition(0, (Vector2) firePoint.position);
        laser.SetPosition(1, isDirectionStatic ? laserDirection : (Vector2) direction.position);

        startVFX.transform.position = (Vector2) firePoint.position;

        RaycastHit2D hit = Physics2D.Raycast(
            origin: firePoint.position,
            direction: isDirectionStatic ? laserDirection : (Vector2) direction.position.normalized,
            distance: isDirectionStatic ? laserMaxDistance : direction.position.magnitude
        );

        if (hit) {
            this.hit = hit;

            laser.SetPosition(1, hit.point);
        }

        endVFX.transform.position = (Vector2) laser.GetPosition(1);
    }

    public void DisableLaser()
    {
        particles.ForEach((ParticleSystem ps) => ps.Stop());
        preciptationParticles.ForEach((ParticleSystem ps) => ps.Play());
        
        lineRenderer.enabled = false;
        preciptationLineRenderer.enabled = true;
        isDisabled = true;
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
        
        for (int i = 0; i < preciptationVFX.transform.childCount; i++) {
            ParticleSystem ps = preciptationVFX.transform.GetChild(i).GetComponent<ParticleSystem>();

            if (ps != null)
                preciptationParticles.Add(ps);
        }
    }
}
