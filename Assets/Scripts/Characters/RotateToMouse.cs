using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.Normalize();
        float zRotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
        //transform.Rotate(new Vector3(0, 0, transform.rotation.z + zRotation / 30));
    }
}
