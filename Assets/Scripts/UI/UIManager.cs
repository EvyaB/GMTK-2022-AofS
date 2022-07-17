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

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject gameWonPanel;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);
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

    public void ChangeTitle(string newTitle)
    {
        stickNote.ChangeTitle(newTitle);
    }

    public int GetNextLevelId()
    {
        return cubes[0].GetCurrentValue();
    }

    public void ShowButtons(List<KeyCode> buttons, string actionText)
    {
        buttonsPanel.ShowButtons(buttons, actionText);
    }

    internal void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
    internal void ShowGameWon()
    {
        gameWonPanel.SetActive(true);
    }

    IEnumerator TestDiceRolls()
    {
        while (true)
        {
            RollDices();
            yield return new WaitForSeconds(2);
        }
    }

    internal void ShowTimer(float levelTimerSeconds)
    {
        stickNote.SetupTimer(levelTimerSeconds);
    }

    internal void HideTimer()
    {
        stickNote.HideTimer();
    }
}
