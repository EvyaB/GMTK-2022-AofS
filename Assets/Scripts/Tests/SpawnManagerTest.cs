using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerTest : MonoBehaviour
{
    public GameObject spawnMe;
    public GameObject spawnMeALot;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnLoop", 3, 3);
    }

    void SpawnLoop()
    {
        SpawnManager.Instance.SpawnRandomPointInBound(spawnMe, 3, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
