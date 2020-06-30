using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entity : MonoBehaviour, IEatable
{
    private ObjectPooler objectPooler;
    private GameManager gameManager;


    [SerializeField]
    private List<Sprite> segmentSprites;

    [SerializeField]
    private float power;

    public bool canEat;

    private GameObject border;

    private bool startDone;

    protected virtual void Start()
    {
        objectPooler = InstanceManager<ObjectPooler>.GetInstance("ObjectPooler");
        gameManager = InstanceManager<GameManager>.GetInstance("GameManager");
        border = transform.GetChild(0).gameObject;
        border.SetActive(false);
        if(gameManager.Player != null)
        {
            if(gameManager.Player.activeSelf)
            {
                StartCoroutine(CheckIfEatable());
            }
        }
        startDone = true;
    }

    protected void OnEnable()
    {
        if(startDone)
        {
            objectPooler = InstanceManager<ObjectPooler>.GetInstance("ObjectPooler");
            gameManager = InstanceManager<GameManager>.GetInstance("GameManager");
            border = transform.GetChild(0).gameObject;
            border.SetActive(false);
            if (gameManager.Player != null)
            {
                if (gameManager.Player.activeSelf)
                {
                    StartCoroutine(CheckIfEatable());
                }
            }
        }
    }

    private IEnumerator CheckIfEatable()
    {
        yield return new WaitForSeconds(0.25f);
        if(Vector2.Distance(transform.position, gameManager.Player.transform.position) < gameManager.Player.transform.localScale.x * 6f)
        { 
            if(transform.localScale.x <= gameManager.Player.transform.localScale.x)
            {
                gameManager.AddToBiggerEntities(this);
            }
            if(transform.localScale.x < (gameManager.Player.transform.localScale.x / 2))
            {
                gameManager.BiggerEntities.Remove(this);
            }
        }
        else
        {
            gameManager.BiggerEntities.Remove(this);
        }
        StartCoroutine(CheckIfEatable());
    }

    public void EnableBorder()
    {
        border.SetActive(true);
    }

    public void DisableBorder()
    {
        border.SetActive(false);
    }

    public void Die()
    {
        SpawnEffect();
        SpawnSegments();
        gameObject.SetActive(false);
    }

    protected void SpawnEffect()
    {
        GameObject Effect = objectPooler.SpawnFromPool("Effect",transform.position + new Vector3(0,0, -0.1f), Quaternion.Euler(-90, 0,0));
        Effect.transform.localScale = new Vector3(transform.localScale.x /4, transform.localScale.x / 4, transform.localScale.x / 5);
        if(gameManager.muted == 1)
        {
            Effect.GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            Effect.GetComponent<AudioSource>().volume = Mathf.Clamp01(transform.localScale.x / gameManager.Player.transform.localScale.x);
        }
    }

    protected void SpawnSegments()
    {
        for(int i = 0; i < Random.Range(3,6); i++)
        {
            GameObject Segment = objectPooler.SpawnFromPool("Segment", transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Segment.transform.localScale = transform.localScale;
            Segment.GetComponent<Segment>().Crunch(segmentSprites, power);
        }
    }
}
