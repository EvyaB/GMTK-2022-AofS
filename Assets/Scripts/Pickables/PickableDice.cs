﻿using System.Collections;
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

    protected override void OnPickup(Collider other)
    {
        if (other.tag == "Player")
        {
            pickedItem = true;
            gameManager.FoundDice(diceType);
        }
    }
}
