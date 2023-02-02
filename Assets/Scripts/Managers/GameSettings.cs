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
    public string NickName
    {
        get 
        {
            int val = Random.Range(0, 999);
            return _nickName+val.ToString(); 
        }
    }

    public static string ROUND_NUMBER {
        get 
        {
            return "ROUND_NUMBER";
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
    public static string PlAYER_ANSWER 
    {
        get
        {
            return "PlAYER_ANSWER";
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
            return 10;
        }
    }
    public static float FourLetterRoundTime
    {
        get
        {
            return 10;
        }
    }
    public static float FiveLetterRoundTime
    {
        get
        {
            return 10;
        }
    }
    public static float SixLetterRoundTime
    {
        get
        {
            return 10;
        }
    }
    public static float SevenLetterRoundTime
    {
        get
        {
            return 10;
        }
    }
    public static bool gameStarted
    {
        get;
        set;
    }
    public static List<string> PlayerVotesArray = new List<string>();

    private void Awake()
    {
        setPlayerVotesArray();
    }

    private static void setPlayerVotesArray()
    {
        PlayerVotesArray.Add(PlAYER1_VOTES);
        PlayerVotesArray.Add(PlAYER2_VOTES);
        PlayerVotesArray.Add(PlAYER3_VOTES);
    }
}
