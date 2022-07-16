using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [SerializeField] private List<KeyCode> usableButtons;

    private void Start()
    {
        uiManager.ShowButtons(usableButtons);
    }

    public void FoundDice()
    {
        uiManager.RollDices();
    }
}
