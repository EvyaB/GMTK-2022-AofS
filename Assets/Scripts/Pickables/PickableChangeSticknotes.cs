using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableChangeSticknotes : Pickable
{
    [SerializeField] private GameObject disappearSticknote;
    [SerializeField] private GameObject appearSticknote;

    protected override void OnPickup(Collider other)
    {
        if (other.tag == "Player")
        {
            pickedItem = true;
            disappearSticknote?.SetActive(false);
            appearSticknote?.SetActive(true);
        }
    }
}
