using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlerTopDown : MonoBehaviour
{
    [SerializeField]
    float speed = 1;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movement
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(xMove, yMove) * speed;

        // Rotation
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.MoveRotation(Quaternion.LookRotation(Vector3.forward, mousePos - transform.position));
    }
}
