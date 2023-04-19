using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyPolicy : MonoBehaviour
{
    [SerializeField] GameObject popup;
    [SerializeField] string privacyLink;
    public void OnCLick_AcceptButton()
    {
        PlayerPrefs.SetInt("Consent", 1);
        this.gameObject.SetActive(false);
    }

    public void OnCLick_PolicyButton() 
    {
        Application.OpenURL(privacyLink);
    }

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Consent",0) == 0)
        {
            popup.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

}
