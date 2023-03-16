using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "manager/GameSetting")]
public class GameSettings : ScriptableObject
{
    [SerializeField] string _gameVersion;
    public string GameVersion { get { return _gameVersion; } }
    [SerializeField] string _nickName;
    public static string NickName
    {
        get 
        {
            //int val = Random.Range(0, 999);
            return PlayerPrefs.GetString("Name", "Player"); 
        }
        set 
        { 
            PlayerPrefs.SetString("Name", value);
        }
    }

    public static List<RoomInfo> CurrentRooms;

    public static bool ConnectedtoMaster { get; set; }

    public static string ROUND_NUMBER {
        get 
        {
            return "ROUND_NUMBER";
        }
    }
    public static string TOURNAMENT_NUMBER {
        get 
        {
            return "TOURNAMENT_NUMBER";
        }
    }
    public static string FACEOFF_ROUND_NUMBER {
        get 
        {
            return "FACEOFF_ROUND_NUMBER";
        }
    }
    public static string ALL_ANSWERS_SUBMITTED {
        get 
        {
            return "ALL_ANSWERS_SUBMITTED";
        }
    }
    public static string NO_OF_ANSWERS_SUBMITTED {
        get 
        {
            return "NO_OF_ANSWERS_SUBMITTED";
        }
    }
    public static string PlAYERS_VOTED 
    {
        get
        {
            return "PlAYERS_VOTED";
        }
    }
    public static string PlAYER1_VOTES 
    {
        get
        {
            return "PlAYER1_VOTES";
        }
    }
    public static string PlAYER2_VOTES 
    {
        get
        {
            return "PlAYER2_VOTES";
        }
    }

    public static string PlAYER3_VOTES 
    {
        get
        {
            return "PlAYER3_VOTES";
        }
    }
    public static string PlAYER4_VOTES 
    {
        get
        {
            return "PlAYER4_VOTES";
        }
    }
    public static string PlAYER_ANSWER 
    {
        get
        {
            return "PlAYER_ANSWER";
        }
    }    
    public static string ANSWER_SUBMITTED
    {
        get
        {
            return "ANSWER_SUBMITTED";
        }
    }
    public static int MAX_ROUNDS
    {
        get
        {
            return 5;
        }
    }
    public static float ThreeLetterRoundTime
    {
        get
        {
            return 30;
        }
    }
    public static float FourLetterRoundTime
    {
        get
        {
            return 40;
        }
    }
    public static float FiveLetterRoundTime
    {
        get
        {
            return 50;
        }
    }
    public static float SixLetterRoundTime
    {
        get
        {
            return 60;
        }
    }
    public static float SevenLetterRoundTime
    {
        get
        {
            return 70;
        }
    }
    public static bool normalGame
    {
        get;
        set;
    }
    public static bool FaceOffGame
    {
        get;
        set;
    }
    public static List<string> PlayerVotesArray;

    private void Awake()
    {
        PlayerVotesArray = new List<string>();
        setPlayerVotesArray();
    }

    public void setPlayerVotesArray()
    {
        PlayerVotesArray = new List<string>();
        Debug.Log("setPlayerVotesArray");
        PlayerVotesArray.Add(PlAYER1_VOTES);
        PlayerVotesArray.Add(PlAYER2_VOTES);
        PlayerVotesArray.Add(PlAYER3_VOTES);
        PlayerVotesArray.Add(PlAYER4_VOTES);
    }
    public static bool PlayerInRoom
    {
        get;set;
    }
}
