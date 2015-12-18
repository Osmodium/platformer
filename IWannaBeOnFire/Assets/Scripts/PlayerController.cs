using UnityEngine;
using UnityEngine.UI;

class PlayerController : MonoBehaviour
{
    public float targetJumpHeight = 1f;
    public float runSpeed = 10.0f;
    public float gravity = -9.8f;
    public CharacterController2D _controller;
    public int deaths = 0;
    public ParticleSystem deathParticleSystem;
    public Transform bullet;
    public Text deathText;
    public float fireCooldown = 0.1f;
    private float _fireCD = 0.0f;
    private bool hasDoubleJump = false;
    private Animator _animator;
    private bool _facingRight = true;
    private Vector2 _savePosition;
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _lastDeathParticle;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool isOnElevator = false;
    [HideInInspector] public float elevatorXSpeed = 0f;
    [HideInInspector] public float elevatorYSpeed = 0f;

    public void Awake()
    {
        _controller = gameObject.GetComponentInChildren<CharacterController2D>() as CharacterController2D;
        _animator = gameObject.GetComponentInChildren<Animator>() as Animator;
        _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>() as SpriteRenderer;
    }

    public void Start()
    {
        
        _savePosition = transform.position;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            if (!isDead)
                Die(false);

            transform.position = _savePosition;
            GetComponent<Collider2D>().enabled = true;

            if (_lastDeathParticle)
                _lastDeathParticle.GetComponent<ParticleSystem>().Stop();
            
            _spriteRenderer.enabled = true;
            isDead = false;
        }

        if (isDead)
            return;
        
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        //Debug.Log(isOnElevator);
        Vector3 velocity = _controller.velocity;

        if (_controller.isGrounded)
        {
            velocity.y = 0;
            hasDoubleJump = true;
        }

        float axis = Input.GetAxis("Horizontal");
        _animator.SetFloat("Speed", Mathf.Abs(axis));

        if (isOnElevator)
        {
            //Debug.Log(elevatorYSpeed);
            if (!_controller.isGrounded && elevatorYSpeed < 0)
                _controller.warpToGrounded();
            
            //velocity.x = elevatorXSpeed;
            //velocity.y = Mathf.Sqrt(2f * elevatorYSpeed*3.5f * -gravity);
            //velocity += new Vector3(elevatorXSpeed*runSpeed, elevatorYSpeed, 0);
            _controller.move(new Vector3(elevatorXSpeed, elevatorYSpeed, 0));
            //velocity.x = 0;
            //transform.position += new Vector3(elevatorXSpeed, 0, 0);
        }
        
        //if (Input.GetKey(KeyCode.RightArrow))
        if (Input.GetButton("Right"))
        {
            velocity.x = runSpeed;
            if (!_facingRight)
                Flip();
        }
        //else if (Input.GetKey(KeyCode.LeftArrow))
        else if (Input.GetButton("Left"))
        {
            velocity.x = -runSpeed;
            if (_facingRight)
                Flip();
        }
        else
        {
            velocity.x = 0;
        }

        if (Input.GetButtonDown("Jump") && (_controller.isGrounded || hasDoubleJump))
        {
            //Debug.Log(isOnElevator);
            if (!_controller.isGrounded && !isOnElevator)
                hasDoubleJump = false;

            isOnElevator = false;
            elevatorYSpeed = 0f;

            velocity.y = Mathf.Sqrt(2f * targetJumpHeight * -gravity);
        }

        _fireCD -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && _fireCD <= 0.0f)
        {
            _fireCD = fireCooldown;
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
            Vector3 spawnSide = (_facingRight ? transform.right : -transform.right) * 0.15f;
            Transform b = Instantiate(bullet, spawnPosition + spawnSide, Quaternion.identity) as Transform;
            b.Rotate(Vector3.back, (_facingRight ? 90f : 270f));
        }

        velocity.y += gravity*Time.deltaTime;
        _controller.move(velocity * Time.deltaTime);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Save()
    {
        _savePosition = transform.position;
    }

    public void Die(bool splat)
    {
        if(isDead)
            return;
        _controller.velocity = Vector3.zero;
        _spriteRenderer.enabled = false;
        isDead = true;
        deathText.text = string.Format("Deaths: {0}", ++deaths);
        if (splat)
        {
            ParticleSystem ps = Instantiate(deathParticleSystem, transform.position, Quaternion.identity) as ParticleSystem;
            _lastDeathParticle = ps;
        }
        GetComponent<Collider2D>().enabled = false;
    }

    public void Die()
    {
        Die(true);
    }
}

