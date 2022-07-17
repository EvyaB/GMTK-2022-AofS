using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Shooter))]
public class playerShooting : MonoBehaviour
{
    public bool isTopDown = false;
    [SerializeField] float gunFireDelay = 0f;

    public GameObject gunShot;
    GameObject playerGunShot;
    Shooter s;

    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {
        // Shooting
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 InitDir = (transform.position + (transform.forward * 1));
            InitDir.z = 0;
            Vector3 lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            s.Shoot(InitDir, transform.rotation, lookDirection);
        }
    }
}
