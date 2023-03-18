using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InfoPanel : MonoBehaviour
{

    [SerializeField] Text infoText;


    public void setinfoText(string text)
    {
        infoText.text = text;   
    }
}
