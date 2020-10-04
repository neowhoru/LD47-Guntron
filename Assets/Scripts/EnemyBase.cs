using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int scoreValue = 1000;
    public AudioClip sfx;
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    public bool isEnabled = false;
    
    public Transform target;
    public float chaseRadius;
    public float attakRadius;
    public Transform homePosition;
    public float moveSpeed = 5f;

    public Vector3 startPosition;
    public GameManager _gameManager;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        _gameManager = FindObjectOfType<GameManager>();
        startPosition = transform.position;
    }

    public void DisableEnemy()
    {
        if (_audioSource != null && sfx != null)
        {
            _audioSource.PlayOneShot(sfx);
        }

        isEnabled = false;
        _spriteRenderer.enabled = false;
        _collider2D.enabled = false;
    }

    public void EnableEnemy()
    {
        transform.position = startPosition;
        isEnabled = true;
        _spriteRenderer.enabled = true;
        _collider2D.enabled = true;
    }
    
    void Update()
    {
        if (isEnabled)
        {
            CheckDistance();    
        }
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attakRadius)
        {
            Debug.Log("follow player");
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Bullet"))
        {
            _collider2D.enabled = false;
            _gameManager.UpdateScore(scoreValue);
            GetComponent<SpriteFlash>().Flash();
            Invoke(nameof(DisableEnemy), 0.1f);
            _gameManager.currentRoom.CheckForRoomFinishState();
        }
    }
    
    public bool IsEnabled()
    {
        return _spriteRenderer.enabled;
    }
}
