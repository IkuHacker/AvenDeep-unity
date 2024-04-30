using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 350f;
    public float climbSpeed = 7f;

    public bool isGrounded;
    public bool isJumping;
    public bool isClimbing;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastDistance = 0.2f;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;
    private float verticalMovement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D rb;
    public CapsuleCollider2D playerCollider;

    public AudioClip jumpSound;

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scène");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed;

        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, raycastDistance, groundLayer);

        Debug.DrawRay(groundCheck.position, Vector2.down * raycastDistance, Color.red);

       
        if (Input.GetButtonDown("Jump") && isGrounded && hit.collider != null && hit.collider.gameObject.tag != "PNJ")
        {
            AudioManager.instance.PlayClipAt(jumpSound, transform.position);
            isJumping = true;
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("speed", characterVelocity);
        animator.SetBool("isJumping", !isGrounded);
        animator.SetBool("isClimbing", isClimbing);

        
        
        

    }

    private void FixedUpdate()
    {
        // Mise à jour du statut "isGrounded" en fonction du raycast
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, raycastDistance, groundLayer);

        MovePlayer(horizontalMovement, verticalMovement);
    }

    private void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        if (!isClimbing) 
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);

            if (isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
        else 
        {
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
        }
       
    }

    private void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
}
