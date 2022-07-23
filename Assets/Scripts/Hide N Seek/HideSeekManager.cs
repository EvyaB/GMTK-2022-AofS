using System.Collections;
using TMPro;
using UnityEngine;

public class HideSeekManager : MonoBehaviour
{
    [SerializeField] GameObject countDownDigit;
    [SerializeField] GameObject hideTitle;
    GameObject canvas;
    GameManager gm;
    EnemyBehaviour[] enemies;
    public int hideTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.GetChild(0).gameObject;
        gm = FindObjectOfType<GameManager>();
        enemies = FindObjectsOfType<EnemyBehaviour>();

        SetMinigameActive(false);
        StartCoroutine(CountDown(hideTime));

    }

    void SetMinigameActive(bool active)
    {
        foreach (var e in enemies)
        {
            e.enabled = active;
        }
        gm.gameObject.SetActive(active);
    }

    void StartSeek()
    {
        Destroy(hideTitle);
        SetMinigameActive(true);
    }

    IEnumerator CountDown(int countdownLength)
    {
        for (int i = countdownLength; i >= 0; i--)
        {
            var d = Instantiate(countDownDigit, canvas.transform);
            d.GetComponent<TMP_Text>().text = i.ToString();
            yield return new WaitForSeconds(1);
            Destroy(d);
        }

        StartSeek();
    }
}
