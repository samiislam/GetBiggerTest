using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject openDoor;
    [SerializeField] GameObject moneyPrefab;
    [SerializeField] List<GameObject> money = new List<GameObject>();
    [SerializeField] int moneyForLevel;
    [SerializeField] GameObject moneySpawn;
    public int index = 1;
    public int LevelType = 0;
    Player player;

    GameObject door = null;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        if (door == null)
        {
            NewDoor();
        }
    }

    private void NewDoor()
    {
        bool isDoorOpen = player.OpenDoor();
        if (isDoorOpen == true)
        {
            door = Instantiate(openDoor, transform.position, Quaternion.identity);
            
            for (int i=0; i < moneyForLevel; i++)
            {
                money.Add(moneyPrefab);
                Instantiate(money[i], moneySpawn.transform.position, Quaternion.identity);
            }
        }
    }
}
