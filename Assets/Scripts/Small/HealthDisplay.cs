using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI healthText;
    Player myPlayer;

    private void Start()
    {
        myPlayer = FindAnyObjectByType<Player>();
        healthText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        healthText.text = myPlayer.GetHealth().ToString();  
    }
}
