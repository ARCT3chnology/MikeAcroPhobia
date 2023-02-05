using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp;
using AuthenticationValues = Photon.Chat.AuthenticationValues;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    //COMPONENETS
    public ChatUIManager _uim;
    //PRIVATE VARIABLES
    ChatClient chatClient;

    //CHANNEL IDS
    [SerializeField] string personalChat;
    bool isConnected = false;

    #region CHAT CALL BACKS
    public void DebugReturn(DebugLevel level, string message)
    {

    }

    public void OnChatStateChange(ChatState state)
    {

    }

    public void OnConnected()
    {
        Debug.Log("CHAT CONNECTED");

        this.chatClient.Subscribe(new string[] { personalChat });
        this.chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }

    public void OnDisconnected()
    {
        Debug.Log("CHAT DISCONNECTED");

        this.chatClient.Unsubscribe(new string[] { personalChat });
        this.chatClient.SetOnlineStatus(ChatUserStatus.Offline);
        //_uim.SendMsgField.SetActive(false);

    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            _uim.textArea.text += senders[i] + " : " + "<mark=#FF800050> " + messages[i] + " </mark>" + "\n";


            Debug.Log(senders[i]);
        }

    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {

    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        foreach (var channel in channels)
        {
            this.chatClient.PublishMessage(channel, "Joined");
        }
        _uim.SendMsgField.SetActive(true);

    }

    public void OnUnsubscribed(string[] channels)
    {
        foreach (var channel in channels)
        {
            this.chatClient.PublishMessage(channel, "Left");
        }
        //_uim.SendMsgField.SetActive(false);
    }

    public void OnUserSubscribed(string channel, string user)
    {

    }

    public void OnUserUnsubscribed(string channel, string user)
    {

    }
    #endregion
    #region UNITY CALLBACKS
    private void Start()
    {
        Application.runInBackground = true;
    }
    void OnApplicationQuit()
    {
        if (chatClient != null) { chatClient.Disconnect(); }
    }


    private void OnEnable()
    {
        ConnectChat();
    }

    private void OnDisable()
    {
        if (chatClient != null) { this.chatClient.Disconnect(); }
    }
    private void Update()
    {
        //EXTABLISH AND MAINTAIN A CONNECTION
        if (isConnected) chatClient.Service();



    }

    #endregion


    

    #region UI CALLBACKS
    public void SendMsg_OnClick()
    {
        if (_uim.enterMsg.text.IsNullOrEmpty()) return;
        this.chatClient.PublishMessage(personalChat, _uim.enterMsg.text);
        _uim.ClearenterMsg();
    }

    #endregion
    
    #region PRIVATE FUNCTIONS
    private void ConnectChat(string id)
    {
        if (personalChat.IsNullOrEmpty()) 
        { 
            personalChat = PhotonNetwork.CurrentRoom.Name;
        }

        isConnected = true;
        
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat,
            PhotonNetwork.AppVersion,
            new AuthenticationValues(id));
    }


    #endregion
    #region PUBLIC CALLBACKS
    public void ConnectChat()
    {
        ConnectChat(PhotonNetwork.NickName);
    }

    #endregion

}
