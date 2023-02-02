using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static AcronymSetter;

public class RoundConfigurator : MonoBehaviour
{
    public Text titleText;
    public Slider timeSlider;
    public Timer roundTimer;
    public AcronymSetter acronymSetter;
    public VotingMenu votingMenu;
    public InputField answerInput;


    public void setTitleText(string text)
    {
        titleText.text = text;
    }

    public void setAcronymType(acronyms acronyms)
    {
        acronymSetter.acronymType = acronyms;
    }

    public void setTimerForRound(float endTime)
    {
        roundTimer._starttime = endTime;
        roundTimer._endtime = 0;
    }

    public void resetAnswerField()
    {
        answerInput.SetTextWithoutNotify("");
        //votingMenu.resetVotesList();
    }
}
