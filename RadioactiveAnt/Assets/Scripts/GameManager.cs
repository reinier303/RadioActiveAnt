using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused;
    public GameObject pauseMenu;
    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject winMenu;
    [SerializeField]
    private GameObject time;

    public float TimeElapsed;
    public GameObject Player;
    [SerializeField]
    private int stage;

    [SerializeField]
    private Camera camera;

    public List<Stage> Stages;

    [HideInInspector]
    public Stage currentStage;

    private bool gameIsWon;

    public List<Entity> BiggerEntities;

    public int muted;

    private void Awake()
    {
        InstanceManager<GameManager>.ResetInstances();
        InstanceManager<GameManager>.CreateInstance("GameManager", this);
        muted = PlayerPrefs.GetInt("Mute");
        currentStage = Stages[0];
        isPaused = false;
        camera = Camera.main;
    }

    private void Start()
    {
        StartCoroutine(EnableBorders());
    }

    public void AddToBiggerEntities(Entity entity)
    {
        if(BiggerEntities.Count > 3)
        {
            if(entity.transform.localScale.x > BiggerEntities[3].transform.localScale.x)
            {
                BiggerEntities.RemoveAt(3);
                BiggerEntities.Add(entity);

            }
        }
        else
        {
            BiggerEntities.Add(entity);
        }
    }

    private IEnumerator EnableBorders()
    {
        foreach(Entity entity in BiggerEntities)
        {
            entity.EnableBorder();
        }
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(EnableBorders());
    }

    public void CheckPlayerSize()
    {
        if (Player.transform.localScale.x >= currentStage.SizeToWin && currentStage.StageNumber != Stages.Count)
        {
            camera = Camera.main;
            currentStage = Stages[currentStage.StageNumber++];
            StartCoroutine(lerpCameraSize(camera.orthographicSize, currentStage.CameraSize, 3f));
            Player.GetComponent<PlayerMovement>().Speed = currentStage.PlayerSpeed;
            Player.GetComponent<PlayerEat>().growthFactor = currentStage.GrowthScale;

        }
    }

    private void Update()
    {
        if(!gameIsWon)
        {
            TimeElapsed += Time.deltaTime;
        }

        //Pause and Restart

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if(!isPaused)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
            }
        }

    }

    public void RestartGame()
    {
        //InstanceManager<GameManager>.ResetInstances();
        SceneManager.LoadSceneAsync(1);
        UnPauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        isPaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void StartGame()
    {
        RestartGame();
    }

    public void Win()
    {
        winMenu.SetActive(true);
        Player.SetActive(false);
        gameIsWon = true;
        SetHighscore();
    }

    private void SetHighscore()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty");
        if(PlayerPrefs.GetFloat("EHighscore") < TimeElapsed && difficulty == "Easy")
        {
            PlayerPrefs.SetFloat("EHighscore", TimeElapsed);
            KongregateAPIBehaviour.Instance.SubmitHighscore(TimeElapsed, "Easy");
        }
        else if (PlayerPrefs.GetFloat("MHighscore") < TimeElapsed && difficulty == "Medium")
        {
            PlayerPrefs.SetFloat("MHighscore", TimeElapsed);
            KongregateAPIBehaviour.Instance.SubmitHighscore(TimeElapsed, "Medium");
        }
        else if (PlayerPrefs.GetFloat("HHighscore") < TimeElapsed && difficulty == "Hard")
        {
            PlayerPrefs.SetFloat("HHighscore", TimeElapsed);
            KongregateAPIBehaviour.Instance.SubmitHighscore(TimeElapsed, "Hard");
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public IEnumerator lerpCameraSize(float start, float end, float LerpTime)
    {
        float StartTime = Time.time;
        float EndTime = StartTime + LerpTime;

        while (Time.time < EndTime)
        {
            float timeProgressed = (Time.time - StartTime) / LerpTime;  // this will be 0 at the beginning and 1 at the end.
            camera.orthographicSize = Mathf.Lerp(start, end, timeProgressed);

            yield return new WaitForFixedUpdate();
        }
    }
}

[System.Serializable]
public class Stage
{
    public string name;

    public int StageNumber;

    public float SizeToWin;
    public float CameraSize;
    public float PlayerSpeed;
    public float GrowthScale;

    public float MaxPosition;

    public List<string> Enemies;
}
