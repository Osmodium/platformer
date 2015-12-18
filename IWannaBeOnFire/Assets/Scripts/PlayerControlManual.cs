using UnityEngine;
using System.Collections;
using System.Security.AccessControl;

public class PlayerControlManual : MonoBehaviour
{
    public float speed = 3.0f;
    public string axisName = "Horizontal";
    public Transform groundCheck;
    public Transform forwardCheck;
    public LayerMask whatIsGround;
    public float jumpPower = 300.0f;
    public float groundCheckRadius = 0.11f;
    public float forwardCheckRadius = 0.11f;

    private bool grounded = false;
    private bool forwardValid = false;
    private bool hasDoubleJump = false;
    private Animator anim;
    [HideInInspector]
    public bool facingRight = true;
    private Vector2 savePosition;

	public void Start()
	{
        savePosition = transform.position;
	    anim = gameObject.GetComponentInChildren<Animator>();
	}
    
    public void Update()
    {
        float axis = Input.GetAxis(axisName);
        if (axis > 0 && !facingRight)
            Flip();
        else if (axis < 0 && facingRight)
            Flip();
        
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        forwardValid = Physics2D.OverlapCircle(forwardCheck.position, forwardCheckRadius, whatIsGround);
        
        if (!forwardValid)
        {
            anim.SetFloat("Speed", Mathf.Abs(axis));
            transform.position += transform.right*axis*speed*Time.deltaTime;
        }

        if (grounded)
            hasDoubleJump = true;

        if ((grounded || hasDoubleJump) && Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            GetComponent<Rigidbody2D>().AddForce(transform.up * jumpPower);
            if (!grounded)
                hasDoubleJump = false;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(forwardCheck.position, forwardCheckRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
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
        //savePosition = transform.position;
        transform.position = savePosition;
    }
}
