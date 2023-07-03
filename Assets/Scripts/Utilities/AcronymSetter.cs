using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcronymSetter : MonoBehaviourPun
{
    public enum acronyms
    {
        ThreeLetters,
        FourLetters,
        FiveLetters,
        SixLetters,
        SevenLetters
    }
    [SerializeField] bool _faceOffAcronym;
    public bool faceOffAcronym
    {
        get { return _faceOffAcronym; }
        set { _faceOffAcronym = value;}
    }
    public acronyms acronymType;
    [SerializeField] Text acroText;
    [HideInInspector] char[] alphabets = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    public int[] returnRandomAlphabet()
    {
        int a = Random.Range(0, 26);
        int b = Random.Range(0, 26);
        int c = Random.Range(0, 26);
        int d = Random.Range(0, 26);
        int e = Random.Range(0, 26);
        int f = Random.Range(0, 26);
        int g = Random.Range(0, 26);
        int[] alphas = new int[] { a, b, c,d,e,f,g }; 
        return alphas;
    }

    [PunRPC]
    public void RPC_setTextFor3Letters(int[] letters)
    {
        //Debug.Log("RPC_setText3");
        acroText.text = alphabets[letters[0]].ToString() + alphabets[letters[1]].ToString() + alphabets[letters[2]].ToString(); 
    }
    [PunRPC]
    public void RPC_setTextFor4Letters(int[] letters)
    {
        Debug.Log("RPC_setText4");
        acroText.text = alphabets[letters[0]].ToString() + alphabets[letters[1]].ToString() + alphabets[letters[2]].ToString()
            + alphabets[letters[3]].ToString(); 
    }
    [PunRPC]
    public void RPC_setTextFor5Letters(int[] letters)
    {
        Debug.Log("RPC_setText5");
        acroText.text = alphabets[letters[0]].ToString() + alphabets[letters[1]].ToString() + alphabets[letters[2]].ToString()
            +alphabets[letters[3]].ToString()+alphabets[letters[4]].ToString(); 
    }
    [PunRPC]
    public void RPC_setTextFor6Letters(int[] letters)
    {
        Debug.Log("RPC_setText6");
        acroText.text = alphabets[letters[0]].ToString() + alphabets[letters[1]].ToString() + alphabets[letters[2]].ToString()
            +alphabets[letters[3]].ToString()+alphabets[letters[4]].ToString()+alphabets[letters[5]].ToString(); 
    }
    [PunRPC]
    public void RPC_setTextFor7Letters(int[] letters)
    {
        Debug.Log("RPC_setText7");
        acroText.text = alphabets[letters[0]].ToString() + alphabets[letters[1]].ToString() + alphabets[letters[2]].ToString()
            + alphabets[letters[3]].ToString() + alphabets[letters[4]].ToString()+alphabets[letters[5]].ToString()+alphabets[letters[6]].ToString(); 
    }

    public void editor_setText(int[] letters)
    {
        acroText.text = alphabets[letters[0]].ToString() + alphabets[letters[1]].ToString() + alphabets[letters[2]].ToString(); 
    }

    private void OnEnable()
    {
        //#if !UNITY_EDITOR
        if (!faceOffAcronym)
        {
            if (acronymType == acronyms.ThreeLetters)
            {
                ThreeLetterAcronym();
            }
            else if (acronymType == acronyms.FourLetters)
            {
                FourLetterAcronym();
            }
            else if (acronymType == acronyms.FiveLetters)
            {
                FiveLetterAcronym();
            }
            else if (acronymType == acronyms.SixLetters)
            {
                SixLetterAcronym();
            }
            else if (acronymType == acronyms.SevenLetters)
            {
                SevenLetterAcronym();
            }
        }
        else
        {
            Debug.Log("Acro is: " + PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.ACRO_FOR_FACEOFF]);
            acroText.text = PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.ACRO_FOR_FACEOFF].ToString();
        }
//#endif
//#if UNITY_EDITOR
//        int[] letters = new int[3];
//        letters = returnRandomAlphabet();
//        editor_setText(letters);
//#endif
    }

    public void ThreeLetterAcronym()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int[] letters = new int[3];
            letters = returnRandomAlphabet();
            photonView.RPC(nameof(RPC_setTextFor3Letters), RpcTarget.AllBuffered, letters);
        }
    }
    public void ThreeLetterAcronym(Player p)
    {
        int[] letters = new int[3];
        letters = returnRandomAlphabet();
        photonView.RPC(nameof(RPC_setTextFor3Letters), p, letters);
        
    }
    public void FourLetterAcronym()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int[] letters = new int[4];
            letters = returnRandomAlphabet();
            photonView.RPC(nameof(RPC_setTextFor4Letters), RpcTarget.AllBuffered, letters);
        }
    }
    public void FourLetterAcronym(Player p)
    {
        int[] letters = new int[4];
        letters = returnRandomAlphabet();
        photonView.RPC(nameof(RPC_setTextFor4Letters), p, letters);
    }
    public void FiveLetterAcronym()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int[] letters = new int[5];
            letters = returnRandomAlphabet();
            photonView.RPC(nameof(RPC_setTextFor5Letters), RpcTarget.AllBuffered, letters);
        }
    }
    public void FiveLetterAcronym(Player p)
    {
        int[] letters = new int[5];
        letters = returnRandomAlphabet();
        photonView.RPC(nameof(RPC_setTextFor5Letters), p, letters);
    }

    public void SixLetterAcronym()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int[] letters = new int[6];
            letters = returnRandomAlphabet();
            photonView.RPC(nameof(RPC_setTextFor6Letters), RpcTarget.AllBuffered, letters);
        }
    }
    public void SixLetterAcronym(Player p)
    {
        int[] letters = new int[6];
        letters = returnRandomAlphabet();
        photonView.RPC(nameof(RPC_setTextFor6Letters), p, letters);   
    }
    public void SevenLetterAcronym()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int[] letters = new int[7];
            letters = returnRandomAlphabet();
            photonView.RPC(nameof(RPC_setTextFor7Letters), RpcTarget.AllBuffered, letters);
        }
    }
    public void SevenLetterAcronym(Player p)
    {
        int[] letters = new int[7];
        letters = returnRandomAlphabet();
        photonView.RPC(nameof(RPC_setTextFor7Letters), p, letters);
    }
}
