using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorState
    {
        OPEN, CLOSE
    }

    public enum DoorDirection
    {
        UP, DOWN, LEFT, RIGHT
    }

    public DoorDirection currentDirection = DoorDirection.UP;

    public DoorState currentState = DoorState.CLOSE;
    private new BoxCollider2D _collider;
    private AudioSource _audioSource;
    private SpriteRenderer _renderer;
    
    public AudioClip sfx;

    public Sprite openSprite;
    public Sprite closeSprite;
    
    public GameObject targetDoor;
    public bool isFinalDoor = false;
    
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponent<SpriteRenderer>();
        
        if (currentState == DoorState.OPEN)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    public void CloseDoor()
    {
        _collider.isTrigger = false;
        _renderer.sprite = closeSprite;
        // if (sfx != null)
        // {
        //     _audioSource.PlayOneShot(sfx);    
        // }

        currentState = DoorState.OPEN;

    }

    public void OpenDoor()
    {
        Debug.Log("Open Door");
        _collider.isTrigger = true;
        _renderer.sprite = openSprite;
        if (sfx != null)
        {
            _audioSource.PlayOneShot(sfx);    
        }
        currentState = DoorState.OPEN;
    }


}
