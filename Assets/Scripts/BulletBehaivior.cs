using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaivior : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, direction, step);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(transform.gameObject);
    }
}
