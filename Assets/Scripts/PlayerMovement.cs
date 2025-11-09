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
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if (horizontal == 0)
        {
            anim.SetBool("is walking", false);
        }
        else
        {
            anim.SetBool("is walking", true);
        }

        bool isWalking = horizontal != 0;
        anim.SetBool("is walking", isWalking);

        //Play/Stop walking sound based on movement state
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
