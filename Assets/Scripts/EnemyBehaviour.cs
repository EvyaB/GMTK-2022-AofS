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

    bool isTryingToFindPlayer = true;
    string goingAroundObject = "";

    // Start is called before the first frame update
    void Start()
    {
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        transform.LookAt(gameObject.transform, direction);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (checkIfPlayerIsSeen())
        {
            isTargetVisible = true;
            transform.LookAt(target.transform.position);
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance >= MinDist)
            {
                isTryingToFindPlayer = false;
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;

                if (distance <= MaxDist)
                {
                    //Shoot

                }
            }
        }
        else
        {
            isTargetVisible = false;
            if (isTryingToFindPlayer == false)
                isTryingToFindPlayer = true;
            //Try to find player
            transform.LookAt(target.transform.position);
            if (Vector2.Distance(transform.position, target.transform.position) >= MinDist && isTryingToFindPlayer)
            {
                Vector3 newDirection = findNewDirection();
                if (newDirection.x != 0 || newDirection.y != 0)
                    transform.position += newDirection * MoveSpeed * Time.deltaTime;
                else
                    transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            }
        }
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
            if (Physics.Raycast(transform.position, rayDirection, out hit))
            {
                // enemy can see the player!
                if (goingAroundObject == "")
                    goingAroundObject = hit.transform.name;
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
            if (Physics.Raycast(transform.position, rayDirection2, out hit2))
            {
                // enemy can see the player!
                if (goingAroundObject == "")
                    goingAroundObject = hit.transform.name;
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
        }
        return new Vector2(0, 0);
    }

    private bool checkIfPlayerIsSeen()
    {
        RaycastHit hit, hit2;
        Vector3 positionOfRightCorener = transform.position + transform.GetComponent<BoxCollider>().size / 2;
        var rayDirection = -target.transform.position + positionOfRightCorener;
        Debug.DrawRay(positionOfRightCorener, rayDirection);
        if (Physics.Raycast(positionOfRightCorener, rayDirection, out hit))
        {
            if (hit.transform.name == target.name)
            {
                // enemy can see the player!
                Vector3 positionOfLeftCorener = transform.position - GetComponent<BoxCollider>().size / 2;
                var rayDirection2 = - target.transform.position + positionOfLeftCorener;
                Debug.DrawRay(positionOfLeftCorener, rayDirection2);
                if (Physics.Raycast(positionOfLeftCorener, rayDirection2, out hit2))
                    if (hit2.transform.name == target.name)
                    {
                        return true;
                    }
            }
        };
        // there is something obstructing the view
        return false;
    }
}
