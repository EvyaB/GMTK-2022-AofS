using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDisappear : MonoBehaviour
{
    public float timeToTogglePlatform = 2f;
    private float currentTime = 0;
    public float interval = 3;
    private bool isGone = false;
    // Start is called before the first frame update
    void Start()
    {
        isGone = false;
    }

    IEnumerator StartDisappearing()
    {
        isGone = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(interval);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        isGone = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > timeToTogglePlatform)
        {
            currentTime = 0;
            togglePlatform();
        }

//if (!isGone)
    //        StartCoroutine(StartDisappearing());
    }

    void togglePlatform()
    {
        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
        GetComponent<BoxCollider>().enabled = !GetComponent<BoxCollider>().enabled;
    }
}
