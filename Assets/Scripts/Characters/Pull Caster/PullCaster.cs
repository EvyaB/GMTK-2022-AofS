using UnityEngine;
using UnityEngine.Events;

public class PullCaster : MonoBehaviour
{
    [SerializeField]
    GameObject hook;
    [SerializeField]
    bool isHookCasted = false;
    float safeGourd;

    public static readonly UnityEvent castCancelEvent = new();
    // Start is called before the first frame update
    void Start()
    {
        var b = GetComponent<BoxCollider>().bounds;
        safeGourd = Mathf.Sqrt(Mathf.Pow(b.extents.x, 2) + Mathf.Pow(b.extents.y, 2));
        HookTrigger.returnedEvent.AddListener(() => isHookCasted = false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isHookCasted && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(hook, transform.position + transform.right * safeGourd, transform.rotation);
            isHookCasted = true;
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
