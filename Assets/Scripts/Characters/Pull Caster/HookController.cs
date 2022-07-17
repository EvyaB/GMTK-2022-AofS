using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    float backwardSpeed;
    bool isHooked = false;
    bool onTheWayBack = false;
    PullableObject hookedObj;
    Vector3 relativePositionToHookedObj;
    PullCaster pullCasterInstance;

    // Start is called before the first frame update
    void Start()
    {
        pullCasterInstance = FindObjectOfType<PullCaster>();
        HookTrigger.returnedEvent.AddListener(() => Destroy(gameObject));
        PullCaster.castCancelEvent.AddListener(() =>
        {
            onTheWayBack = true;
            isHooked = false;
        });
        HookTrigger.collisionEvent.AddListener((Collider c) =>
        {
            onTheWayBack = true;

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

        if (isHooked)
        {
            hookedObj.transform.position = transform.position + relativePositionToHookedObj;
        }
    }
    private void LateUpdate()
    {
        if (!onTheWayBack)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            var dir = pullCasterInstance.transform.position - gameObject.transform.position;
            transform.Translate(dir.normalized* backwardSpeed * Time.deltaTime, Space.World);
        }
    }
}
