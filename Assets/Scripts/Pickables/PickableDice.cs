using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableDice : Pickable
{
    [SerializeField] private DiceType diceType;

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
            gameManager.FoundDice(diceType);
            Destroy(gameObject, this.destoryDelay);
        }
    }
}
