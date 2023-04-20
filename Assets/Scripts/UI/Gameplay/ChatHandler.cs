using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatHandler : MonoBehaviour
{
    //[SerializeField] Text ButtonText;
    [SerializeField] bool chatOpened;
    [SerializeField] GameObject GlobalChatPanel;
    [SerializeField] GameObject PersonalChatPanel;
    [SerializeField] bool isPublic;
    [SerializeField] Image icon;
    [SerializeField] Sprite ChatIcon;
    [SerializeField] Sprite CrossIcon;

    [SerializeField] ChatManager _lobbyChatManager;
    public ChatManager LobbyChatManager
    {
        get => _lobbyChatManager;
        set => _lobbyChatManager = value;
    }
    [SerializeField] ChatManager _roomChatManager;
    public ChatManager RoomChatManager
    {
        get => _roomChatManager;
        set => _roomChatManager = value;
    }

    //private void OnEnable()
    //{
    //    if (isPublic)
    //    {
    //        JoinLobbyChat();
    //    }
    //}

    public void OnClick_ChatButton()
    {
        AudioManager.Instance.Play("MenuButton");

        if (isPublic)
        {
            if (chatOpened)
            {
                GlobalChatPanel.SetActive(false);
                chatOpened = false;
                icon.sprite = ChatIcon;
            }
            else
            {
                GlobalChatPanel.SetActive(true);
                icon.sprite = CrossIcon;
                chatOpened = true;
            }
        }
        else
        {
            if (chatOpened)
            {
                PersonalChatPanel.SetActive(false);
                chatOpened= false;
                icon.sprite = ChatIcon;
            }
            else
            {
                PersonalChatPanel.SetActive(true);
                icon.sprite = CrossIcon;
                chatOpened = true;
            }
        }
    }

    public void JoinLobbyChat(string RoomName)
    {
        if (RoomChatManager.isConnected)
        {
            RoomChatManager.DisconnectChat();
        }
        LobbyChatManager.personalChat = RoomName;

        LobbyChatManager.ConnectChat();
        isPublic = true;
    }

    public void JoinRoomChat(string RoomName)
    {
        if (LobbyChatManager.isConnected)
        {
            LobbyChatManager.DisconnectChat();
        }
        RoomChatManager.personalChat = RoomName;
        RoomChatManager.ConnectChat();
        isPublic = false;
    }
}
