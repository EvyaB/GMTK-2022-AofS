using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    const string enemyString = "Enemy";

    public GameObject player;
    public GameObject enemyGameobject;
    public int numberOfEnemies;
    public int speed;
    public int difficulty;

    List<Vector2> positions = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        positions.Add(new Vector2(0,0));
        for(int i=0; i< numberOfEnemies; i++)
            createEnemy(i);
    }

    private void createEnemy(int id)
    {
        GameObject enemy = Instantiate(enemyGameobject);
        enemy.name = enemyString + id;
        enemy.transform.position = positions[id];
        enemy.GetComponent<EnemyBehaviour>().target = player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
