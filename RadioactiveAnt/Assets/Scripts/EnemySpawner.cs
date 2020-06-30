using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    private ObjectPooler objectPooler;

    [SerializeField]
    private float spawnTime;

    private GameManager gameManager;

    private string difficulty;

    private void Start()
    {
        gameManager = InstanceManager<GameManager>.GetInstance("GameManager");
        objectPooler = InstanceManager<ObjectPooler>.GetInstance("ObjectPooler");

        StartCoroutine(RepeatingCoroutine(spawnTime, 0.5f, SpawnEnemy));

        difficulty = PlayerPrefs.GetString("Difficulty");
        AdjustDifficulty();
    }

    private void AdjustDifficulty()
    {
        if(difficulty == "Easy")
        {
            spawnTime = 0.8f;
        }
        else if (difficulty == "Medium")
        {
            spawnTime = 0.65f;
        }
        else
        {
            spawnTime = 0.4f;
        }
    }

    private void SpawnEnemy()
    {
        Vector2 StartPosition = GenerateRandomPosition();
        float rotationZ = CalculateRotation(StartPosition);
        objectPooler.SpawnFromPool(gameManager.currentStage.Enemies[Random.Range(0,gameManager.currentStage.Enemies.Count)], StartPosition, Quaternion.Euler(0,0, rotationZ));
    }

    private float CalculateRotation(Vector2 StartPosition)
    {
        if(StartPosition.x == -gameManager.currentStage.MaxPosition)
        {
            return 270;
        }
        else if (StartPosition.x == gameManager.currentStage.MaxPosition)
        {
            return 90;
        }
        else if (StartPosition.y == -gameManager.currentStage.MaxPosition)
        {
            return 0;
        }
        else
        {
            return 180;
        }

    }

    private Vector2 GenerateRandomPosition()
    {
        bool XorY = (Random.value > 0.5f);
        float x, y = 0;

        if (XorY)
        {
            x = Random.Range(-gameManager.currentStage.MaxPosition, gameManager.currentStage.MaxPosition);
            bool PosOrNeg = (Random.value > 0.5f);
            if(PosOrNeg)
            {
                y = gameManager.currentStage.MaxPosition;
            }
            else
            {
                y = -gameManager.currentStage.MaxPosition;
            }
        }
        else
        {
            y = Random.Range(-gameManager.currentStage.MaxPosition, gameManager.currentStage.MaxPosition);
            bool PosOrNeg = (Random.value > 0.5f);
            if (PosOrNeg)
            {
                x = gameManager.currentStage.MaxPosition;
            }
            else
            {
                x = -gameManager.currentStage.MaxPosition;
            }
        }
        return new Vector2(x,y);
    }

    private IEnumerator RepeatingCoroutine(float repeatTime, float startTime, System.Action action)
    {
        yield return new WaitForSeconds(startTime);
        action?.Invoke();
        yield return new WaitForSeconds(repeatTime);
        StartCoroutine(RepeatingCoroutine(repeatTime, 0f, action));
    }
}
