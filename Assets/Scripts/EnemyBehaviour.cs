using System;
using UnityEngine;


public class EnemyBehaviour : MonoBehaviour
{
    public GameObject target;
    public int MoveSpeed = 1;
    public int MinDist = 1;
    public int MaxDist = 15;
    public GameObject bulletGameObject;
    public bool isTargetVisible = false;
    public bool foundNewDirection = false;

    bool isTryingToFindPlayer = true;
    string goingAroundObject = "";
    const float MAX_DELAY = 0.3f;
    public float shotDelay = MAX_DELAY;
    float timeSincePrevoiusShot = 0;
    BoxCollider bc;
    Rigidbody rb;

    Shooter shooter;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        shooter = GetComponent<Shooter>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtTarget();
        isTargetVisible = checkIfPlayerIsSeen();
        if (isTargetVisible)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance >= MinDist)
            {
                isTryingToFindPlayer = false;
                //transform.position += transform.right * MoveSpeed * Time.deltaTime;
                //rb.AddForce(transform.right * MoveSpeed);
                rb.velocity += transform.right;

                if (distance <= MaxDist && timeSincePrevoiusShot <= 0)
                {
                    //Shoot
                    Vector3 direction = Vector3.right;
                    direction.z = 0;
                    Vector3 position = transform.position;
                    position.z = 0;
                    shooter.Shoot(position, transform.rotation, direction);
                    timeSincePrevoiusShot = shotDelay;
                }
            }
        }
        else
        {
            isTryingToFindPlayer = true;

            //Try to find player
            if (Vector2.Distance(transform.position, target.transform.position) >= MinDist)
            {
                var newDirection = findNewDirection();
                if (newDirection.HasValue)
                {
                    //transform.position += newDirection.Value.normalized * MoveSpeed * Time.deltaTime;
                    rb.velocity = (newDirection.Value.normalized + rb.velocity) / 2;
                }
                else
                {
                    //transform.position += transform.right * MoveSpeed * Time.deltaTime;
                    rb.velocity += transform.right;
                }
            }
        }
        Wallphobia();
        timeSincePrevoiusShot -= Time.deltaTime;
        Debug.DrawRay(transform.position, rb.velocity, Color.green, 0.3f, false);
        rb.velocity = rb.velocity.normalized * MoveSpeed;
    }

    private void LookAtTarget()
    {
        //transform.LookAt(target.transform.position);
        var targetDir = target.transform.position - transform.position;
        targetDir.Normalize();
        float zAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);
    }

    bool findNewDirectionByAngle(Vector3 directionToPlayer, float angle, out Vector3 rayDirection, Color color, float rayLength = 6f)
    {
        RaycastHit hit;
        rayDirection = Quaternion.Euler(0, 0, angle) * directionToPlayer;

        Debug.DrawRay(transform.position, rayDirection, color, 0.1f, false);
        if (Physics.Raycast(transform.position, rayDirection, out hit, rayLength))
        {
            //Debug.DrawRay(transform.position, hit.transform.position - transform.position, Color.cyan, 0.1f, false);

            // Ignore arena borders 
            return hit.transform.GetComponent<ArenaBorder>() != null;
        }
        else
        {
            //Debug.DrawRay(transform.position, rayDirection, Color.yellow, 0.1f, false);
            return true;
        }
    }

    private Vector3? findNewDirection()
    {
        Vector3 directionToPlayer = target.transform.position - transform.position;
        Vector3 res;
        for (float i = 0; i < 10; i++)
        {
            if (findNewDirectionByAngle(directionToPlayer, i * 20, out res, Color.clear))
            {
                foundNewDirection = true;
                return res;
            }
            if (findNewDirectionByAngle(directionToPlayer, -i * 20, out res, Color.clear))
            {
                foundNewDirection = true;
                return res;
            }
        }
        foundNewDirection = false;
        return null;
    }

    private void Wallphobia()
    {
        Vector3 res;
        for (float i = 0; i < 72; i++)
        {
            if (!findNewDirectionByAngle(target.transform.position - transform.position, i * 5, out res, Color.clear, 1f))
            {
                rb.velocity += res * -1;
                break;
            }
        }
    }

    bool checkIfPlayerIsSeenOnSide(Vector3 origin, Color color)
    {
        RaycastHit[] hits;
        RaycastHit hit;
        var rayDirection = target.transform.position - origin;
        Debug.DrawRay(origin, rayDirection, color, 0.1f, false);
        hits = Physics.RaycastAll(origin, rayDirection);
        if (hits.Length != 0)
        {
            int i;
            hit = hits[0];
            Array.Sort(hits, (x, y) =>
            {
                var distanceX = transform.position - x.transform.position;
                var distancey = transform.position - y.transform.position;
                return distanceX.magnitude.CompareTo(distancey.magnitude);
            });
            for (i = 0; i < hits.Length; i++)
            {
                if (!(hits[i].transform.name == transform.name || hits[i].transform.name.StartsWith("Border")))
                {
                    hit = hits[i];
                    break;
                }
            }
            if (i == hits.Length)
            {
                return false;
            }
            if (hit.transform.name == target.name)
            {
                return true;
            }
        }
        return false;
    }

    private bool checkIfPlayerIsSeen()
    {
        Vector3 origin = transform.position + (bc.bounds.extents.x / 2 * transform.right + bc.bounds.extents.y / 2 * transform.up);
        if (checkIfPlayerIsSeenOnSide(origin, Color.red))
        {
            // enemy can see the player!
            origin = transform.position + (bc.bounds.extents.x / 2 * transform.right + bc.bounds.extents.y / 2 * -transform.up);
            if (checkIfPlayerIsSeenOnSide(origin, Color.red))
            {
                return true;
            }
        }
        // there is something obstructing the view
        return false;
    }
}
