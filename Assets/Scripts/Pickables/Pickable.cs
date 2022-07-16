using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickable : MonoBehaviour
{
    [SerializeField] protected bool shouldRotate = true;
    [SerializeField] protected float rotationSpeed = 30.0f;
    [SerializeField] protected Vector3 rotationDirection = Vector3.left;
    [SerializeField] protected float destoryDelay = 0.1f;
    protected bool pickedItem = false;

    protected GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
}
