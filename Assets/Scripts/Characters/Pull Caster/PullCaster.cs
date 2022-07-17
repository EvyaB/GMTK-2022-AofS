using UnityEngine;
using UnityEngine.Events;

public class PullCaster : MonoBehaviour
{
    [SerializeField]
    GameObject hookPrefab;
    GameObject hookInstance;
    [SerializeField]
    bool isHookCasted = false;
    float safeGourd;
    LineRenderer lr;

    public static readonly UnityEvent castCancelEvent = new();
    // Start is called before the first frame update
    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        var b = GetComponent<BoxCollider>().bounds;
        safeGourd = Mathf.Sqrt(Mathf.Pow(b.extents.x, 2) + Mathf.Pow(b.extents.y, 2));
        HookTrigger.returnedEvent.AddListener(() => isHookCasted = false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isHookCasted && Input.GetKeyDown(KeyCode.Mouse0))
        {
            hookInstance = Instantiate(hookPrefab, transform.position + transform.right * safeGourd, transform.rotation);
            isHookCasted = true;
        }

        if (isHookCasted)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hookInstance.transform.position);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (isHookCasted)
            {
                castCancelEvent.Invoke();
            }
            
        }

    }
}
