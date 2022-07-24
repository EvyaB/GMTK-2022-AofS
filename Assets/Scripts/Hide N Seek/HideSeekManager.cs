using System.Linq;
using System.Collections;
using TMPro;
using UnityEngine;

public class HideSeekManager : GameManager
{
    [SerializeField] GameObject countDownDigit;
    [SerializeField] GameObject enemyPrefab;
    GameObject canvas;
    EnemyBehaviour[] enemies;
    public int hideTime = 5;
    public uint enemiesCount = 3;
    // Start is called before the first frame update
    new void Start()
    {
        canvas = transform.GetChild(0).gameObject;
        enemies = SpawnManager.Instance.SpawnRandomPointInBound(enemyPrefab, enemiesCount, 0.5f)
            .Select((e) => e.GetComponent<EnemyBehaviour>())
            .ToArray();
        enemies.ToList().ForEach((e) => e.target = FindObjectOfType<PlayerControlerTopDown>().gameObject);

        SetEnemiesActive(false);
        StartCoroutine(SeekCountDown(hideTime));
    }

    void SetEnemiesActive(bool active)
    {
        foreach (var e in enemies)
        {
            e.enabled = active;
        }
    }

    void StartSeek()
    {
        SetEnemiesActive(true);
        base.Start();
    }

    IEnumerator SeekCountDown(int countdownLength)
    {
        const int START_END_LABELS = 2;
        countdownLength += START_END_LABELS;

        for (int i = countdownLength; i >= 0; i--)
        {
            var d = Instantiate(countDownDigit, canvas.transform);
            var text = i.ToString();
            var displayTime = 1;
            if (i == countdownLength)
            {
                text = "HIDE\nBEGIN\nNOW";
                displayTime = 2;
            }
            else if (i == 0)
            {
                text = "SEEK\nBEGIN";
                displayTime = 2;
            }
            d.GetComponent<TMP_Text>().text = text;
            yield return new WaitForSeconds(displayTime);
            Destroy(d);
        }

        StartSeek();
    }

}
