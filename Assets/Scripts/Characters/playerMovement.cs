using UnityEngine;
using System.Collections;
using System;

public class playerMovement : MonoBehaviour
{
    public float speed = 10f;
    public bool isTopDown = false;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public bool canJump = false;
    public Vector3 jump = new Vector3(0.0f, 3.0f, 0.0f);
    public float jumpForce = 2.0f;

    public int maxJumpCounts = 1;

    private int remainingJumps = 1;

    public float movementAcceleration = 50f;
    public float maxMoveSpeed = 12f;

    [Range(0f, 1f)]
    public float linearDrag = 0.75f;

    bool isGrounded = false;
    Rigidbody rb;

    public float minimumTimeBetweenJumps = 0.3f;
    float timerSinceLastJump = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();

        if (isTopDown)
        {
            rb.useGravity = false;
            maxJumpCounts = 0; // can't jump in top down game
            canJump = false;

            //Movement
            float yMove = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(xMove, yMove) * speed * Time.deltaTime;

            // Rotation
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.MoveRotation(Quaternion.LookRotation(Vector3.forward, mousePos - transform.position));
        }
        else
        {
            // Avoid unity fields that we override
            rb.drag = 0;
            rb.useGravity = true;

            //Movement
            Vector3 m_Input = new Vector3(xMove, 0, 0);
            //      rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
            rb.AddForce(new Vector3(xMove, 0f, 0f) * movementAcceleration);
            if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
            {
                rb.velocity = new Vector3(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
            }
        }

        // Jumping 
        // Reset jumps count if on the ground
        if (isGrounded && timerSinceLastJump <= 0.0f)
        {
            remainingJumps = maxJumpCounts;
        }

        if (canJump)
        {
            timerSinceLastJump -= Time.deltaTime;

            // Jump
            if (Input.GetKeyDown(KeyCode.Space) && !isTopDown && remainingJumps > 0 && timerSinceLastJump <= 0.0f)
            {
                timerSinceLastJump = minimumTimeBetweenJumps;
                remainingJumps--;
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            }
        }

        // Handle falling behaviour
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            // jumping (and possibly holding button for long/short jump)
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        // Bit shift the index of the Player layer (8) and UI (5) to get a bit mask without the player or the UI
        int layerMask = 1 << 7;
        layerMask += 1 << 5;

        // This would cast rays only against colliders in layer 7.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        var boxCollider = this.GetComponent<BoxCollider>();
        float distanceDown = 0.01f;
        if (boxCollider != null)
        {
            distanceDown += boxCollider.center.y / 2;
        }
        else
        {
            var capsuleCollider = this.GetComponent<CapsuleCollider>();
            distanceDown += capsuleCollider.bounds.size.y / 2;
        }

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player and UI layer
        return (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, distanceDown, layerMask));
    }

    private void OnDrawGizmos()
    {
        float distanceDown = 0.01f;
        var capsuleCollider = this.GetComponent<CapsuleCollider>();
        distanceDown += capsuleCollider.bounds.size.y / 2;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * distanceDown);
    }

    void FixedUpdate()
    {
        float xMove = Input.GetAxis("Horizontal");

        // apply linear drag
        if (!isTopDown && Mathf.Abs(xMove) < 0.4f)
        {
            var vel = rb.velocity;
            vel.x *= 1.0f - linearDrag; // reduce x component...
            vel.z *= 1.0f - linearDrag; // and z component each cycle
            rb.velocity = vel;
        }
        else
        {
            rb.drag = 0f;
        }
    }
}
