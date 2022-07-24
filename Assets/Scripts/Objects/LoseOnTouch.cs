using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseOnTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Glob.Tags.Player))
        {
            FindObjectOfType<GameManager>().InformPlayerDeath(gameObject);
        }
    }
}
