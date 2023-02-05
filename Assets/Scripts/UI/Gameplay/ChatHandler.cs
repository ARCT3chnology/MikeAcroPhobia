using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatHandler : MonoBehaviour
{
    [SerializeField] Text ButtonText;
    [SerializeField] bool chatOpened;
    [SerializeField] GameObject GlobalChatPanel;
    [SerializeField] GameObject PersonalChatPanel;
    public void OnClick_ChatButton()
    {
        if (MenuManager.Instance.gameMenus[2].gameObject.activeInHierarchy)
        {
            if (chatOpened)
            {
                GlobalChatPanel.SetActive(false);
                chatOpened = false;
                ButtonText.text = "CHAT";
            }
            else
            {
                GlobalChatPanel.SetActive(true);
                ButtonText.text = "BACK";
                chatOpened = true;
            }
        }
        else
        {
            if (chatOpened)
            {
                PersonalChatPanel.SetActive(false);
                chatOpened= false;
                ButtonText.text = "CHAT";
            }
            else
            {
                PersonalChatPanel.SetActive(true); 
                ButtonText.text = "BACK";
                chatOpened = true;
            }
        }
    }
}
