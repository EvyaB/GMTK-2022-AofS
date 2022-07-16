using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<CubeUi> cubes;

    [SerializeField]
    private GameObject cubePrefab;

    public void Start()
    {
        //StartCoroutine(TestDiceRolls());
    }

    public void AddCube()
    {
        var obj = Instantiate(cubePrefab);
        cubes.Add(obj.GetComponent<CubeUi>());
    }

    public void RollDices()
    {
        foreach (CubeUi cube in cubes)
        {
            cube.RollDice();
        }
    }

    IEnumerator TestDiceRolls()
    {
        while (true)
        {
            RollDices();
            yield return new WaitForSeconds(2);
        }
    }
}
