using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerControlerTopDown : MonoBehaviour
{
    [SerializeField]
    float speed = 1;
    Rigidbody rb;

    SpriteRenderer sr;
    SpriteAtlas atlas;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movement
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(xMove, yMove) * speed;

        if (xMove != 0)
            sr.sprite = atlas.GetSprite("top-down-right");
 
        // Rotation
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.MoveRotation(Quaternion.LookRotation(Vector3.forward, mousePos - transform.position));
    }
}
