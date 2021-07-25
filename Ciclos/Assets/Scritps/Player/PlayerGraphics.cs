using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphics : MonoBehaviour
{
    [SerializeField] private Animator scaleAnimator = null;

    private Player player = null;
    private Collision col = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private InputManager inputManager = null;

    private float landAirTime;

    private bool hasLanded;

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
        SetAnimationBools();
    }

    private void SetAnimationBools()
    {
        anim.SetBool("OnGround", col.isGrounded);
        anim.SetBool("OnWall", col.isOnWall);
        anim.SetBool("OnRightWall", col.isOnRightWall);
        anim.SetBool("CanMove", player.canMove);
    }

    public void SetMovement(float yVel)
    {
        anim.SetFloat("HorizontalAxis", Mathf.Abs(inputManager.xAxis));
        anim.SetFloat("VerticalVelocity", yVel);
    }

    public void SetTrigger(string triggerID = "", string heightTriggerID = "")
    {
        if (triggerID != "") {
            anim.SetTrigger(triggerID);
        }

        if (heightTriggerID != "") {
            scaleAnimator.SetTrigger(heightTriggerID);
        }
    }

    public void Flip(int side)
    {
        if (player.wallSlide) {
            if (side == -1 && sr.flipX)
                return;

            if (side == 1 && !sr.flipX)
                return;
        }

        sr.flipX = side != 1;
    }
}
