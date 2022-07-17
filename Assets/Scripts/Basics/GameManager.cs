using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [SerializeField] private List<KeyCode> usableButtons;
    [SerializeField] private string minigameTitleText = "DESTORY";
    [SerializeField] private string actionText = "Fire:";


    private void Start()
    {
        uiManager.ChangeTitle(minigameTitleText);
        uiManager.ShowButtons(usableButtons, actionText);
    }

    public void FoundDice()
    {
        uiManager.RollDices();
    }
}
