using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private LayerMask whatsIsPlayer = 0;
    [SerializeField] private float kockbackForce = 10f;
    [SerializeField] private float r = .2f;
    [SerializeField] private float timeToActivate = .12f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DeactivateAnim() => animator.SetBool("IsActive", false);

    private void OnDisable()
    {
        DeactivateAnim();
        animator.SetTrigger("Deactivate");
    }

    private IEnumerator WaitToStart(float time, Player player)
    {
        yield return new WaitForSeconds(time);

        if (Physics2D.OverlapCircle(transform.position, r, whatsIsPlayer)) {
            animator.SetBool("IsActive", true);

            player.SetUseBetterJump(false);
            player.Jump(kockbackForce);   
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Player player_ = collision.gameObject.GetComponent<Player>();

            StartCoroutine(WaitToStart(timeToActivate, player_));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, r);
    }
}
