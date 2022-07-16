using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    float backwardSpeed;
    bool isHooked = false;
    PullableObject hookedObj;
    Vector3 relativePositionToHookedObj;

    // Start is called before the first frame update
    void Start()
    {
        HookTrigger.returnedEvent.AddListener(() => Destroy(this.gameObject));
        PullCaster.castCancelEvent.AddListener(() =>
        {
            speed = -backwardSpeed;
            isHooked = false;
        });
        HookTrigger.collisionEvent.AddListener((Collider c) =>
        {
            speed = -backwardSpeed; // THIS NOT TAKE INTO ACCOUNT PLAYER MPVEMENT

            hookedObj = c.gameObject.GetComponent<PullableObject>();
            if (hookedObj != null)
            {
                isHooked = true;
                relativePositionToHookedObj = c.transform.position - transform.position;

            }

        });
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed);


        if (isHooked)
        {
            hookedObj.transform.position = transform.position + relativePositionToHookedObj;
        }
    }
}
