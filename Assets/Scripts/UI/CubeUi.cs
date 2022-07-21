using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CubeUi : MonoBehaviour
{
    [SerializeField] private GameObject cubeText;
    [SerializeField] private int CubeFacesCount = 6;

    [SerializeField] private int currentValue = 1;
    [SerializeField] private DiceType diceType;

    [SerializeField] private Sprite platformMinigameIcon;
    [SerializeField] private Sprite tankMinigameIcon;
    [SerializeField] private Sprite hideMinigameIcon;

    public DiceType GetDiceType() { return diceType; }

    public void RollDice(int maxVal)
    {
        int rollVal = Random.Range(1, maxVal + 1); // roll within [1, maxVal]

        if (diceType == DiceType.Minigame)
        {
            HandleMiniGameIcons(rollVal);
        }
        else
        {
            cubeText.GetComponent<TextMeshProUGUI>().text = rollVal.ToString();
        }
    }

    private void HandleMiniGameIcons(int rollVal)
    {
        switch (rollVal)
        {
            case 1:
            case 4:
                cubeText.GetComponent<Image>().sprite = platformMinigameIcon;
                break;
            case 2:
            case 5:
                cubeText.GetComponent<Image>().sprite = tankMinigameIcon;
                break;
            case 3:
            case 6:
                cubeText.GetComponent<Image>().sprite = hideMinigameIcon;
                break;
            default:
                Debug.LogError("Got invalid value");
                break;
        }
    }

    public int GetCurrentValue()
    {
        return currentValue;
    }
}
