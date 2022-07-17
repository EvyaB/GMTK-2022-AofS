using System;
using System.Collections;
using UnityEngine;


public class EnemyBehaviour : MonoBehaviour
{
    public GameObject target;
    public int MoveSpeed = 1;
    public int MinDist = 1;
    public int MaxDist = 15;
    public GameObject bulletGameObject;
    public bool isTargetVisible = false;

    bool isTryingToFindPlayer = true;
    string goingAroundObject = "";
    const float MAX_DELAY = 0.3f;
    public float shotDelay = MAX_DELAY;
    float timeSincePrevoiusShot = 0;
    BoxCollider bc;

    Shooter shooter;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        //Vector2 direction = target.transform.position - transform.position;
        //direction.Normalize();
        //transform.LookAt(gameObject.transform, direction);
        shooter = GetComponent<Shooter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtTarget();
        if (checkIfPlayerIsSeen())
        {
            isTargetVisible = true;
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance >= MinDist)
            {
                isTryingToFindPlayer = false;
                transform.position += transform.right * MoveSpeed * Time.deltaTime;

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
            isTargetVisible = false;
            if (isTryingToFindPlayer == false)
            {
                isTryingToFindPlayer = true;
            }
            //Try to find player
            if (Vector2.Distance(transform.position, target.transform.position) >= MinDist && isTryingToFindPlayer)
            {
                Vector3 newDirection = findNewDirection();
                if (newDirection.x != 0 || newDirection.y != 0)
                {
                    transform.position += newDirection * MoveSpeed * Time.deltaTime;
                }
                else
                {
                    transform.position += transform.right * MoveSpeed * Time.deltaTime;
                }
            }
        }
        timeSincePrevoiusShot -= Time.deltaTime;
    }

    private void LookAtTarget()
    {
        //transform.LookAt(target.transform.position);
        var targetDir = target.transform.position - transform.position;
        targetDir.Normalize();
        float zAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);
    }

    void findNewDirectionHelp()
    {

    }

    private Vector2 findNewDirection()
    {
        Vector3 directionToPlayer = target.transform.position - transform.position;
        for (float i = 0; i < 20; i++)
        {
            RaycastHit hit;
            directionToPlayer.x += (float)Math.Sin(10.0f * i);
            directionToPlayer.y += (float)Math.Cos(10.0f * i);
            directionToPlayer.Normalize();
            directionToPlayer.z = 0;
            var rayDirection = directionToPlayer;
            if (Physics.Raycast(transform.position, rayDirection, out hit, 6.0f))
            {
                // enemy can see the player!
                if (goingAroundObject == "")
                {
                    goingAroundObject = hit.transform.name;
                }
                else if (goingAroundObject != hit.transform.name)
                {
                    goingAroundObject = "";
                    return directionToPlayer;
                }
            }
            else
            {
                return directionToPlayer;
            }
            RaycastHit hit2;
            directionToPlayer.x += (float)Math.Sin(-10.0f * i);
            directionToPlayer.y += (float)Math.Cos(-10.0f * i);
            directionToPlayer.Normalize();
            directionToPlayer.z = 0;
            var rayDirection2 = directionToPlayer;
            if (Physics.Raycast(transform.position, rayDirection2, out hit2, 6.0f))
            {
                // enemy can see the player!
                if (goingAroundObject == "")
                {
                    goingAroundObject = hit2.transform.name;
                }
                else if (goingAroundObject != hit2.transform.name)
                {
                    goingAroundObject = "";
                    return directionToPlayer;
                }
            }
            else
            {
                return directionToPlayer;
            }
        }
        return new Vector2(0, 0);
    }

    private bool checkIfPlayerIsSeen()
    {
        RaycastHit[] hits, hits2;
        RaycastHit hit, hit2;
        Vector3 positionOfRightCorener = transform.position + (bc.bounds.extents.x * transform.right + bc.bounds.extents.y * transform.up);
        var rayDirection = target.transform.position - positionOfRightCorener;
        Debug.DrawRay(positionOfRightCorener, rayDirection, Color.red, 0.1f, false);
        hits = Physics.RaycastAll(positionOfRightCorener, rayDirection);
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
                // enemy can see the player!
                Vector3 offset = GetComponent<BoxCollider>().size / 2;
                Vector3 positionOfLeftCorener = transform.position + (bc.bounds.extents.x * transform.right + bc.bounds.extents.y * -transform.up);
                var rayDirection2 = target.transform.position - positionOfLeftCorener;
                Debug.DrawRay(positionOfLeftCorener, rayDirection2);
                hits2 = Physics.RaycastAll(positionOfLeftCorener, rayDirection2);
                Array.Sort(hits2, (x, y) =>
                {
                    var distanceX = transform.position - x.transform.position;
                    var distancey = transform.position - y.transform.position;
                    return distanceX.magnitude.CompareTo(distancey.magnitude);
                });
                if (hits2.Length != 0)
                {
                    int i2 = 0;
                    hit2 = hits2[0];
                    for (i2 = 0; i2 < hits2.Length; i2++)
                    {
                        if (!(hits2[i2].transform.name == transform.name || hits2[i2].transform.name.StartsWith("Border")))
                        {
                            hit2 = hits2[i2];
                            break;
                        }
                    }
                    if (i2 == hits2.Length)
                    {
                        return false;
                    }

                    if (hit2.transform.name == target.name)
                    {
                        return true;
                    }
                }
            }
        };
        // there is something obstructing the view
        return false;
    }
}
