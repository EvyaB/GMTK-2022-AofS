using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSticknotes : Pickable
{
    [SerializeField] private GameObject disappearSticknote;
    [SerializeField] private GameObject appearSticknote;

    private void OnTriggerEnter(Collider other)
    {
        if (!pickedItem && other.tag == "Player")
        {
            disappearSticknote?.SetActive(false);
            appearSticknote?.SetActive(true);
            Destroy(gameObject, this.destoryDelay);
        }
    }
}
