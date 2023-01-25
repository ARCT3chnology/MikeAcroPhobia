using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomCustomProperty : MonoBehaviour
{
    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();
    [SerializeField] Text _text;

    private void setCustomNumber()
    {
        System.Random rnd = new System.Random();
        int result = rnd.Next(0,99);
        _text.text = result.ToString();
        _myCustomProperties["RandomNumber"] = result;
        PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
    }

    public void onCLick_Button()
    {
        setCustomNumber();
    }

}
