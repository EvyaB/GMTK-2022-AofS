using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour
{
    public float speed = 10f;
    public bool isTopDown = false;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public bool canJump = false;
    public Vector3 jump = new Vector3(0.0f, 3.0f, 0.0f);
    public float jumpForce = 2.0f;

    public float movementAcceleration = 50f;
    public float maxMoveSpeed = 12f;

    [Range(0f, 1f)]
    public float linearDrag = 0.75f;

    bool isGrounded = false;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        if (isTopDown)
        {
            rb.useGravity = false;

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
        if (canJump)
        {
            // Jump
            if (Input.GetKeyDown(KeyCode.Space) && !isTopDown && isGrounded)
            {
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                isGrounded = false;
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

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

}
