using UnityEngine;

public class PlayerControl : MonoBehaviour 
{
    public bool hasControl = true;
    public float maxSpeed = 20.0f;
    public bool canWalk = true;
    public bool isInvinsible = false;
    public bool canJump = true;
    public bool canDoubleJump = true;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 100.0f;

    private bool facingRight = true;
    private Animator animator;
    private bool grounded = false;

    private bool hasDoubleJump = true;

	public void Start()
	{
	    animator = gameObject.GetComponentInChildren<Animator>();
	}
	
	public void FixedUpdate()
	{

	    if (!hasControl)
	    {
            animator.SetFloat("Speed", Mathf.Abs(0f));
	        return;
	    }

	    grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

	    if (grounded)
	        hasDoubleJump = true;
        
	    float move = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(move));

	    GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

	    if (move > 0 && !facingRight)
	        Flip();
	    else if (move < 0 && facingRight)
	        Flip();

	}

    public void Update()
    {
        if(!hasControl) return;
        if ((grounded || hasDoubleJump) && Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));

            if (!grounded && hasDoubleJump)
                hasDoubleJump = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Die()
    {
        Debug.Log("You die!");
        hasControl = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
