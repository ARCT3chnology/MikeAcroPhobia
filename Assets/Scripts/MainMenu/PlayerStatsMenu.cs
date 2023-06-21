using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsMenu : MonoBehaviour
{
    [SerializeField] TMP_Text PlayerName;
    [SerializeField] TMP_Text PlayerLevel;
    [SerializeField] Text StarsText;
    [SerializeField] Image ExperienceSlider;
    [SerializeField] GameObject PlayerStatsUI;
    [SerializeField] RawImage PlayerImageHolder;
    [SerializeField] Texture defaultImage;
    public static PlayerStatsMenu Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            DestroyImmediate(Instance);
        }
    }

    public void setName()
    {
        PlayerName.text = GameSettings.NickName;
        PhotonNetwork.LocalPlayer.NickName = GameSettings.NickName;
    }    

    public void setLevel()
    {
        PlayerLevel.text = (PlayerStats.CurrentLevel + 1).ToString();
    }

    public void setImage()
    {
        if (PlayerStats.PlayerImage!=null)
        {
            PlayerImageHolder.texture = PlayerStats.PlayerImage;
        }
        else
        {
            PlayerImageHolder.texture = defaultImage;
        }
    }

    public void setExperienceSlider()
    {
        float fillValue;
        if (PlayerStats.CurrentStars > 0)
        {
            fillValue = ((float)PlayerStats.CurrentStars - (float)MasterManager.Instance._gameSettings.gameLevels[PlayerStats.CurrentLevel].minStars) / ((float)MasterManager.Instance._gameSettings.gameLevels[PlayerStats.CurrentLevel].maxStars / (float)MasterManager.Instance._gameSettings.gameLevels[PlayerStats.CurrentLevel].minStars);
        }
        else
        {
            fillValue = 0;
        }
        PlayerStats.Experience = fillValue;
        //Debug.Log("Experience: " + fillValue.ToString());
        ExperienceSlider.fillAmount = PlayerStats.Experience;
    }

    public void setPlayerStatsmenuState(bool state)
    {
        PlayerStatsUI.SetActive(state);
    }

    public void UpdateStarsText() 
    {
        StarsText.text = PlayerStats.CurrentStars.ToString() + " / " +MasterManager.Instance._gameSettings.gameLevels[PlayerStats.CurrentLevel].maxStars.ToString();
    }
}
