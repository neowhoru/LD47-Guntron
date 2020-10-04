using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   private Light2D globalLight;
   public TextMeshProUGUI scoreText;
   public TextMeshProUGUI timerText;
   public Text roomName;
   public GameObject fadePanel;
   public int currentScore;

   public bool isGlobalLightEnabled = false;
   public Room currentRoom;
   public bool IsGameStarted = false;

   public void Start()
   {
      globalLight = GetComponent<Light2D>();
      globalLight.enabled = isGlobalLightEnabled;
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
         UpdateRoomName(currentRoom.roomName);
      }
   }

   public void FinishGame()
   {
      PlayerPrefs.SetInt("SCORE",currentScore);
      PlayerPrefs.Save();
      fadePanel.GetComponent<Animator>().Play("FadeOutAnimation");
      Invoke("LoadFinishScene",4);
   }

   public void LoadFinishScene()
   {
      SceneManager.LoadSceneAsync("FinishedGameScene");
   }

   public void ResetCurrentRoom()
   {
      currentRoom.EnterRoom();
      UpdateRoomName(currentRoom.roomName);
   }

   public void UpdateRoomName(string roomId)
   {
      roomName.text = "Room " + roomId;
   }

   public void EnableArrowOnCurrentRoom()
   {
      currentRoom.EnableArrow();
   }

   public void UpdateRoom(Room room)
   {
      currentRoom = room;
      room.EnterRoom();
      UpdateRoomName(currentRoom.roomName);
      
   }
   
}
