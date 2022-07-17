using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour
{
    public float speed = 10f;
    public bool isTopDown = false;
    bool isGrounded = false;
    Rigidbody rb;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    private bool hasFired = false;
    [SerializeField] float gunFireDelay = 0f;

    public GameObject gunShot;
    GameObject playerGunShot;
    Shooter s;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 1.5f, 0.0f);
        s = GetComponent<Shooter>();
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

        // Shooting
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 InitDir = (transform.position + (transform.forward * 1));
            InitDir.z = 0;
            Vector3 lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            s.Shoot(InitDir, transform.rotation, lookDirection);
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
