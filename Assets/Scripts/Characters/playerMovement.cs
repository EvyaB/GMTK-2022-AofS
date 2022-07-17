using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour
{
    public float speed = 10f;
    public bool isTopDown = false;
    bool isGrounded = false;
    Rigidbody rb;
    public Vector3 jump = new Vector3(0.0f, 3.0f, 0.0f);
    public float jumpForce = 2.0f;
    [SerializeField] float gunFireDelay = 0f;

    public GameObject gunShot;


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

            //Movement
            float  yMove = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(xMove, yMove) * speed;

            // Rotation
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.MoveRotation(Quaternion.LookRotation(Vector3.forward, mousePos - transform.position));
        }
        else
        {
            rb.useGravity = true;

            //Movement
            Vector3 m_Input = new Vector3(xMove, 0, 0);
            rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
        }


        // Jumping 
        if (Input.GetKeyDown(KeyCode.Space) && !isTopDown && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
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
