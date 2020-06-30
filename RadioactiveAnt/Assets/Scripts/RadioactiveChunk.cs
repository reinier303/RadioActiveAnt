using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class RadioactiveChunk : MonoBehaviour
{
    private UnityEngine.Experimental.Rendering.Universal.Light2D light2D;
    // Start is called before the first frame update
    void Start()
    {
        Transform player = InstanceManager<GameManager>.GetInstance("GameManager").Player.transform;
        transform.localScale = (player.transform.localScale / Random.Range(20, 10));
        light2D = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        float intensity;
        float radius;
        if(player.localScale.x >= 0.3f)
        {
            intensity = 0.5f;
            radius = 1f;
        }
        else if (player.localScale.x >= 2f)
        {
            intensity = 1f;
            radius = 2f;
        }
        else
        {
            intensity = 0.1f;
            radius = 0.2f;
        }
        light2D.intensity = intensity;
        light2D.pointLightOuterRadius = radius;
    }

}
