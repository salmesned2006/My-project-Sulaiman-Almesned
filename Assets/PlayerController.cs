using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float movementX;
    float movementY;
    private float speed = 6; 
    [SerializeField] float jumpPower = 1f;
    private bool jumping = false; 
    [SerializeField] Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private int diamondScore; 
    
    private Animator animator; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
       animator = GetComponent<Animator>();
       spriteRenderer = GetComponent<SpriteRenderer>();
    }
    

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("walking", movementX != 0);
        if (rb.linearVelocityX != 0)
        {
            spriteRenderer.flipX = (rb.linearVelocityX < 0);
        } 
    }
    
    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        movementX = v.x;
        movementY = v.y;
        
        Debug.Log(v);


    }
    
    void OnJump()
    {
        if (touchingGround)
        {
            jumping = true;
        }
    }

    void FixedUpdate()
    {
        float XmoveDistance = movementX * speed * Time.fixedDeltaTime;
        float YmoveDistance = movementY * speed * Time.fixedDeltaTime;

        rb.linearVelocityX = XmoveDistance;

        if (touchingGround && jumping)
        {
            rb.AddForce(jumpPower * Vector2.up, ForceMode2D.Impulse); 
            jumping = false; 
        }
        
        transform.position = new Vector2(transform.position.x + XmoveDistance, transform.position.y + YmoveDistance);
    }
    

    private bool touchingGround; 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            touchingGround = true;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        touchingGround = false;
    }
    
    public void AddDiamond(int value)
    {
        diamondScore += value;
        Debug.Log(value);
    }
}
