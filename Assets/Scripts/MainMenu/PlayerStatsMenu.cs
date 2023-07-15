using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerStatsMenu : MonoBehaviour
{
    [SerializeField] TMP_Text PlayerName;
    [SerializeField] TMP_Text PlayerLevel;
    [SerializeField] Text StarsText;
    [SerializeField] Image ExperienceSlider;
    [SerializeField] GameObject PlayerStatsUI;
    [SerializeField] RawImage PlayerImageHolder;
    [SerializeField] Texture defaultImage;
    [SerializeField] GraphicRaycaster graphicRaycaster;
    public static PlayerStatsMenu Instance;

    [Serializable]
    public struct DetailedPlayerStats
    {
        public GameObject mainGameObject;
        public Image PlayerImage;
        public TMP_Text txt_PlayerName;
        public TMP_Text txt_GamesWon;
        public TMP_Text txt_TotalVotes;
        public TMP_Text txt_WinRate;
        public TMP_Text txt_Level;
        public Image experienceSlider;
        public TMP_Text txt_Experience;
    }
    public DetailedPlayerStats LargePlayerStats;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(Instance);
        }
    }

    public void SetGamesWonText()
    {
        LargePlayerStats.txt_GamesWon.text = PlayerStats.GamesWon.ToString();
        setWinRate();
    }

    public void setVotesText()
    {
        LargePlayerStats.txt_TotalVotes.text = PlayerStats.TotalVotes.ToString();
    }

    public void setWinRate()
    {
        if (PlayerStats.GamesLost == 0 && PlayerStats.GamesWon > 0)
        {
            LargePlayerStats.txt_WinRate.text = "100%";
        }
        else if (PlayerStats.GamesLost > 0 && PlayerStats.GamesWon > 0)
        {
            LargePlayerStats.txt_WinRate.text = ((PlayerStats.GamesLost/PlayerStats.GamesWon)*100).ToString() +"%";
        }
        else
        {
            LargePlayerStats.txt_WinRate.text = "0%";
        }
    }

    public void setName()
    {
        PlayerName.text = GameSettings.NickName;
        LargePlayerStats.txt_PlayerName.text = GameSettings.NickName;
        PhotonNetwork.LocalPlayer.NickName = GameSettings.NickName;
        SetGamesWonText();
        setVotesText();
        setWinRate();
    }    

    public void setLevel()
    {
        PlayerLevel.text = (PlayerStats.CurrentLevel + 1).ToString();
        LargePlayerStats.txt_Level.text = (PlayerStats.CurrentLevel + 1).ToString();
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
        //Debug.Log("Setting SLider Value");
        float fillValue;
        if (PlayerStats.ExperiencePoints > 0)
        {
            Debug.Log("Current: " + PlayerStats.ExperiencePoints + "Min " + (float)MasterManager.Instance._gameSettings.gameLevels[PlayerStats.CurrentLevel].minStars + "Max: " + MasterManager.Instance._gameSettings.gameLevels[PlayerStats.CurrentLevel].maxStars);
            fillValue = ((float)PlayerStats.ExperiencePoints - (float)MasterManager.Instance._gameSettings.gameLevels[PlayerStats.CurrentLevel].minStars) / ((float)MasterManager.Instance._gameSettings.gameLevels[PlayerStats.CurrentLevel].maxStars - (float)MasterManager.Instance._gameSettings.gameLevels[PlayerStats.CurrentLevel].minStars);
            if(fillValue >= 1)
            {
                Debug.Log("Level " + PlayerStats.CurrentLevel + " Completed starting next level");
                StartCoroutine(startNextLevel());
            }
        }
        else
        {
            fillValue = 0;
        }
        PlayerStats.Experience = fillValue;
        //Debug.Log("Experience: " + fillValue.ToString());
        ExperienceSlider.fillAmount = PlayerStats.Experience;
        LargePlayerStats.experienceSlider.fillAmount = PlayerStats.Experience;
    }

    public IEnumerator startNextLevel()
    {
        yield return new WaitForSeconds(2f);
        updateLevelText();
        setExperienceSlider();
        UpdateStarsText();
    }

    public void updateLevelText()
    {
        PlayerLevel.transform.parent.transform.DOPunchScale(Vector3.one, .5f);
        PlayerStats.CurrentLevel++;
        setLevel();
    }

    public void setPlayerStatsmenuState(bool state)
    {
        PlayerStatsUI.SetActive(state);
    }

    public void UpdateStarsText() 
    {
        StarsText.text = PlayerStats.ExperiencePoints.ToString() + " / " +MasterManager.Instance._gameSettings.gameLevels[PlayerStats.CurrentLevel].maxStars.ToString();
        LargePlayerStats.txt_Experience.text = PlayerStats.ExperiencePoints.ToString() + " / " +MasterManager.Instance._gameSettings.gameLevels[PlayerStats.CurrentLevel].maxStars.ToString();
    }

    void Update()
    {
        // Check if the player has touched the screen
#if !UNITY_EDITOR
        if (Input.touchCount > 0)
            {
                // Get the first touch
                Touch touch = Input.GetTouch(0);

                // Check if the touch is over a UI element
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    // The touch is over a UI element
                    // Implement your logic here when the UI element is touched
                    Debug.Log("UI element touched");
                }
            }
#endif
#if UNITY_EDITOR || UNITY_STANDALONE_WIN 
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // Get the mouse position
                Vector3 mousePosition = Input.mousePosition;

                // Create a pointer event
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = mousePosition;

                // Perform the raycast and store the results
                List<RaycastResult> results = new List<RaycastResult>(); // Adjust the size based on your needs
                EventSystem.current.RaycastAll(eventData, results);

                // Iterate through the raycast results
                for (int i = 0; i < results.Count; i++)
                {
                    GameObject hitObject = results[i].gameObject;

                    // Check if the hit object is a UI element
                    if (hitObject.GetComponent<UIBehaviour>() != null)
                    {
                        // A UI element was hit by the raycast
                        // Implement your logic based on the hit information
                        //Debug.Log("Hit UI element: " + hitObject.name);
                        if(hitObject.name == "PlayerStatsImage")
                        {
                            LargePlayerStats.mainGameObject.SetActive(true);
                            LargePlayerStats.mainGameObject.transform.DOScale(Vector3.one,.5f).SetEase(Ease.InOutSine);
                            graphicRaycaster.enabled = true;
                        }

                        if (hitObject.name == "ClosePanel")
                        {
                            LargePlayerStats.mainGameObject.transform.DOScale(Vector3.zero, .5f).SetEase(Ease.InOutSine).OnComplete(() =>
                            {
                                LargePlayerStats.mainGameObject.SetActive(false);
                                graphicRaycaster.enabled = false;
                            });
                        }
                        break; // Exit the loop after the first UI hit
                    }
                }
            }
        }
#endif 

    }

    public void UpdateMatchesWon()
    {
        Debug.Log("Updating Games Won");
        PlayerStats.GamesWon++;
        SetGamesWonText();
    }


    public void UpdateMatchesLost()
    {
        Debug.Log("Updating Games LOST");
        PlayerStats.GamesLost++;
        setWinRate();
    }
}
