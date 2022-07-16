using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    int currentScene = 1;
    int totalSceneCount;
    // Start is called before the first frame update
    void Start()
    {
        totalSceneCount = SceneManager.sceneCountInBuildSettings;
        InvokeRepeating("ChangeScene", 3, 3);
        SceneManager.LoadScene(currentScene, LoadSceneMode.Additive);
    }

    void ChangeScene()
    {
        SceneManager.UnloadSceneAsync(currentScene);
        currentScene = (currentScene + 1) % totalSceneCount == 0 ? 1: currentScene + 1;
        SceneManager.LoadScene(currentScene, LoadSceneMode.Additive);

    }



    // Update is called once per frame
    void Update()
    {

    }
}
