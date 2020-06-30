using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanEaseIn : MonoBehaviour
{
    public LeanTweenType InType;
    public float Time, Delay;
    RectTransform rect;

    private void OnEnable()
    {
        UnityEngine.Time.timeScale = 1;
        transform.localScale = new Vector2(0, 0);
        LeanTween.scale(gameObject, new Vector2(1, 1), Time).setDelay(Delay).setEase(InType);
    }

}
