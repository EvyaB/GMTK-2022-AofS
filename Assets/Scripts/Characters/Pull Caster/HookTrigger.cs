using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HookTrigger : MonoBehaviour
{
    public static readonly UnityEvent returnedEvent = new();
    public static readonly UnityEvent<Collider> collisionEvent = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PullCaster>() != null)
        {
            returnedEvent.Invoke();
        }
        else
        {
            collisionEvent.Invoke(other);
        }
    }
}
