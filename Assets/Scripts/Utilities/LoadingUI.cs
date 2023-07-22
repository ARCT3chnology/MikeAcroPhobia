using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using static Connectivity;

public class LoadingUI : MonoBehaviourPunCallbacks
{
    [SerializeField] Slider LoadingBar;
    [SerializeField] bool connected;


    private void Awake()
    {
        LoadingBar.value = 0;
        if (PlayerStatsMenu.Instance!=null)
        {
            PlayerStatsMenu.Instance.setPlayerStatsmenuState(false);
        }
    }

    private void Start()
    {
        Debug.Log("Connecting to server" + PhotonNetwork.NetworkClientState);
        //PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        if (!connected || PhotonNetwork.NetworkClientState == ClientState.Disconnected)
        {
            connected = PhotonNetwork.ConnectUsingSettings();
            if (PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
            {
                GameSettings.ConnectedtoMaster = true;
                LoadingBar.value = 1;
                Invoke(nameof(SHowMainMenu), 1.0f);
            }
        }
        //else if(PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
        //{
        //    GameSettings.ConnectedtoMaster = true;
        //    LoadingBar.value = 1;
        //    Invoke(nameof(SHowMainMenu), 1.0f);
        //}
        Debug.Log("Connecting to server" + PhotonNetwork.NetworkClientState);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.EnableCloseConnection = true;
        Debug.Log(connected);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        GameSettings.ConnectedtoMaster = true;
        LoadingBar.value = 1;
        Invoke(nameof(SHowMainMenu), 1.0f);
        //SHowMainMenu();

    }
    public sex Gender;

    private void SHowMainMenu()
    {
        if (PlayerPrefs.GetInt("Gender") == 0)
        {
            Gender = sex.male;
        }
        else
        {
            Gender = sex.female;
        }
        MenuManager.Instance.OpenMenu(menuName.PlayPanel);
        PlayerStatsMenu.Instance.setName();
        PlayerStatsMenu.Instance.setLevel();
        PlayerStatsMenu.Instance.setImageProfile(Gender);
        PlayerStatsMenu.Instance.setExperienceSlider();
        PlayerStatsMenu.Instance.UpdateStarsText();
        PlayerStatsMenu.Instance.setPlayerStatsmenuState(true);

        this.gameObject.SetActive(false);

    }

    public override void OnConnected()
    {
        Debug.Log("Connected");
        //PlayButton.interactable = false;
        base.OnConnected();
    }

}
