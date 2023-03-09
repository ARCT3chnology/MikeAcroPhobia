using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Connectivity : MonoBehaviourPunCallbacks
{

    [SerializeField] InputField nameInput;
    [SerializeField] Text placeholderText;
    [SerializeField] Button PlayButton;
    [SerializeField] bool connected;
    private void Start()
    {
        Debug.Log("Connecting to server");
        //PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        
        connected = PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.EnableCloseConnection = true;
        placeholderText.text = GameSettings.NickName;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        GameSettings.ConnectedtoMaster = true;
        PlayButton.interactable = true;
        
        //base.OnConnectedToMaster();
    }



    public void SettingNickName_OnClick()
    {
        PhotonNetwork.NickName = nameInput.text;
        GameSettings.NickName = nameInput.text;
        MenuManager.Instance.OpenMenu(menuName.PlayPanel);
    }

    public void onCLick_PlayButton()
    {
        SceneManager.LoadScene(1);
    }
}
