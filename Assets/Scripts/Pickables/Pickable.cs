using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Pickable : MonoBehaviour
{
    [SerializeField] protected bool shouldRotate = true;
    [SerializeField] protected float rotationSpeed = 30.0f;
    [SerializeField] protected Vector3 rotationDirection = Vector3.left;

    [SerializeField] protected bool shouldRespawn = false;
    [SerializeField] protected bool shouldDestroy = true;
    [SerializeField] protected float destoryDelay = 0.1f;
    [SerializeField] protected float respawnDelay = 5f;
    protected bool pickedItem = false;
    private bool disablePickable = false;

    protected GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    protected abstract void OnPickup(Collider other);

    private void OnTriggerEnter(Collider other)
    {
        if (!disablePickable)
        {
            OnPickup(other);
            StartCoroutine(AfterPickup());
        }
    }

    protected IEnumerator AfterPickup()
    {
        if (pickedItem)
        {
            disablePickable = true;
            if (shouldRespawn)
            {
                var renderer = GetComponent<Renderer>();
                var collider = GetComponent<Collider>();
                renderer.enabled = false;
                collider.enabled = false;

                yield return new WaitForSeconds(respawnDelay);
                pickedItem = false;
                disablePickable = false;
                renderer.enabled = true;
                collider.enabled = true;
            }
            if (shouldDestroy)
            {
                Destroy(gameObject, this.destoryDelay);
            }
        }
    }

}
