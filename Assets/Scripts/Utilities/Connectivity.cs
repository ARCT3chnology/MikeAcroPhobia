using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class Connectivity : MonoBehaviourPunCallbacks
{

    [SerializeField] InputField nameInput;
    [SerializeField] Text placeholderText;
    [SerializeField] Button PlayButton;
    [SerializeField] bool connected;
    [SerializeField] Menu LoginMenu;
    [SerializeField] Menu SplashMenu;
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
        Debug.Log("Connecting to server");
        //PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        if (!connected || PhotonNetwork.NetworkClientState == ClientState.Disconnected)
        {
            connected = PhotonNetwork.ConnectUsingSettings();
        }
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.EnableCloseConnection = true;
        Debug.Log(connected);
        //placeholderText.text = GameSettings.NickName;

        //if(GameSettings.NickName != "Player")
        //{
        //    MenuManager.Instance.CloseMenu(LoginMenu);
        //    MenuManager.Instance.OpenMenu(menuName.PlayPanel);
        //}
        if (PhotonNetwork.IsConnected)
        {
            PlayButton.interactable = true;
            if (connected)
            {
            }
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        switch (cause)
        {
            case DisconnectCause.None:
                {
                    Debug.Log(cause);
                    break;
                }
            case DisconnectCause.ExceptionOnConnect:
                {
                    Debug.Log(cause);
                    break;
                }
            case DisconnectCause.DnsExceptionOnConnect:
                {
                    Debug.Log(cause);

                    break; 
                }
            case DisconnectCause.ServerAddressInvalid:
                {
                    Debug.Log(cause);

                    break; 
                }
            case DisconnectCause.Exception:
                {
                    Debug.Log(cause);

                    break;
                }
            case DisconnectCause.ServerTimeout:
                {
                    Debug.Log(cause);
                    PlayButton.interactable = false;
                    connected = false;
                    StartCoroutine(StartReconnecting());
                    PhotonNetwork.ReconnectAndRejoin();
                    break; 
                }
            case DisconnectCause.ClientTimeout:
                {
                    Debug.Log(cause);
                    PlayButton.interactable = false;
                    connected = false;
                    StartCoroutine(StartReconnecting());
                    PhotonNetwork.ReconnectAndRejoin();
                    break; 
                }
            case DisconnectCause.DisconnectByServerLogic:
                Debug.Log(cause);

                break;
            case DisconnectCause.DisconnectByServerReasonUnknown:
                    Debug.Log(cause);

                break;
            case DisconnectCause.InvalidAuthentication:

                Debug.Log(cause);
                break;
            case DisconnectCause.CustomAuthenticationFailed:

                Debug.Log(cause);
                break;
            case DisconnectCause.AuthenticationTicketExpired:

                Debug.Log(cause);
                break;
            case DisconnectCause.MaxCcuReached:

                Debug.Log(cause);
                break;
            case DisconnectCause.InvalidRegion:

                Debug.Log(cause);
                break;
            case DisconnectCause.OperationNotAllowedInCurrentState:
                Debug.Log(cause);

                break;
            case DisconnectCause.DisconnectByClientLogic:
                Debug.Log(cause);
                break;
            case DisconnectCause.DisconnectByOperationLimit:
                Debug.Log(cause);
                break;
            case DisconnectCause.DisconnectByDisconnectMessage:
                Debug.Log(cause);
                break;
            case DisconnectCause.ApplicationQuit:
                Debug.Log(cause);
                break;
            default:
                break;
        }
        base.OnDisconnected(cause);
    }

    IEnumerator StartReconnecting()
    {
        while (PhotonNetwork.Reconnect() == false)
        {
            Debug.Log("reconnecting");
            yield return null;
        }

        Debug.Log("connected");
    }

    public override void OnConnected()
    {
        Debug.Log("Connected");
        PlayButton.interactable = false;
        base.OnConnected();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        GameSettings.ConnectedtoMaster = true;
        PlayButton.interactable = true;
        //ConnectionCanvas.instance.showConnectedPanel(true);
        //base.OnConnectedToMaster();
    }



    public void SettingNickName_OnClick()
    {
        PhotonNetwork.NickName = nameInput.text;
        GameSettings.NickName = nameInput.text;
        AudioManager.Instance.Play("MainMenuSound");
        MenuManager.Instance.OpenMenu(menuName.PlayPanel);
    }

    public void onCLick_PlayButton()
    {
        SceneManager.LoadScene(1);
    }
}
