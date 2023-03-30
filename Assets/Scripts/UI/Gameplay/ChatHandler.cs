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
    public void OnClick_ChatButton()
    {
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
}
