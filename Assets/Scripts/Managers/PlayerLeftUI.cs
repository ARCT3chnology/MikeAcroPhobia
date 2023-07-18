using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLeftUI : MonoBehaviour
{
    [SerializeField] TMP_Text txt_left;
    [SerializeField] Vector2 endPosition;
    [SerializeField] Vector2 startPosition;


    public void showText(string playerName)
    {
        startPosition = txt_left.GetComponent<RectTransform>().anchoredPosition ;
        endPosition = new Vector2(0, -(Screen.height - 100f));
        txt_left.GetComponent<RectTransform>().DOAnchorPos(startPosition,0.5f).From(endPosition);
        txt_left.text = "\""+playerName + "\" Has Left The Room";
        gameObject.SetActive(true);
        Invoke(nameof(hideText), 4f);
    }

    public void hideText()
    {
        txt_left.GetComponent<RectTransform>().DOAnchorPos(endPosition, 0.5f).From(startPosition);
        //txt_left.text = playerName + "Has Left the Room";
        gameObject.SetActive(false);
    }
    [ContextMenu("Setheight")]
    public void setHeight()
    {
        endPosition = new Vector2(0, -(Screen.height - 100f));

    }
    
}
