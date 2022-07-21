using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private GameManager _manager;

    private void Awake()
    {
        _manager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // is player/enemies/projectiles
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8 || other.gameObject.layer == 10)
        {
            if (other.tag == "Player")
            {
                if (_manager != null)
                {
                    _manager.InformPlayerDeath(other.gameObject);
                }
            }

            Destroy(other.gameObject);
        }
    }
}
