using System;
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

    [SerializeField]
    private StickNoteInfo stickNote;

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

    public void ChangeTitle(string newTitle)
    {
        stickNote.ChangeTitle(newTitle);
    }

    public void ShowButtons(List<KeyCode> buttons, string actionText)
    {
        buttonsPanel.ShowButtons(buttons, actionText);
    }

    internal void ShowGameOver()
    {
        //throw new NotImplementedException();
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
