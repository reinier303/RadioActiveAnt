using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KongregateUserNameText : MonoBehaviour
{
    private void Start()
    {
        KongregateAPIBehaviour kongregateAPI = KongregateAPIBehaviour.Instance;
        if(kongregateAPI == null)
        {
            return;
        }
        TextMeshProUGUI textComponent = GetComponent<TextMeshProUGUI>();
        if (kongregateAPI.Username != null)
        {
            textComponent.text = kongregateAPI.Username;
        }
        else
        {
            textComponent.text = "NaN";
        }
    }
}
