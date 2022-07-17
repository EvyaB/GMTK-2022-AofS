using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StickNoteInfo : MonoBehaviour
{
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI titleLabel;

    private float timeLeft = 0f;

    public void Update()
    {
        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            timeLeft = 0.0f;
        }

        timer.text = timeLeft.ToString("0.0");
    }

    public void ChangeTitle(string newTitle)
    {
        titleLabel.text = newTitle;
    }

    internal void HideTimer()
    {
        timerPanel.SetActive(false);
    }

    internal void SetupTimer(float levelTimerSeconds)
    {
        timeLeft = levelTimerSeconds;
        timerPanel.SetActive(true);
        timer.text = levelTimerSeconds.ToString("0.0");
    }
}
