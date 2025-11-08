using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    private bool isFacingRight = true;
    private Animator anim;
    private AudioSource audioSource;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private LayerMask ceilingLayer;
    [SerializeField] private AudioClip walkingSound;


    // Update is called once per frame
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        if (groundCheck == null)
        {
            GameObject groundCheckObject = new GameObject("GroundCheck");
            groundCheckObject.transform.SetParent(transform);
            groundCheckObject.transform.localPosition = new Vector3(0, -0.5f, 0);
            groundCheck = groundCheckObject.transform;
            Debug.Log("GroundCheck was not assigned. A default one has been created.");
        }

        if (ceilingCheck == null)
        {
            GameObject ceilingCheckObject = new GameObject("CeilingCheck");
            ceilingCheckObject.transform.SetParent(transform);
            ceilingCheckObject.transform.localPosition = new Vector3(0, 0.5f, 0);
            ceilingCheck = ceilingCheckObject.transform;
            Debug.Log("CeilingCheck was not assigned. A default one has been created.");
        }
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        bool isWalking = horizontal != 0;
        anim.SetBool("is walking", isWalking);

        // Play/Stop walking sound based on movement state
        if (isWalking && IsGrounded() && !audioSource.isPlaying)
        {
            audioSource.clip = walkingSound;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if ((!isWalking || !IsGrounded()) && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    

    
}
