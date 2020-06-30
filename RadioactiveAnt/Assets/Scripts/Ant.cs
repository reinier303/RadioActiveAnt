using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : Entity
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float minSize, maxSize;

    protected override void Start()
    {
        base.Start();
        float size = Random.Range(minSize,maxSize);
        if(size > 0.055f)
        {
            size = Random.Range(minSize, maxSize);
        }
        transform.localScale = new Vector2(size, size);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color32(164, 147, (byte)Random.Range(0,255), 255);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
