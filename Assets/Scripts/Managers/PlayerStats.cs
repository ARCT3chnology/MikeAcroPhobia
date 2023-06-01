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
        get
        {
            return PlayerPrefs.GetInt("CurrentLevel", 0);
        }
        set
        {
            PlayerPrefs.SetInt("CurrentLevel", value);
        }
    }
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
    public static int CurrentStars;
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
}
