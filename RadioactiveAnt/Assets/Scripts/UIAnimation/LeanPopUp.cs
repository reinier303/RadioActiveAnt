using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeanPopUp : MonoBehaviour
{
    public LeanTweenType InType, OutType;

    public float Time, Delay;

    public UnityEvent OnCompleteCallback;

    private void OnEnable()
    {
        transform.localScale = new Vector2(0,0);
        LeanTween.scale(gameObject, new Vector2(1, 1), Time).setDelay(Delay).setEase(InType).setIgnoreTimeScale(true);
    }

    public void OnComplete()
    {
        if(OnCompleteCallback != null)
        {
            OnCompleteCallback.Invoke();
        }
    }

    public void OnClose()
    {
        LeanTween.scale(gameObject, new Vector2(0, 0), Time).setDelay(Delay).setEase(OutType).setOnComplete(OnComplete);
    }
}
