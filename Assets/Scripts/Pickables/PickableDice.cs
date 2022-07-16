using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableDice : Pickable
{
    private void Update()
    {
        if (this.shouldRotate)
        {
            this.transform.Rotate(rotationDirection, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pickedItem && other.tag == "Player")
        {
            pickedItem = true;
            gameManager.FoundDice();
            Destroy(gameObject, this.destoryDelay);
        }
    }
}
