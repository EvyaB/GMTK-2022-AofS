using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] float minPullDistance = 0.5f;
    [SerializeField]
    private float speed;
    [SerializeField]
    float hookedSpeed;
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
            if (hookedObj != null && Input.GetKey(KeyCode.Mouse0))
            {
                var distance = Vector3.Distance(pullCasterInstance.transform.position, hookedObj.transform.position);
                var colliderExtent =  ((Vector2)hookedObj.GetComponent<BoxCollider>().bounds.extents).magnitude;
                if (distance - colliderExtent > minPullDistance)
                {
                    isHooked = true;
                    relativePositionToHookedObj = c.transform.position - transform.position;
                }
            }

        });
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!onTheWayBack)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            var backSpeed = isHooked ? hookedSpeed : speed;
            var dir = pullCasterInstance.transform.position - gameObject.transform.position;
            transform.Translate(dir.normalized * backSpeed * Time.deltaTime, Space.World);
        }
        if (isHooked && hookedObj != null)
        {
            hookedObj.transform.position = transform.position + relativePositionToHookedObj;
        }

    }
    private void LateUpdate()
    {

    }
}
