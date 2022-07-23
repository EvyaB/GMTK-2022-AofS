using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    private float step;
    public float speed = 8f;
    public Vector3 direction;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(killAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        step = speed * Time.deltaTime;
        direction.z = 0;
        transform.Translate((direction * speed * Time.deltaTime));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            Destroy(gameObject);
    }

    private IEnumerator killAfterTime()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(transform.gameObject);
    }
}
