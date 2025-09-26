using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Combat : MonoBehaviour
{
    public string targetTag = "Enemy";   // Player�� "Enemy", Enemy�� "Player"
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    public int attackDamage = 10;
    public float moveSpeed = 3f;         // �̵� �ӵ�

    private float lastAttackTime = 0f;
    private Transform target;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // �̵� ����
    }

    void Update()
    {
        FindTarget();

        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackRange)
        {
            // ���� �� �� Ÿ�� �Ѿư���
            Vector2 newPos = Vector2.MoveTowards(rb.position, target.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(newPos);
        }
        else
        {
            // ���� �� �� ����
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack(target);
                lastAttackTime = Time.time;
            }
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        target = nearestEnemy != null ? nearestEnemy.transform : null;
    }

    void Attack(Transform enemy)
    {
        Debug.Log($"{gameObject.name} attacks {enemy.name} for {attackDamage} damage!");

        Character c = enemy.GetComponent<Character>();
        if (c != null)
        {
            c.TakeMeleeDamage(attackDamage);
        }
    }

    // ���� ���� Ȯ�ο� Gizmo
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}