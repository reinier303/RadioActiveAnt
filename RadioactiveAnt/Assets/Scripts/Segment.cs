using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    public void Crunch(List<Sprite> sprites, float power)
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
        Vector2 randomForce = new Vector2(Random.Range(-power, power), Random.Range(-power, power));
        GetComponent<Rigidbody2D>().AddForce(randomForce);
    }
}
