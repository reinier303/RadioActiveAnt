using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanLoop : MonoBehaviour
{
    public LeanTweenType InType;

    public float Time, Factor, Delay;
    // Start is called before the first frame update
    void OnEnable()
    {
        LeanTween.scale(gameObject, new Vector2(Factor, Factor), Time).setLoopPingPong().setDelay(Delay).setEase(InType);
    }
}
