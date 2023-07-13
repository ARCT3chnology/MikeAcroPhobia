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
    public string personalChat { get; set; }
    
    
    public bool isConnected = false;

    #region CHAT CALL BACKS
    public void DebugReturn(DebugLevel level, string message)
    {

    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log("Chat State: " + state);
    }

    public void OnConnected()
    {
        Debug.Log("CHAT CONNECTED");
        isConnected = true;
        this.chatClient.Subscribe(new string[] { personalChat });
        this.chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }

    public void OnDisconnected()
    {
        Debug.Log("CHAT DISCONNECTED");
        isConnected = false;
        this.chatClient.Unsubscribe(new string[] { personalChat });
        this.chatClient.SetOnlineStatus(ChatUserStatus.Offline);
        //_uim.SendMsgField.SetActive(false);

    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            _uim.textArea.text += "<b>"+senders[i]+"</b>" + " : " + messages[i] + "\n";

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
        //foreach (var channel in channels)
        //{
            this.chatClient.PublishMessage(personalChat, "Joined");
        //}
        if(_uim.SendMsgField!=null)
            _uim.SendMsgField.SetActive(true);

    }

    public void OnUnsubscribed(string[] channels)
    {
        //foreach (var channel in channels)
        //{
            this.chatClient.PublishMessage(personalChat, "Left");
        //}
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
        if (isConnected == false)
        {
            Invoke(nameof(ConnectChat), 1.5f);
        }
            //ConnectChat();
    }

    private void OnDisable()
    {
        //if (chatClient != null) 
        //{ 
        //    this.chatClient.Disconnect(); 
        //}
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
        ConnectChat(GameSettings.NickName);
    }
    public void DisconnectChat()
    {
        Debug.Log("Disconnecting Chat");
        this.chatClient.Disconnect();
    }

    #endregion

}
