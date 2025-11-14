using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool test = false;

    public float horizontal;
    public float speed = 8f;
    public bool isFacingRight;
    public Transform groundCheck;
    public LayerMask ground;
    public float jumpingPower = 8f;
    public Rigidbody2D rb;

    public PlayerCombat combat;
    //public bool enableMeele = true;
    //public Transform attackPoint;
    //public float meeleRange = 0.5f;
    //public LayerMask enemyLayer;
    //public bool canMeeleDash = true;
    //public bool inMeeleDash;
    //public float meeleDashPower = -20f; //Only negative numbers
    //public float meeleTime = 0.2f;
    //public float meeleDashTime = 0.1f;
    //public float meeleDashCooldown = 1f;

    public bool groundSlam = false;
    public float groundSlamPower = -10f; //Only negative numbers
    [SerializeField] bool enableGroundSlaming;

    [SerializeField] bool enableDashing;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = -24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;



    //[SerializeField] private TrailRenderer tr;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded() || Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && enableDashing)
        {
            StartCoroutine(Dash());
        }

        if (enableGroundSlaming)
        {

            if (!IsGrounded() && Input.GetKeyDown(KeyCode.S)|| !IsGrounded() && Input.GetKeyDown(KeyCode.DownArrow))
            {
                groundSlam = true;
                rb.velocity = new Vector2(rb.velocity.x, groundSlamPower);
            }

            if (IsGrounded() && groundSlam && enableGroundSlaming)
            {
                groundSlam = false;
            }
        }

        //if (inMeeleRange())
        //{
        //    //Debug.Log("Enemy in range");
        //}

        //if (Input.GetKeyDown(KeyCode.F) && canMeeleDash && enableMeele)
        //{
        //    StartCoroutine(meeleDash());
        //}
    }

    private void FixedUpdate()
    {
        if (isDashing || combat.isAttacking)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
    }

    void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }
    }

    //public IEnumerator Attack()
    //{
    //   Vector2 meelePointRight = new Vector2(transform.position.x + 0.5f, transform.position.y);
    //    Vector2 meelePointLeft = new Vector2(transform.position.x - 0.5f, transform.position.y);
    //
    //    if (isFacingRight)
    //    {
    //        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meelePointRight, meeleRange, enemyLayer);
    //        foreach (Collider2D enemy in hitEnemies)
    //        {
    //            //Damage enemy
    //            Debug.Log("Hit " + enemy.name);
    //        }
    //    }
    //    else
    //    {
    //        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meelePointLeft, meeleRange, enemyLayer);
    //        foreach (Collider2D enemy in hitEnemies)
    //        {
    //            //Damage enemy
    //            Debug.Log("Hit " + enemy.name);
    //        }
    //    }
    //    yield return meeleTime;
    //}

    //public bool inMeeleRange()
    //{
    //    Vector2 meelePointRight = new Vector2(transform.position.x + 1f, transform.position.y);
    //    Vector2 meelePointLeft = new Vector2(transform.position.x - 1f, transform.position.y);
    //    if (isFacingRight)
    //        return Physics2D.OverlapCircle(meelePointRight, meeleRange, enemyLayer);
    //    else
    //        return Physics2D.OverlapCircle(meelePointLeft, meeleRange, enemyLayer);
    //}

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    //public IEnumerator meeleDash()
    //{
    //    canMeeleDash = false;
    //    inMeeleDash = true;
    //    float originalGravity = rb.gravityScale;
    //    rb.gravityScale = 0f;
    //    rb.velocity = new Vector2(transform.localScale.x * meeleDashPower, 0f);
    //    StartCoroutine(Attack());
    //    yield return new WaitForSeconds(meeleDashTime);
    //    rb.gravityScale = originalGravity;
    //    inMeeleDash = false;
    //    yield return new WaitForSeconds(meeleDashCooldown);
    //    canMeeleDash = true;
    //}

    //void OnDrawGizmosSelected()
    //{
    //    if (attackPoint == null)
    //        return;
    //    Gizmos.DrawWireSphere(---, meeleRange);
    //}
}
