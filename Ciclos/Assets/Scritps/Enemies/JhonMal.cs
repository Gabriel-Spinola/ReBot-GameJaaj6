using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JhonMal : MonoBehaviour
{
    public static bool shoot;
    public static bool playerDied = false;

    [SerializeField] private GameObject bullet;
    [SerializeField] private DialogueTrigger trigger;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject camera2;

    private Animator anim;

    private bool started = false;
    bool Olhando = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (shoot && !playerDied) {
            anim.SetTrigger("Shoot");
        }

        if (playerDied) {
            if (!Olhando)
                anim.SetTrigger("Final");

            if(!DialoguesManager.IsOnADialogue && started) {
                StartCoroutine(END());
            }
        }
    }

    private IEnumerator END()
    {
        Olhando = true;
        anim.SetTrigger("Olhando");
        if (FindObjectOfType<MusicManager>() != null) {
            Destroy(FindObjectOfType<MusicManager>().gameObject);
        }

        camera1.SetActive(false);
        camera2.SetActive(true);

        yield return new WaitForSeconds(10f);

        Application.Quit();
        Debug.Log("Quit");
    }

    public void Shoot()
    {
        CommonBullet currentBullet = Instantiate(bullet, shootPoint.position, transform.rotation).GetComponent<CommonBullet>();

        currentBullet.isJhon = true;
        currentBullet.speed = 10f;
        currentBullet.dir = new Vector2(-1f, 0f);
        currentBullet.xScale = (int) transform.localScale.x;

        AudioManager._I.PlaySound2D("Shoot", .6f, 180);
    }

    public void StartFinalDialogue()
    {
        if (!started) {
            trigger.TriggerDialogue();

            started = true;
        }
    }
}
