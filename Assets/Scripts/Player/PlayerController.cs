using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    //private Animator anim;
    private Vector2 movement;

    public bool isPlayerControlled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerControlled) return;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //anim.SetFloat("Horizontal", movement.x);
        //anim.SetFloat("Vertical", movement.y);
        //anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        if (!isPlayerControlled) return;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
