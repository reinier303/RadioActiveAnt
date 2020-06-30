using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GameManager gameManager = InstanceManager<GameManager>.GetInstance("GameManager");

        if (gameManager.muted == 1)
        {
            GetComponent<AudioSource>().volume = 0;
        }
    }
}
