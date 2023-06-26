using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using DanielLochner.Assets.SimpleScrollSnap;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class Connectivity : MonoBehaviourPunCallbacks
{

    [SerializeField] InputField nameInput;
    [SerializeField] Text placeholderText;
    [SerializeField] Button PlayButton;
    [SerializeField] bool connected;
    [SerializeField] Menu LoginMenu;
    [SerializeField] Menu SplashMenu;
    [SerializeField] GameObject ageInput;
    //private void Awake()
    //{
    //    PlayerPrefs.DeleteAll();
    //}

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

        //if (GameSettings.NickName != "Player")
        //{
        //    MenuManager.Instance.CloseMenu(LoginMenu);
        //    MenuManager.Instance.OpenMenu(menuName.PlayPanel);
        //    PlayerStatsMenu.Instance.setName();
        //    PlayerStatsMenu.Instance.setLevel();
        //    PlayerStatsMenu.Instance.setImage();
        //    PlayerStatsMenu.Instance.setExperienceSlider();
        //    PlayerStatsMenu.Instance.setPlayerStatsmenuState(true);
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
        nameInput.gameObject.SetActive(false);
        ImageInput.SetActive(true);
    }

    public void onCLick_PlayButton()
    {
        createLobbyForQuickPlay();
        //SceneManager.LoadScene(2);
    }


    public void createLobbyForQuickPlay()
    {
        TypedLobby typedLobby = new TypedLobby("QuickGame", LobbyType.Default);
        PhotonNetwork.JoinLobby(typedLobby);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby: " + "QuickGame");
        JoinRandomRoom();
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Left lobby: " + "QuickGame");
    }

    public void JoinRandomRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = SingletonReferences.instance.MasterManager._gameSettings.maxPlayerForRandomLobby;
        options.PlayerTtl = 0;
        options.EmptyRoomTtl = 0;
        options.IsOpen = true;
        options.IsVisible = true;
        options.CustomRoomPropertiesForLobby = new string[] { "NotStarted" };
        addRoomProperties(options);
        PhotonNetwork.JoinOrCreateRoom("Random", options, TypedLobby.Default);

    }

    private static void addRoomProperties(RoomOptions options)
    {
        Hashtable roomProps = new Hashtable();
        roomProps.Add(GameSettings.PlAYER1_VOTES, 0);
        roomProps.Add(GameSettings.PlAYER2_VOTES, 0);
        roomProps.Add(GameSettings.PlAYER3_VOTES, 0);
        roomProps.Add(GameSettings.PlAYER4_VOTES, 0);
        roomProps.Add(GameSettings.PlAYERS_VOTED, 0);
        roomProps.Add(GameSettings.PlAYERS_LEFT, 0);
        roomProps.Add(GameSettings.ROUND_NUMBER, 0);
        roomProps.Add(GameSettings.ROUND_TIME, 0);
        roomProps.Add(GameSettings.TOURNAMENT_NUMBER, 0);
        roomProps.Add(GameSettings.FACEOFF_ROUND_NUMBER, 0);
        roomProps.Add(GameSettings.VOTING_IN_PROGRESS, false);
        roomProps.Add(GameSettings.ALL_ANSWERS_SUBMITTED, false);
        roomProps.Add(GameSettings.NO_OF_ANSWERS_SUBMITTED, 0);
        options.CustomRoomProperties = roomProps;
    }

    public override void OnJoinedRoom()
    {
        LoadLevelForPlayers();
        //if (PhotonNetwork.CurrentRoom.PlayerCount == SingletonReferences.instance.MasterManager._gameSettings.maxPlayerForRandomLobby)
        //{
        //    //PhotonNetwork.CurrentRoom.IsOpen = false;
        //}
        base.OnJoinedRoom();
    }

    public void LoadLevelForPlayers()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            photonView.RPC(nameof(RPC_LoadLevel), PhotonNetwork.PlayerList[i]);
        }
    }

    [PunRPC]
    public void RPC_LoadLevel()
    {
        Debug.Log("Loading level in Lobby system");
        AudioManager.Instance.Stop("MainMenuSound");
        SceneManager.LoadScene(3);
    }

    public void onCLick_JoinRoom()
    {
        SceneManager.LoadScene(1);
    }


    [SerializeField] SimpleScrollSnap ageScroll;
    [SerializeField] RawImage UserProfile;
    [SerializeField] GameObject ImageInput;
    public void OnClick_AgeScroller()
    {
        string age = ageScroll.Panels[ageScroll.CenteredPanel].GetComponent<Text>().text;
        PlayerStats.BirthYear = int.Parse(age);
        Debug.Log(age);
        Debug.Log("Selected Panel: " + ageScroll.SelectedPanel);
    }


    public void OnClick_AgeSelected()
    {
        Debug.Log("Player age is:" + PlayerStats.BirthYear);
        MenuManager.Instance.OpenMenu(menuName.PlayPanel);
        PlayerStatsMenu.Instance.setName();
        PlayerStatsMenu.Instance.setLevel();
        PlayerStatsMenu.Instance.setImage();
        PlayerStatsMenu.Instance.setExperienceSlider();
        PlayerStatsMenu.Instance.UpdateStarsText();
        PlayerStatsMenu.Instance.setPlayerStatsmenuState(true);
    }

    public void OnCLick_ImageSelected() 
    {
        ImageInput.SetActive(false);
        ageInput.SetActive(true);
        
    }



    public void OnClick_SelectImage()
    {
        NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, 512);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                // Assign texture to a temporary quad and destroy it after 5 seconds
                //GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                //quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                //quad.transform.forward = Camera.main.transform.forward;
                //quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

                //Material material = SingleInterface.ProfilePic.GetComponent<Image>().material;
                UserProfile.texture = texture;
                PlayerStats.PlayerImage = texture;
                //if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                //    material.shader = Shader.Find("Legacy Shaders/Diffuse");

                //material.mainTexture = texture;

                //Destroy(quad, 5f);

                // If a procedural texture is not destroyed manually, 
                // it will only be freed after a scene change
                //Destroy(texture, 5f);
            }
        }, imageName);
    }
    string imageName;
}
