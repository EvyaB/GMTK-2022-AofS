using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cubeText;
    [SerializeField] private int CubeFacesCount = 6;

    [SerializeField] private int currentValue = 1;

    public void RollDice()
    {
        int rollVal = Random.Range(1, CubeFacesCount);
        cubeText.text = rollVal.ToString();
    }

    public int GetCurrentValue()
    {
        return currentValue;
    }
}
