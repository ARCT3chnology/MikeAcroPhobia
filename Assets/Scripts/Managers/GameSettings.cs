using Photon.Realtime;
using System;
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
    [SerializeField] byte _maxPlayerForLobby;
    [SerializeField] byte _maxPlayerForRoom;
    [SerializeField] byte _maxPlayerForRandomLobby;
    public List<levels> gameLevels;

    [Serializable]
    public struct levels
    {
        public int minStars;
        public int maxStars;
    }
    public byte maxPlayerForLobby
    {
        get
        {
            return _maxPlayerForLobby;
        }
        set 
        { 
            _maxPlayerForLobby = value; 
        }
    }
    public byte maxPlayerForRoom
    {
        get
        {
            return _maxPlayerForRoom;
        }
        set 
        {
            _maxPlayerForRoom = value; 
        }
    }

    public byte maxPlayerForRandomLobby
    {
        get
        {
            return _maxPlayerForRandomLobby;
        }
        set
        {
            _maxPlayerForRandomLobby = value;
        }
    }

    static int val;
    private void OnEnable()
    {
        //for testing we use the variable val wit hval In the NickName.

        val = UnityEngine.Random.Range(0, 999);
        
    }

    public static string NickName
    {
        get 
        {
            //return PlayerPrefs.GetString("Name"+val.ToString(), "Player");
            return PlayerPrefs.GetString("Name", "Player");
        }
        set 
        { 
            //PlayerPrefs.SetString("Name"+val.ToString(), value);
            PlayerPrefs.SetString("Name", value);
        }
    }



    public static List<LocalRoomInfo> CurrentRooms;

    public static bool ConnectedtoMaster { get; set; }

    public static string ROUND_NUMBER {
        get 
        {
            return "ROUND_NUMBER";
        }
    }
    public static string ROUND_TIME {
        get 
        {
            return "ROUND_TIME";
        }
    }
    public static string VOTING_IN_PROGRESS {
        get 
        {
            return "VOTING_IN_PROGRESS";
        }
    }
    public static string FACEOFF_IN_PROGRESS {
        get 
        {
            return "FACEOFF_IN_PROGRESS";
        }
    }

    public static string ACRO_FOR_FACEOFF
    {
        get
        {
            return "ACRO_FOR_FACEOFF";
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
    public static string PlAYERS_LEFT 
    {
        get
        {
            return "PlAYERS_LEFT";
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

    public static string PlAYER5_VOTES
    {
        get
        {
            return "PlAYER5_VOTES";
        }
    }
    public static string PlAYER6_VOTES
    {
        get
        {
            return "PlAYER6_VOTES";
        }
    }
    public static string PlAYER7_VOTES
    {
        get
        {
            return "PlAYER7_VOTES";
        }
    }
    public static string PlAYER8_VOTES
    {
        get
        {
            return "PlAYER8_VOTES";
        }
    }
    public static string PlAYER9_VOTES
    {
        get
        {
            return "PlAYER9_VOTES";
        }
    }
    public static string PlAYER10_VOTES
    {
        get
        {
            return "PlAYER10_VOTES";
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

    public static string TimerStartedKey
    {
        get
        {
            return "TimerStartedKey";
        }
    }    

    public static string TimerStartTimeKey
    {
        get
        {
            return "TimerStartTimeKey";
        }
    }

    /// <summary>
    /// The amount of votes player would get;
    /// </summary>
    public static string PLAYER_VOTES
    {
        get
        {
            return "PLAYER_VOTES";
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
            return 60;
        }
    }
    public static float FourLetterRoundTime
    {
        get
        {
            return 60;
        }
    }
    public static float FiveLetterRoundTime
    {
        get
        {
            return 60;
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
        PlayerVotesArray.Add(PlAYER5_VOTES);
        PlayerVotesArray.Add(PlAYER6_VOTES);
        PlayerVotesArray.Add(PlAYER7_VOTES);
        PlayerVotesArray.Add(PlAYER8_VOTES);
        PlayerVotesArray.Add(PlAYER9_VOTES);
        PlayerVotesArray.Add(PlAYER10_VOTES);
    }
    public static bool PlayerInRoom
    {
        get;set;
    }
}
public class LocalRoomInfo
{
    public string roomName;
    public int playerCount;
}
