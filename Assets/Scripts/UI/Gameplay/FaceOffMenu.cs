using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FaceOffMenu : MonoBehaviour
{
    [SerializeField] GameObject PlayerPanel;
    [SerializeField] GameObject VoterPanel;
    [SerializeField] GameObject waitingPanel;

    public void showPlayerPanel()
    {
        this.gameObject.SetActive(true);
        PlayerPanel.SetActive(true);
    }

    public void showVotersPanel()
    {
        this.gameObject.SetActive(true);
        VoterPanel.SetActive(true);
    }
}
