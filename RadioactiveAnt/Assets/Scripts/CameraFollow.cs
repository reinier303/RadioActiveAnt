using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector2 Margin;
    [SerializeField]
    private Vector2 Smoothing;
    [SerializeField]
    private BoxCollider2D Bounds;

    private Camera camera;
    private Vector2 min, max;

    public bool IsFollowing { get; set; }

    private void Start()
    {
        IsFollowing = true;
        min = Bounds.bounds.min;
        max = Bounds.bounds.max;
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if(IsFollowing)
        {
            if(Mathf.Abs(x - player.position.x) > Margin.x)
            {
                x = Mathf.Lerp(x, player.position.x, Smoothing.x * Time.deltaTime);
            }
            if (Mathf.Abs(y - player.position.y) > Margin.y)
            {
                y = Mathf.Lerp(y, player.position.y, Smoothing.y * Time.deltaTime);
            }
        }

        float cameraHalfWidth = camera.orthographicSize * ((float)Screen.width / Screen.height);

        x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, min.y + cameraHalfWidth, max.y - cameraHalfWidth);

        transform.position = new Vector3(x,y, transform.position.z);
    }
}
