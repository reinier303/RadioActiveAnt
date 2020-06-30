using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = InstanceManager<GameManager>.GetInstance("GameManager");
        if (PlayerPrefs.GetInt("TutorialSeen") == 0)
        {
            gameManager.PauseGame();
            PlayerPrefs.SetInt("TutorialSeen", 1);
            gameManager.pauseMenu.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
