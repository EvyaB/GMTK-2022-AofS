using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [SerializeField] private List<KeyCode> usableButtons;
    [SerializeField] private string minigameTitleText = "DESTORY";
    [SerializeField] private string actionText = "Fire:";

    [SerializeField] private bool gameOver = false;

    private void Start()
    {
        gameOver = false;

        if (uiManager == null)
        {
            uiManager = GameObject.FindObjectOfType<UIManager>();
        }

        uiManager.ChangeTitle(minigameTitleText);
        uiManager.ShowButtons(usableButtons, actionText);
    }

    public void WinLevel()
    {
        if (!gameOver)
        {
            gameOver = true;
            StartCoroutine(SwitchLevel());
        }
    }

    private IEnumerator SwitchLevel()
    {
        uiManager.ShowGameWon();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(uiManager.GetNextLevelId());
    }

    public void InformPlayerDeath(GameObject gameObject)
    {
        GameOver();
    }

    public void FoundDice()
    {
        uiManager.RollDices();
    }
    
    private void GameOver()
    {
        gameOver = true;
        uiManager.ShowGameOver();
    }

    private void GameWon()
    {
        gameOver = true;
        uiManager.ShowGameWon();
    }

    public void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneManager.LoadScene(0);
        }
    }
}
