using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightPulse : MonoBehaviour
{
    private UnityEngine.Experimental.Rendering.Universal.Light2D light2D;
    private float baseIntensity;
    private bool high;

    [SerializeField]
    private float start, end, time;
    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        baseIntensity = light2D.intensity;
        high = false;
        StartCoroutine(lerpLight(start,end,time));
    }

    public IEnumerator lerpLight(float start, float end, float LerpTime)
    {
        float StartTime = Time.time;
        float EndTime = StartTime + LerpTime;

        if(high)
        {
            while (Time.time < EndTime)
            {
                float timeProgressed = (Time.time - StartTime) / LerpTime;  // this will be 0 at the beginning and 1 at the end.
                light2D.intensity = Mathf.Lerp(end, start, timeProgressed);

                yield return new WaitForFixedUpdate();
            }
            high = false;
        }
        else
        {
            while (Time.time < EndTime)
            {
                float timeProgressed = (Time.time - StartTime) / LerpTime;  // this will be 0 at the beginning and 1 at the end.
                light2D.intensity = Mathf.Lerp(start, end, timeProgressed);

                yield return new WaitForFixedUpdate();
            }
            high = true;
        }

        StartCoroutine(lerpLight(start, end, time));
    }
}
