using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<CubeUi> cubes;

    [SerializeField]
    private GameObject cubePrefab;

    [SerializeField]
    private ButtonsPanel buttonsPanel;

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

    public void ShowButtons(List<KeyCode> buttons, string actionText)
    {
        buttonsPanel.ShowButtons(buttons, actionText);
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
