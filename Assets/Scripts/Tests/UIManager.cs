using UnityEngine;

public class UIManager : MonoBehaviour
{
    static bool isFirstScene = true;
    [SerializeField] GameObject ui;
    // Start is called before the first frame update
    void Start()
    {
        if (isFirstScene)
        {
            Instantiate(ui);
            DontDestroyOnLoad(ui);
            isFirstScene = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
