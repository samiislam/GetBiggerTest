using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpButton : MonoBehaviour
{
    [SerializeField] int myIndex;
    Player myPlayer;
    Button myButton;
    UpgradeSystem myUpgradeSystem;

    bool dashActive;
    bool bombsActive;
    bool regenerationActive;
    void Start()
    {
        myPlayer = FindAnyObjectByType<Player>();
        myButton = GetComponent<Button>();
    }

    void Update()
    {
        if(myIndex == 0)
        {
            dashActive = myPlayer.GetDashCoolDown();
            if (dashActive == false)
            {
                myButton.interactable = false;
            }
            else if (dashActive == true)
            {
                myButton.interactable = true;
            }
        } 
        
        if(myIndex == 1)
        {
            bombsActive = myPlayer.GetBombCoolDown();
            if (bombsActive == false)
            {
                myButton.interactable = false;
            }
            else if (bombsActive == true)
            {
                myButton.interactable = true;
            }
        }

        if(myIndex == 2)
        {
            regenerationActive = myPlayer.GetRegenerationCoolDown();
            if (regenerationActive == false)
            {
                myButton.interactable = false;
            }
            else if (regenerationActive == true)
            {
                myButton.interactable = true;
            }
        } 
    }
}
