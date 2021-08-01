using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphics : MonoBehaviour
{
    [SerializeField] private Animator scaleAnimator = null;

    [HideInInspector] public bool disableAnimation = false;

    private Player player = null;
    private Collision col = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private InputManager inputManager = null;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        col = GetComponentInParent<Collision>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        inputManager = player.InputManager;
    }

    private void Update()
    {
        if (DialoguesManager.IsOnADialogue) {
            ResetAnimation();

            return;
        }

        if (disableAnimation)
            return;
        
        SetAnimationBools();
    }

    public void StartFinal()
    {
        JhonMal.playerDied = true;
    }

    private void SetAnimationBools()
    {
        anim.SetBool("OnGround", col.isGrounded);
        anim.SetBool("OnWall", col.isOnWall);
        anim.SetBool("OnRightWall", col.isOnRightWall);
        anim.SetBool("CanMove", player.canMove);
        anim.SetBool("WallSlide", player.wallSlide);
    }

    public void PlayWalkSound() => AudioManager._I.PlaySound2D("Walk", 1.4f, 100);

    public void SetMovement(float yVel)
    {
        if (disableAnimation)
            return;

        anim.SetFloat("HorizontalAxis", Mathf.Abs(inputManager.xAxis));
        anim.SetFloat("VerticalVelocity", yVel);
    }

    public void SetTrigger(string triggerID = "", string heightTriggerID = "")
    {
        if (disableAnimation)
            return;

        if (triggerID != "") {
            anim.SetTrigger(triggerID);
        }

        if (heightTriggerID != "") {
            scaleAnimator.SetTrigger(heightTriggerID);
        }
    }

    public void SetTrigger(string ID)
    {
        anim.SetTrigger(ID);
    }

    public void Flip(int side)
    {
        if (disableAnimation)
            return;

        if (player.wallSlide) {
            if (side == -1 && sr.flipX)
                return;

            if (side == 1 && !sr.flipX)
                return;
        }

        sr.flipX = side != 1;
    }

    private void ResetAnimation()
    {
        anim.SetBool("OnGround", false);
        anim.SetBool("OnWall", false);
        anim.SetBool("OnRightWall", false);
        anim.SetBool("CanMove", false);

        anim.SetFloat("HorizontalAxis", -0.1f);
        anim.SetFloat("VerticalVelocity", 0f);
    }

    public IEnumerator DisableAnimation(float time)
    {
        ResetAnimation();

        disableAnimation = true;

        yield return new WaitForSeconds(time);

        disableAnimation = false;
    }

   
}
