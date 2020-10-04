using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{
   private Light2D globalLight;
   public TextMeshProUGUI scoreText;
   public TextMeshProUGUI timerText;
   public int currentScore;

   public bool isGlobalLightEnabled = false;
   public Room currentRoom;
   public bool IsGameStarted = false;
   
   public void Start()
   {
      globalLight = GetComponent<Light2D>();
      globalLight.gameObject.SetActive(isGlobalLightEnabled);
      UpdateScore(0);
   }

   public void UpdateScore(int amount)
   {
      currentScore += amount;
      scoreText.SetText("SCORE: " + currentScore.ToString().PadLeft(8, '0'));
   }

   private void Update()
   {
      if (!IsGameStarted)
      {
         // It seems we are on the first room
         IsGameStarted = true;
         currentRoom.EnterRoom();
      }
   }

   public void ResetCurrentRoom()
   {
      currentRoom.EnterRoom();
   }

   public void EnableArrowOnCurrentRoom()
   {
      currentRoom.EnableArrow();
   }

   public void UpdateRoom(Room room)
   {
      currentRoom = room;
   }
}
