using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [SerializeField] private List<KeyCode> usableButtons;
    [SerializeField] private string minigameTitleText = "DESTORY";
    [SerializeField] private string actionText = "Fire:";

    [SerializeField] private bool gameOver = false;

    [SerializeField] private bool hasTimer = true;
    [SerializeField] private bool loseAtTimerEnd = false;
    [SerializeField] private float levelTimerSeconds = 20.0f;
    private Coroutine timerEndCoroutine;

    private void Start()
    {
        gameOver = false;

        if (uiManager == null)
        {
            uiManager = GameObject.FindObjectOfType<UIManager>();
        }

        uiManager.ChangeTitle(minigameTitleText);
        uiManager.ShowButtons(usableButtons, actionText);

        SetupLevelTimer();
    }

    public void GetTimerSettings(bool hasTimer, bool loseAtTimerEnd, float timerVal)
    {
        this.hasTimer = hasTimer;
        this.loseAtTimerEnd = loseAtTimerEnd;
        this.levelTimerSeconds = timerVal;

        SetupLevelTimer();
    }

    private void SetupLevelTimer()
    {
        if (timerEndCoroutine != null)
        {
            StopCoroutine(timerEndCoroutine);
        }

        if (this.hasTimer)
        {
            uiManager.ShowTimer(levelTimerSeconds);
            timerEndCoroutine = StartCoroutine(TimerEndCoroutine());
        }
        else
        {
            uiManager.HideTimer();
        }
    }

    private IEnumerator TimerEndCoroutine()
    {
        yield return new WaitForSeconds(levelTimerSeconds);
        if (loseAtTimerEnd)
        {
            GameOver();
        }
        else
        {
            WinLevel();
        }
    }

    public void WinLevel()
    {
        if (!gameOver)
        {
            gameOver = true;
            StartCoroutine(SwitchLevel());
        }
    }

    // TODO - should probably move this and other Scene loading code here to our Scenemanager script
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

    public void FoundDice(DiceType diceType)
    {
        uiManager.RollDices(diceType);
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
