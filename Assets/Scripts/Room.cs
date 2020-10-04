using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string roomName = "1-1";
    public string enterMessage = "Fight!"; // ToDo: this could be changed maybe

    public GameObject hintObject;
    public GameObject dustPrefab;
    public GameObject arrowObject;
    public List<GameObject> enemys = new List<GameObject>();

    public void EnterRoom()
    {
        arrowObject.gameObject.SetActive(false);

        foreach (Transform childObject in transform)
        {
            if (childObject.tag.Equals("Collectable"))
            {
                childObject.GetComponent<Collectable>().EnableCollectable();
            }

            if (childObject.tag.Equals("Enemy"))
            {
                childObject.GetComponent<EnemyBase>().EnableEnemy();
            }

            if (childObject.tag.Equals("Door"))
            {
                childObject.GetComponent<Door>().CloseDoor();
            }
        }
    }

    public void EnableArrow()
    {
        arrowObject.gameObject.SetActive(true);
        arrowObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void Update()
    {
        //CheckForRoomFinishState();
    }

    public void CheckForRoomFinishState()
    {
        int countCollectable = 0;
        int countEnemysActive = 0;
        List<GameObject> doors = new List<GameObject>();
        foreach (Transform childObject in transform)
        {
            if (childObject.tag.Equals("Collectable"))
            {
                if (childObject.GetComponent<Collectable>().IsEnabled())
                {
                    countCollectable++;
                }
            }

            if (childObject.tag.Equals("Enemy"))
            {
                if (childObject.GetComponent<EnemyBase>().IsEnabled())
                {
                    countEnemysActive++;
                }
            }

            if (childObject.tag.Equals("Door"))
            {
                doors.Add(childObject.gameObject);
            }
        }

        if (countCollectable == 0 && countEnemysActive == 0)
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<Door>().OpenDoor();
            }
        }
    }


    public void DisableHintObject()
    {
        Vector3 hintObjectPos = hintObject.transform.position;
        hintObject.gameObject.SetActive(false);
        if (dustPrefab != null)
        {
            Instantiate(dustPrefab, hintObjectPos, Quaternion.identity);
        }
        
    }
}
