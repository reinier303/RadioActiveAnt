using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEat : MonoBehaviour
{
    public float growthFactor;

    private GameManager gameManager;

    private string difficulty;

    private void Start()
    {
        gameManager = InstanceManager<GameManager>.GetInstance("GameManager");
        difficulty = PlayerPrefs.GetString("Difficulty");
        AdjustDifficulty();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEatable eatableScript = collision.GetComponent<IEatable>();
        if (eatableScript != null)
        {
            if(CompareSize(collision.transform))
            {
                eatableScript.Die();
                Grow(collision.transform);
                if(collision.tag == "Professor")
                {
                    gameManager.Win();
                }
            }
            else if(collision.GetComponent<Entity>().canEat)
            {
                Die();
            }
        }
    }

    private void AdjustDifficulty()
    {
        if (difficulty == "Easy")
        {
            growthFactor += ((growthFactor - 1) * 1.1f );
        }
        else if (difficulty == "Hard")
        {
            growthFactor -= ((growthFactor - 1) * 0.1f);
        }
    }

    private bool CompareSize(Transform other)
    {
        if(transform.localScale.x >= other.localScale.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Grow(Transform other)
    {
        if (other.transform.localScale.x <= (transform.localScale.x / 4))
        {
            StartCoroutine(lerpScale(transform.localScale, transform.localScale * (growthFactor - ((growthFactor - 1) / 1.2f)), 0.5f));
        }
        else if (other.transform.localScale.x <= (transform.localScale.x / 3))
        {
            StartCoroutine(lerpScale(transform.localScale, transform.localScale * (growthFactor - ((growthFactor - 1) / 1.5f)), 0.5f));
        }
        else if (other.transform.localScale.x <= (transform.localScale.x / 2))
        {
            StartCoroutine(lerpScale(transform.localScale, transform.localScale * (growthFactor - ((growthFactor - 1) / 1.8f)), 0.5f));
        }
        else if(gameObject.activeSelf)
        {
            StartCoroutine(lerpScale(transform.localScale, transform.localScale * growthFactor, 0.5f));
        }
        gameManager.CheckPlayerSize();
    }

    private void Die()
    {
        gameManager.BackToMenu();
    }

    public IEnumerator lerpScale(Vector3 startScale, Vector3 endScale, float LerpTime)
    {
        float StartTime = Time.time;
        float EndTime = StartTime + LerpTime;

        while (Time.time < EndTime)
        {
            float timeProgressed = (Time.time - StartTime) / LerpTime;  // this will be 0 at the beginning and 1 at the end.
            transform.localScale = Vector3.Lerp(startScale, endScale, timeProgressed);

            yield return new WaitForFixedUpdate();
        }

    }
}
