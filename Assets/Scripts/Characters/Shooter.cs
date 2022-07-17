using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private bool hasFired = false;
    [SerializeField] float gunFireDelay = 0f;

    public GameObject gunShot;
    GameObject playerGunShot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }


    public void Shoot(Vector3 startLocation, Quaternion startRotation, Vector3 shootDirection)
    {
        if (!hasFired)
        {
            StartCoroutine(FireGun(startLocation, startRotation, shootDirection));
        }
    }

    IEnumerator FireGun(Vector3 startLocation, Quaternion startRotation, Vector3 shootDirection)
    {
        hasFired = true;
        playerGunShot = Instantiate(gunShot, startLocation, startRotation);
        playerGunShot.GetComponent<GunShot>().direction = shootDirection.normalized;
        yield return new WaitForSeconds(gunFireDelay);
        hasFired = false;
    }
}
