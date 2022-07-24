using UnityEngine;

public class SpawnManager : MonoBehaviour
{   

    [SerializeField] int maxSpawnAttempts = 50;
    [SerializeField] bool debug = false;

    public static SpawnManager Instance { get; private set; }
    private void Start()
    {
        var rend = GetComponent<SpriteRenderer>();
        rend.color = new Color(rend.color.r, rend.color.b, rend.color.g, debug ? 0.5f : 0);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            throw new System.Exception(GetType().Name + ": Trying to create second instacne of singleton");
        }
        Instance = this;
    }

    public bool IsOccupied(Vector3 center, Vector3 extent)
    {
        var colliders = Physics.OverlapBox(center, extent);
        return colliders.Length == 0;

    }

    Vector3 randomPointInBounds(Bounds b, Vector3 targetExtent)
    {
        var xMin = b.center.x - b.extents.x + targetExtent.x;
        var xMax = b.center.x + b.extents.x - targetExtent.x;
        var yMin = b.center.y - b.extents.y + targetExtent.y;
        var yMax = b.center.y + b.extents.y - targetExtent.y;

        return new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
    }

    public GameObject[] SpawnRandomPointInBound(GameObject obj, uint count = 1, float safeGroudDistance = 0)
    {
        var spawnies = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            spawnies[i] = SpawnSingleRandomPointInBound(obj, safeGroudDistance);
        }
        return spawnies;
    }

    GameObject SpawnSingleRandomPointInBound(GameObject obj, float safeGroudDistance)
    {
        var spawnBounds = GetComponent<SpriteRenderer>().bounds;

        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            var bc = obj.GetComponent<BoxCollider>();
            var rndPoint = randomPointInBounds(spawnBounds, bc.bounds.extents);

            // No other object in bounds of obj collider aside from obj 
            if (IsOccupied(rndPoint, bc.bounds.extents + (Vector3.one * safeGroudDistance)))
            {
                return Instantiate(obj, rndPoint, obj.transform.rotation);
            }
        }

        throw new System.Exception("SpawnManager: Cound not instantiate '" + obj.name + "' after " + maxSpawnAttempts + " tries.");
    }
}
