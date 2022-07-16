using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonsPanel : MonoBehaviour
{
    [SerializeField] private GameObject buttonLeftMouse;
    [SerializeField] private GameObject buttonW;
    [SerializeField] private GameObject buttonA;
    [SerializeField] private GameObject buttonS;
    [SerializeField] private GameObject buttonD;

    public void ShowButtons(List<KeyCode> keyCodes)
    {
        DisableAllButtons();

        foreach (KeyCode keyCode in keyCodes)
        {
            switch (keyCode)
            {
                case KeyCode.Mouse0:
                    buttonLeftMouse.SetActive(true);
                    break;
                case KeyCode.W:
                    buttonW.SetActive(true);
                    break;
                case KeyCode.A:
                    buttonA.SetActive(true);
                    break;
                case KeyCode.S:
                    buttonS.SetActive(true);
                    break;
                case KeyCode.D:
                    buttonD.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    public void DisableAllButtons()
    {
        buttonLeftMouse.SetActive(false);
        buttonW.SetActive(false);
        buttonA.SetActive(false);
        buttonS.SetActive(false);
        buttonD.SetActive(false);
    }
}
