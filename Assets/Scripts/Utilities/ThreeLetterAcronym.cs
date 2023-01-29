using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeLetterAcronym : MonoBehaviourPun
{
    [SerializeField] Text acroText;
    [HideInInspector] char[] alphabets = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    public int[] returnRandomAlphabet()
    {
        int a = Random.Range(0, 26);
        int b = Random.Range(0, 26);
        int c = Random.Range(0, 26);
        object[] datas = new object[] { a, b, c };
        int[] alphas = new int[] { a, b, c }; 
        return alphas;
    }

    [PunRPC]
    public void RPC_setText(int[] letters)
    {
        Debug.Log("RPC_setText");
        acroText.text = alphabets[letters[0]].ToString() + alphabets[letters[1]].ToString() + alphabets[letters[2]].ToString(); 
    }

    public void editor_setText(int[] letters)
    {
        acroText.text = alphabets[letters[0]].ToString() + alphabets[letters[1]].ToString() + alphabets[letters[2]].ToString(); 
    }

    private void Start()
    {
//#if !UNITY_EDITOR
        if (PhotonNetwork.IsMasterClient)
        {
            int[] letters = new int[3];
            letters = returnRandomAlphabet();
            photonView.RPC("RPC_setText", RpcTarget.All,letters);
        }
//#endif
//#if UNITY_EDITOR
//        int[] letters = new int[3];
//        letters = returnRandomAlphabet();
//        editor_setText(letters);
//#endif
    }

}
