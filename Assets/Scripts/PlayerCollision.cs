using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private float mDampingX, mDampingY, mDampingZ;
    private CinemachineVirtualCamera vcamPlayer;

    private GameManager _gameManager;
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        vcamPlayer = FindObjectOfType<CinemachineVirtualCamera>();
        mDampingX = vcamPlayer.GetCinemachineComponent<CinemachineTransposer>().m_XDamping;
        mDampingY = vcamPlayer.GetCinemachineComponent<CinemachineTransposer>().m_YDamping;
        mDampingZ = vcamPlayer.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Door":
                HandleDoors(other);
                break;
            case "Room":
                _gameManager.UpdateRoom(other.GetComponent<Room>());
                other.GetComponent<Room>().EnterRoom();
                break;
            case "Collectable":
                HandleCollectable(other);
                break;
            
            case "Enemy":
                HandleEnemyCollision(other);
                break;
        }
    }

    private void HandleEnemyCollision(Collider2D coll)
    {
        GetComponent<PlayerMovement>().PlayerDie();
        Invoke("RestartRoom", 2);
    }

    public void RestartRoom()
    {
        _gameManager.ResetCurrentRoom();    
        GetComponent<PlayerMovement>().Respawn();
    }
    
    private void HandleDoors(Collider2D other)
    {
        Door theDoor = other.GetComponent<Door>();
        if (theDoor.currentState.Equals(Door.DoorState.OPEN))
        {
            // Move to the new Room
            DisableVirtualPlayerCameraDamping();
            TeleportToDoor(theDoor);
            Invoke("EnableVirtualPlayerCameraDamping", 1);
        }
    }

    private void TeleportToDoor(Door door)
    {
        Vector2 targetPos = door.targetDoor.transform.position;
        switch (door.targetDoor.GetComponent<Door>().currentDirection)
        {
            case Door.DoorDirection.UP:
                targetPos.y = targetPos.y - 1f;
                break;
            case Door.DoorDirection.DOWN:
                targetPos.y = targetPos.y + 1f;
                break;
            case Door.DoorDirection.LEFT:
                targetPos.x = targetPos.x + 2f;
                break;
            case Door.DoorDirection.RIGHT:
                targetPos.x = targetPos.x - 2f;
                break;
        }

        GetComponent<PlayerMovement>().TeleportToPosition(targetPos);
    }

    public void DisableVirtualPlayerCameraDamping()
    {
        CinemachineVirtualCamera vcamPlayer = FindObjectOfType<CinemachineVirtualCamera>();
        vcamPlayer.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
        vcamPlayer.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
        vcamPlayer.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 0;
    }

    public void EnableVirtualPlayerCameraDamping()
    {
        CinemachineVirtualCamera vcamPlayer = FindObjectOfType<CinemachineVirtualCamera>();
        vcamPlayer.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = mDampingX;
        vcamPlayer.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = mDampingY;
        vcamPlayer.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = mDampingZ;
    }
    

    public void HandleRoom(Collider2D other)
    {
        // Restart Room 
        other.GetComponent<Room>().EnterRoom();
    }

    public void HandleCollectable(Collider2D other)
    {
        Collectable collectable = other.GetComponent<Collectable>();
        _gameManager.UpdateScore(collectable.scoreValue);

        if (collectable.type.Equals(Collectable.CollectableType.HINT))
        {
            _gameManager.EnableArrowOnCurrentRoom();
        }
        
        collectable.DisableCollectable();
        _gameManager.currentRoom.CheckForRoomFinishState();

    }
}