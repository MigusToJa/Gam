using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public float dashDistance = 0.5f;
    public float dashDuration = 0.1f;
    public float attackCooldown = 0.3f;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public bool isAttacking;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private System.Collections.IEnumerator Attack()
    {
        isAttacking = true;

        // 1. Dash slightly forward
        Vector2 dashDir = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector2 startPos = rb.position;
        Vector2 endPos = startPos + dashDir * dashDistance;

        float elapsed = 0f;
        while (elapsed < dashDuration)
        {
            rb.MovePosition(Vector2.Lerp(startPos, endPos, elapsed / dashDuration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(endPos);

        // 2. Detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Destroy(enemy.gameObject);
        }

        // 3. Cooldown delay
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
