using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vote : MonoBehaviour
{
    [SerializeField] Text answerText;
    [SerializeField] Button votebutton;

    public void setVoteText(Player player )
    {

        answerText.text = player.CustomProperties["ThreeletterAcronym"].ToString();
    }
}
