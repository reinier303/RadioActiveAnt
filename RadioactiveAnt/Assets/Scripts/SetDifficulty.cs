using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDifficulty : MonoBehaviour
{
    private void Start()
    {
        if(PlayerPrefs.GetString("Difficulty") == null)
        {
            PlayerPrefs.SetString("Difficulty", "Medium");
        }
    }

    public void Easy()
    {
        PlayerPrefs.SetString("Difficulty", "Easy");
    }

    public void Medium()
    {
        PlayerPrefs.SetString("Difficulty", "Medium");
    }

    public void Hard()
    {
        PlayerPrefs.SetString("Difficulty", "Hard");
    }
}
