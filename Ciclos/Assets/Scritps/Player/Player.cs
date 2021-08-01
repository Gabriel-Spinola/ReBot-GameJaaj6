using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerGraphics))]
[RequireComponent(typeof(Rigidbody2D), typeof(Collision))]
public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem jumpParticle;
    [SerializeField] private ParticleSystem wallJumpParticle;
    [SerializeField] private ParticleSystem slideParticle;
    [SerializeField] private RoomManager roomManager;

    public InputManager InputManager;

    [SerializeField] private int whatIsSpikesLayerID = 0;

    [SerializeField] private Vector2 s;
    [SerializeField] private float f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float friction = .2f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2;

    [Header("Walljump")]
    [SerializeField] private float wallJumpHeight = 10f;
    [SerializeField] private float wallJumpSpeed = 10f;

    [Header("WallSlide")]
    [SerializeField] private float slideSpeed = 6f;
    [SerializeField] private float slideDelay = 1f;

    [HideInInspector] public PlayerGraphics playerGraphics = null;

    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool wallSlide = false;

    private Rigidbody2D rb = null;
    private Collision col = null;

    private bool wallJumped = false;
    private bool avoidDoubleJump = false;
    private bool useBetterJump = true;
    private bool isInterpolationDisabled = false;
    private bool canChangeInterpolation = false;
    private bool disableInterpolation = false;
    private bool isPlayerDisabled = false;

    private bool prevGrounded = false;

    private int side = 1;
    private int canJump = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collision>();
        playerGraphics = GetComponentInChildren<PlayerGraphics>();
    }

    private void Update()
    {
        if (DialoguesManager.IsOnADialogue) {
            rb.velocity = Vector2.zero;

            return;
        }

        if (isPlayerDisabled) {
            return;
        }

        if (IsCrushed()) {
            Die();
        }

        if (col.isGrounded && !prevGrounded) {
            playerGraphics.SetTrigger("", "Squash");
        }

        BetterJump();
        Movement();
        WallParticle();

        playerGraphics.SetMovement(rb.velocity.y);

        canJump--;

        if (col.isGrounded) {
            canJump = 8;
        }

        if (col.isGrounded) {
            wallJumped = false;
            useBetterJump = true;
        }

        if (InputManager.keyJump) {
            if (canJump > 0) {
                Jump(jumpForce);
            }

            if (!col.isGrounded && col.isOnWall) {
                WallJump();
            }
        }

        if (col.isOnWall && !col.isGrounded && !InputManager.keyJump && rb.velocity.y < 0.01f) {
            if (InputManager.xAxis != 0) {
                wallSlide = true;

                StartCoroutine(WallSlide(slideDelay));
            }
        }

        if (!col.isOnWall || col.isGrounded)
            wallSlide = false;

        if (InputManager.xAxis != 0 && !wallSlide && canMove) {
            side = (int) InputManager.xAxis;
            playerGraphics.Flip(side);
        }

        prevGrounded = col.isGrounded;
    }

    private void FixedUpdate()
    {
        if (col.isGrounded) {
            if (isInterpolationDisabled && canChangeInterpolation) {
                rb.interpolation = RigidbodyInterpolation2D.Interpolate;

                disableInterpolation = false;
            }
        }

        if (disableInterpolation) {
            rb.interpolation = RigidbodyInterpolation2D.None;
        }
    }

    private void Movement()
    {
        if (!canMove)
            return;

        if (InputManager.xAxis != 0) {
            rb.velocity = Vector2.right * InputManager.xAxis * moveSpeed + Vector2.up * rb.velocity.y;
        }
        else {
            if (!wallJumped) {
                rb.velocity = Vector2.right * Mathf.Lerp(rb.velocity.x, 0f, friction) + Vector2.up * rb.velocity.y;
            }
            else {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, friction - .17f), rb.velocity.y);
            }
        }
    }

    public void Jump(float jumpForce)
    {
        if (avoidDoubleJump)
            return;

        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = jumpParticle;

        playerGraphics.SetTrigger("Jump", "Stretch");

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        particle.Play();
        AudioManager._I.PlaySound2D("Jump");

        canJump = 0;

        StartCoroutine(DisableJump(.3f));
    }

    public void Jump(Vector2 jumpDir)
    {
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wallJumpParticle;

        playerGraphics.SetTrigger("Jump", "Stretch");

        rb.velocity = jumpDir;

        particle.Play();
        AudioManager._I.PlaySound2D("Jump");

        canJump = 0;
        StartCoroutine(DisableJump(.3f));
    }

    private void WallJump()
    {
        if (( side == 1 && col.isOnRightWall ) || side == -1 && !col.isOnRightWall) {
            side *= -1;
            playerGraphics.Flip(side);
        }

        StartCoroutine(DisableMovement(.2f));

        int wallJumpDir = col.isOnRightWall ? -1 : col.isOnLeftWall ? 1 : 0;
        wallJumped = true;

        Jump(Vector2.right * wallJumpSpeed / 1.2f * wallJumpDir + Vector2.up * wallJumpHeight / 1.2f);
    }

    private IEnumerator WallSlide(float time)
    {
        if (col.wallSide != side)
            playerGraphics.Flip(side * -1);

        yield return new WaitForSeconds(time);

        if (!canMove || InputManager.keyJump || InputManager.keyJumpHold)
            yield break;

        bool pushingWall = false;

        if ((rb.velocity.x > 0 && col.isOnRightWall) || (rb.velocity.x < 0 && col.isOnLeftWall)) {
            pushingWall = true;
        }

        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

    /// <summary>
    /// if falling, add fallMultiplier
    /// if jumping and not holding spacebar or walljumping, increase gravity to peform a small jump
    /// if jumping and holding spacebar, perform a full jump
    /// </summary>
    private void BetterJump()
    {
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics.gravity.y * ( fallMultiplier - 1 ) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !InputManager.keyJumpHold || wallJumped) {
            rb.velocity += Vector2.up * Physics.gravity.y * ( lowJumpMultiplier - 1 ) * Time.deltaTime;
        }
    }

    private void WallParticle()
    {
        var main = slideParticle.main;

        if (wallSlide) {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else {
            main.startColor = Color.clear;
        }
    }

    private int ParticleSide() => col.isOnRightWall ? 1 : -1;

    private bool IsCrushed() => col.isGrounded && Physics2D.OverlapCircle((Vector2) transform.position + s, f).CompareTag("Platform");

    public IEnumerator DisableMovement(float time)
    {
        canMove = false;

        yield return new WaitForSeconds(time);

        canMove = true;
    }

    public IEnumerator DisablePlayer(float time)
    {
        isPlayerDisabled = true;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        InputManager.xAxis = 0f;

        yield return new WaitForSeconds(time);

        isPlayerDisabled = false;
    }

    public IEnumerator DisableJump(float time)
    {
        avoidDoubleJump = true;

        yield return new WaitForSeconds(time);

        avoidDoubleJump = false;
    }

    private void Die()
    {
#if UNITY_EDITOR
        Debug.Log("Died");

        return;
#endif
#pragma warning disable CS0162
        roomManager.Respawn();
#pragma warning restore CS0162
    }

    public void TakeDamage() => Die();

    public Rigidbody2D GetRigidbody() => rb;
    public void SetUseBetterJump(bool val) => useBetterJump = val;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == whatIsSpikesLayerID) {
            TakeDamage();
        }

        if (other.gameObject.CompareTag("Platform")) {
            transform.parent = other.gameObject.transform;

            disableInterpolation = true;

            isInterpolationDisabled = true;
            canChangeInterpolation = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform")) {
            transform.parent = null;

            canChangeInterpolation = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) {
            TakeDamage();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2) transform.position + s, f);
    }
}