using UnityEngine;

public class AIController : MonoBehaviour
{
    public float moveSpped = 3f;
    private Rigidbody2D rb;
    private Vector2 movement;

    public Transform followTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (followTarget != null)
        {
            Vector2 dir = (followTarget.position - transform.position).normalized;
            movement = dir;
        } else
        {
            movement = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpped * Time.fixedDeltaTime);
    }
}
