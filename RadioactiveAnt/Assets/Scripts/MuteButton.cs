using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    private Sprite onSprite;

    [SerializeField] Sprite offSprite;

    private Image image;

    // Start is called before the first frame update
    private void Awake()
    {
        image = GetComponent<Image>();
        onSprite = image.sprite;
        if (PlayerPrefs.GetInt("Mute") == 0)
        {
            Mute();
        }
        else
        {
            UnMute();
        }
    }

    public void MuteButtonPress()
    {
        if(PlayerPrefs.GetInt("Mute") == 1)
        {
            Mute();
        }
        else
        {
            UnMute();
        }
    }

    private void Mute()
    {
        PlayerPrefs.SetInt("Mute", 0);
        image.sprite = onSprite;
    }

    private void UnMute()
    {
        PlayerPrefs.SetInt("Mute", 1);
        image.sprite = offSprite;
    }
}
