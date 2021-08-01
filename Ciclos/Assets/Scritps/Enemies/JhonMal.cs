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

    private Animator anim;

    private bool started = false;

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
            anim.SetTrigger("Final");

            if(!DialoguesManager.IsOnADialogue) {
                Application.Quit();

                Debug.Log("QUIT");
            }
        }
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
