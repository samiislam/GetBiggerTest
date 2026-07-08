using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyDisplay : MonoBehaviour
{
    TextMeshProUGUI keyText;
    Player player;
    KeysPerLevel myKeysPerLevel;

    private void Start()
    {
        myKeysPerLevel = FindAnyObjectByType<KeysPerLevel>();
        keyText = GetComponent<TextMeshProUGUI>();
        player = FindAnyObjectByType<Player>();
    }

    private void Update()
    {
        keyText.text = player.GetKeys().ToString() + myKeysPerLevel.GetKeysPerLevel().ToString();
    }
}
