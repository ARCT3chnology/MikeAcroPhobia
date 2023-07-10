using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats 
{
    public static int BirthYear;
    //public static int Level = 1;
    public static float Experience;
    public static Texture2D PlayerImage;

    public static int CurrentLevel
    {
        get;set;
    }
    //public static int CurrentLevel
    //{
    //    get
    //    {
    //        return PlayerPrefs.GetInt("CurrentLevel", 0);
    //    }
    //    set
    //    {
    //        PlayerPrefs.SetInt("CurrentLevel", value);
    //    }
    //}
    //public static int CurrentStars
    //{
    //    get
    //    {
    //        return PlayerPrefs.GetInt("CurrentStars", 0);
    //    }
    //    set
    //    {
    //        PlayerPrefs.SetInt("CurrentStars", value);
    //    }
    //}
    public static int TotalVotes
    {
        get;set;
    }
    //public static int TotalVotes
    //{
    //    get
    //    {
    //        return PlayerPrefs.GetInt("TotalVotes", 0);
    //    }
    //    set
    //    {
    //        PlayerPrefs.SetInt("TotalVotes", value);
    //    }
    //}

    public static int ExperiencePoints
    {
        get; set;
    }
    //public static int ExperiencePoints
    //{
    //    get
    //    {
    //        return PlayerPrefs.GetInt("ExperiencePoints");
    //    }
    //    set
    //    {
    //        PlayerPrefs.SetInt("ExperiencePoints",value);

    //    }
    //}

    public static int GamesWon
    {
        get; set;
    }
    //public static int GamesWon
    //{
    //    get { return PlayerPrefs.GetInt("GamesWon"); }
    //    set { PlayerPrefs.SetInt("GamesWon", value); }
    //}

    //public static int totalvotes
    //{
    //    get; set;
    //}
    //public static int TotalVotes
    //{
    //    get { return PlayerPrefs.GetInt("TotalVotes"); }
    //    set { PlayerPrefs.SetInt("TotalVotes", value); }
    //}

    public static int WinRate
    {
        get; set;
    }
    //public static int WinRate
    //{
    //    get { return PlayerPrefs.GetInt("WinRate"); }
    //    set { PlayerPrefs.SetInt("WinRate", value); }
    //}

    public static int GamesLost
    {
        get; set;
    }
    //public static int GamesLost
    //{
    //    get { return PlayerPrefs.GetInt("GamesLost"); }
    //    set { PlayerPrefs.SetInt("GamesLost", value); }
    //}
}
