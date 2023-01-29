using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    [SerializeField] InputField nameInput;

    public void onCLick_ContinueButton()
    {
        if (nameInput != null)
        {
            PhotonNetwork.NickName = nameInput.text;
            gameObject.SetActive(false);    
        }
    }
}
