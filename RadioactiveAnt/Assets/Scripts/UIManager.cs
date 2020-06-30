using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private TextMeshProUGUI TimeText, highscoreText;

    private string difficulty;

    [SerializeField]
    private GameObject easy, medium, hard;

    [SerializeField]
    private TextMeshProUGUI easyText, mediumText, hardText;


    private void Start()
    {
        gameManager = InstanceManager<GameManager>.GetInstance("GameManager");
        difficulty = PlayerPrefs.GetString("Difficulty");
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            SetDifficulty();
            string difficulty = PlayerPrefs.GetString("Difficulty");

            easyText.text = PlayerPrefs.GetFloat("EHighscore").ToString("F2");
            mediumText.text = PlayerPrefs.GetFloat("MHighscore").ToString("F2");
            hardText.text = PlayerPrefs.GetFloat("HHighscore").ToString("F2");

        }
    }

    private void SetDifficulty()
    {
        if (difficulty == "Easy")
        {
            easy.SetActive(true);
            medium.SetActive(false);
        }
        else if (difficulty == "Hard")
        {
            hard.SetActive(true);
            medium.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TimeText.text = "Time:" + gameManager.TimeElapsed.ToString("F2");
    }
}
