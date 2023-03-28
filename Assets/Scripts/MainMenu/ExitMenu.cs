using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    [SerializeField] GameObject exitPanel;
    public void onClick_ExitButtonYes()
    {
        Application.Quit();
    }

    public void onClick_ExitButtonNo()
    {
        exitPanel.SetActive(false);
    }

}
