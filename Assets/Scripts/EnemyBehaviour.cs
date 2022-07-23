using System;
using UnityEngine;


public class EnemyBehaviour : MonoBehaviour
{
    public GameObject target;
    public int MoveSpeed = 1;
    public bool isTargetVisible = false;
    public bool wasLastDirectionPositive = false;
    BoxCollider bc;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtTarget();
        isTargetVisible = checkIfPlayerIsSeen();
        if (isTargetVisible)
        {
            rb.velocity += transform.right;
        }
        else
        {
            var newDirection = findNewDirection();
            if (newDirection.HasValue)
            {
                //transform.position += newDirection.Value.normalized * MoveSpeed * Time.deltaTime;
                rb.velocity = (newDirection.Value.normalized + rb.velocity.normalized);
            }
            else
            {
                //transform.position += transform.right * MoveSpeed * Time.deltaTime;
                rb.velocity += transform.right;
            }
        }
        Wallphobia();
        rb.velocity = rb.velocity.normalized * MoveSpeed;
        Debug.DrawRay(transform.position, rb.velocity, Color.yellow, 0.3f, false);
    }

    private void LookAtTarget()
    {
        var targetDir = target.transform.position - transform.position;
        targetDir.Normalize();
        float zAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);
    }

    bool findNewDirectionByAngle(Vector3 directionToPlayer, float angle, out Vector3 rayDirection, Color color, float rayLength = 2f)
    {
        RaycastHit hit;
        rayDirection = Quaternion.Euler(0, 0, angle) * directionToPlayer;

        Debug.DrawRay(transform.position, rayDirection.normalized * rayLength, color, 0.1f, false);
        if (Physics.Raycast(transform.position, rayDirection.normalized, out hit, rayLength))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private Vector3? findNewDirection()
    {
        Vector3 directionToPlayer = target.transform.position - transform.position;
        var dir = directionToPlayer;
        const int incAngle = 9;
        const int searchSteps = 10;
        const int rayLength = 2;
        Vector3 resPositive = new(), resNegative = new();
        bool neg = false, pos = false;
        for (float i = 0; i < searchSteps && !wasLastDirectionPositive; i++)
        {
            if (findNewDirectionByAngle(dir.normalized, -i * incAngle, out resNegative, Color.red, rayLength))
            {
                neg = true;
                break;
            }
        }
        for (float i = 0; i < searchSteps && (wasLastDirectionPositive || !neg); i++)
        {
            if (findNewDirectionByAngle(dir, i * incAngle, out resPositive, Color.cyan, rayLength))
            {
                pos = true;
                break;
            }
        }
        if (neg)
        {
            wasLastDirectionPositive = false;
            return resNegative;
        }
        else if (pos)
        {
            wasLastDirectionPositive = true;
            return resPositive;
        }
        wasLastDirectionPositive = !wasLastDirectionPositive;
        return null;
    }

    private void Wallphobia()
    {
        Vector3 res;
        for (float i = 0; i < 72; i++)
        {
            var safeGourd = Mathf.Sqrt(Mathf.Pow(bc.bounds.extents.x, 2) + Mathf.Pow(bc.bounds.extents.y, 2));
            if (!findNewDirectionByAngle(transform.right, i * 5, out res, Color.clear, safeGourd))
            {
                rb.velocity += res.normalized * -1;
                return;
            }
        }
    }

    bool checkIfPlayerIsSeenOnSide(Vector3 origin, Color color)
    {
        RaycastHit[] hits;
        RaycastHit hit;
        var rayDirection = target.transform.position - origin;
        //Debug.DrawRay(origin, rayDirection, color, 0.1f, false);
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
