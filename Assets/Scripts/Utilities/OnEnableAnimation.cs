using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableAnimation : MonoBehaviour
{
    [System.Serializable]
    private struct AnimationObjects
    {
        public List<RectTransform> rectTransforms;
        public List<Vector2> positions;
        public Ease ease;
        public float delay;
        public float speed;
    }

    [System.Serializable]
    private struct ScaleObjects
    {
        public List<RectTransform> transforms;
        public List<Vector3> scaleTo;
        public List<Vector3> scalefrom;
        public float scaleSpeed;
        public float delay;
        public Ease ease;
    }

    [SerializeField] private AnimationObjects rightObjects;
    [SerializeField] private AnimationObjects leftObjects;
    [SerializeField] private AnimationObjects topObjects;
    [SerializeField] private AnimationObjects BottomObjects;
    [SerializeField] private ScaleObjects scaleObjects;

    [SerializeField] private bool isRight, isLeft, isTop, isBottom, isScale;

    [ContextMenu("SetPositions")]
    public void setPositions()
    {
        if(isRight)
        {
            //rightObjects.positions = new List<Vector2>(rightObjects.rectTransforms.Count);
            for (int i = 0; i < rightObjects.rectTransforms.Count; i++)
            {
                rightObjects.positions[i] = rightObjects.rectTransforms[i].localPosition;
            }
        }
        if(isLeft)
        {
            for (int i = 0; i < leftObjects.rectTransforms.Count; i++)
            {
                leftObjects.positions[i] = leftObjects.rectTransforms[i].localPosition;
            }
        }
        if(isTop)
        {
            for (int i = 0; i < topObjects.rectTransforms.Count; i++)
            {
                topObjects.positions[i] = topObjects.rectTransforms[i].localPosition;
            }
        }
        if (isBottom)
        {
            for (int i = 0; i < BottomObjects.rectTransforms.Count; i++)
            {
                BottomObjects.positions[i] = BottomObjects.rectTransforms[i].localPosition;
            }
        }
    }


    private void OnEnable()
    {

        if (isRight)
        {
            SetRightObjectsOut();
            moveRightObjects(false);
        }
        if (isLeft)
        {
            SetLeftObjectsOut();
            moveleftObjects(false);
        }
        if (isTop)
        {
            SetTopObjectsOut();
            moveTopObjects(false);
        }
        if (isBottom)
        {
            SetBottomObjectsOut();
            moveBottomObjects(false);
        }
        if (isScale)
        {
            StartCoroutine(scaleAnimations());
        }
    }


    IEnumerator scaleAnimations()
    {
        for (int i = 0; i < scaleObjects.transforms.Count; i++)
        {
            scaleObjects.transforms[i].DOScale(scaleObjects.scaleTo[i], scaleObjects.scaleSpeed).From(scaleObjects.scalefrom[i]).SetEase(scaleObjects.ease).SetUpdate(true).startValue = Vector3.zero;
            yield return new WaitForSeconds(scaleObjects.delay);
        }
    }


    #region AnimationFunctions
    private void moveRightObjects(bool withDelay)
    {
        if (withDelay == false)
        {
            for (int i = 0; i < rightObjects.rectTransforms.Count; i++)
            {
                //scaleDebug.Log("moveRightObjects");
                rightObjects.rectTransforms[i].DOAnchorPos(rightObjects.positions[i], rightObjects.speed).SetEase(rightObjects.ease).SetUpdate(true);
            }
        }
        else
        {
            StartCoroutine(moveRightObjectswithDelay());
        }
    }

    IEnumerator moveRightObjectswithDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(rightObjects.delay);
        for (int i = 0; i < rightObjects.rectTransforms.Count; i++)
        {
            rightObjects.rectTransforms[i].DOAnchorPos(rightObjects.positions[i], rightObjects.speed).SetEase(rightObjects.ease).SetUpdate(true);
            yield return wait;
        }
    }
    private void moveleftObjects(bool withDelay)
    {
        if (withDelay == false)
        {
            for (int i = 0; i < leftObjects.rectTransforms.Count; i++)
            {
                leftObjects.rectTransforms[i].DOAnchorPos(leftObjects.positions[i], leftObjects.speed).SetEase(leftObjects.ease).SetUpdate(true);
            }
        }
        else
        {
            StartCoroutine(moveLeftObjectswithDelay());
        }
    }

    IEnumerator moveLeftObjectswithDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(leftObjects.delay);
        for (int i = 0; i < leftObjects.rectTransforms.Count; i++)
        {
            leftObjects.rectTransforms[i].DOAnchorPos(leftObjects.positions[i], leftObjects.speed).SetEase(leftObjects.ease).SetUpdate(true);
            yield return wait;
        }
    }
    private void moveTopObjects(bool withDelay)
    {
        if (withDelay == false)
        {
            for (int i = 0; i < topObjects.rectTransforms.Count; i++)
            {
                topObjects.rectTransforms[i].DOAnchorPos(topObjects.positions[i], topObjects.speed).SetEase(topObjects.ease).SetUpdate(true);
            }
        }
        else
        {
            StartCoroutine(moveTopObjectswithDelay());
        }
    }

    IEnumerator moveTopObjectswithDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(topObjects.delay);
        for (int i = 0; i < topObjects.rectTransforms.Count; i++)
        {
            topObjects.rectTransforms[i].DOAnchorPos(topObjects.positions[i], topObjects.speed).SetEase(topObjects.ease).SetUpdate(true);
            yield return wait;
        }
    }
    private void moveBottomObjects(bool withDelay)
    {
        if (withDelay == false)
        {
            for (int i = 0; i < BottomObjects.rectTransforms.Count; i++)
            {
                BottomObjects.rectTransforms[i].DOAnchorPos(BottomObjects.positions[i], BottomObjects.speed).SetEase(BottomObjects.ease).SetUpdate(true);
            }
        }
        else
        {
            StartCoroutine(moveBottomObjectswithDelay());
        }
    }

    IEnumerator moveBottomObjectswithDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(BottomObjects.delay);
        for (int i = 0; i < BottomObjects.rectTransforms.Count; i++)
        {
            BottomObjects.rectTransforms[i].DOAnchorPos(BottomObjects.positions[i], BottomObjects.speed).SetEase(BottomObjects.ease).SetUpdate(true);
            yield return wait;
        }
    }
    #endregion


    #region SetOutFunctions



    private void SetTopObjectsOut()
    {
        for (int i = 0; i < topObjects.rectTransforms.Count; i++)
        {
            topObjects.rectTransforms[i].anchoredPosition = new Vector2(topObjects.rectTransforms[i].anchoredPosition.x, topObjects.rectTransforms[i].anchoredPosition.y + Screen.height);
        }
    }
    private void SetBottomObjectsOut()
    {
        for (int i = 0; i < BottomObjects.rectTransforms.Count; i++)
        {
            BottomObjects.rectTransforms[i].anchoredPosition = new Vector2(BottomObjects.rectTransforms[i].anchoredPosition.x, BottomObjects.rectTransforms[i].anchoredPosition.y - Screen.height);
        }
    }
    private void SetRightObjectsOut()
    {
        for (int i = 0; i < rightObjects.rectTransforms.Count; i++)
        {
            //Debug.Log("SetRightObjectsOut");
            rightObjects.rectTransforms[i].anchoredPosition = new Vector2(rightObjects.rectTransforms[i].anchoredPosition.x + (Screen.width / 2), rightObjects.rectTransforms[i].anchoredPosition.y);
        }
    }
    private void SetLeftObjectsOut()
    {
        for (int i = 0; i < leftObjects.rectTransforms.Count; i++)
        {
            leftObjects.rectTransforms[i].anchoredPosition = new Vector2(leftObjects.rectTransforms[i].anchoredPosition.x - (Screen.width / 2), leftObjects.rectTransforms[i].anchoredPosition.y);
        }
    }
    #endregion
}
