using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ConnectionCanvas : MonoBehaviour
{
    [SerializeField] RectTransform Panel;
    [SerializeField] Image TextImage;
    [SerializeField] Image iconImage;
    [SerializeField] Sprite connectedSprite;
    [SerializeField] Sprite disconnectedSprite;
    public static ConnectionCanvas instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public void showConnectedPanel(bool autoHide)
    {
        iconImage.color = Color.green;
        TextImage.sprite = connectedSprite;
        Panel.DOAnchorPosX(10, 0.5f).SetEase(Ease.InOutBounce).OnComplete(() =>
        {
            if (autoHide)
                Invoke(nameof(Hidepanel), 3f);
        });
    }

    public void Hidepanel()
    {
        Panel.DOAnchorPosX(-(Screen.width), 0.5f).SetEase(Ease.InOutBounce);
    }

    public void showDisConnectedPanel()
    {
        iconImage.color = Color.red;
        TextImage.sprite = disconnectedSprite;
        Panel.DOAnchorPosX(-10, 0.5f).From().SetEase(Ease.InOutBounce);
    }


}
