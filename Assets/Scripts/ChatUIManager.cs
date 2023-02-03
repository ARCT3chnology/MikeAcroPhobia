using UnityEngine;
using TMPro;

public class ChatUIManager : MonoBehaviour
{
    public GameObject chatPanel;
    public TMP_InputField enterMsg;
    public GameObject SendMsgField;
    public TMP_Text textArea;
    

    public void ClearenterMsg()
    {
        enterMsg.text = "";
    }
                    
}
