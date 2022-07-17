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
    [SerializeField] private GameObject movePanel;
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private TextMeshProUGUI actionLabel;

    private bool hasAction = true;
    private bool canMove = true;

    public void ShowButtons(List<KeyCode> keyCodes, string actionText)
    {
        DisableAllButtons();

        foreach (KeyCode keyCode in keyCodes)
        {
            switch (keyCode)
            {
                case KeyCode.Mouse0:
                    buttonLeftMouse.SetActive(true);
                    hasAction = true;
                    break;
                case KeyCode.W:
                    buttonW.SetActive(true);
                    canMove = true;
                    break;
                case KeyCode.A:
                    buttonA.SetActive(true);
                    canMove = true;
                    break;
                case KeyCode.S:
                    buttonS.SetActive(true);
                    canMove = true;
                    break;
                case KeyCode.D:
                    buttonD.SetActive(true);
                    canMove = true;
                    break;
                default:
                    break;
            }

            movePanel.SetActive(canMove);
            actionPanel.SetActive(hasAction);
            actionLabel.text = actionText;
        }
    }

    public void DisableAllButtons()
    {
        hasAction = false;
        canMove = false;

        buttonLeftMouse.SetActive(false);
        buttonW.SetActive(false);
        buttonA.SetActive(false);
        buttonS.SetActive(false);
        buttonD.SetActive(false);

        movePanel.SetActive(false);
        actionPanel.SetActive(false);
    }
}
