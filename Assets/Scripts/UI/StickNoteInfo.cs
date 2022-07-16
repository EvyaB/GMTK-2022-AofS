using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StickNoteInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI titleLabel;

    bool coroutinesRunning = false;
    int currentSeconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
    }

    public void StopTimer()
    {
        StopAllCoroutines();
        coroutinesRunning = false;
    }
    public void StartTimer()
    {
        if (!coroutinesRunning)
        {
            StartCoroutine(UpdateTimer());
            coroutinesRunning = true;
        }
    }

    IEnumerator UpdateTimer()
    {
        while (true)
        {
            currentSeconds++;

            string minutes = Mathf.Floor(currentSeconds / 60).ToString("00");
            string seconds = Mathf.RoundToInt(currentSeconds % 60).ToString("00");

            timer.text = minutes + ":" + seconds;

            yield return new WaitForSeconds(1);
        }
    }

    public void ChangeTitle(string newTitle)
    {
        titleLabel.text = newTitle;
    }
}
