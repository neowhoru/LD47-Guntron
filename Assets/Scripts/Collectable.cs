using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int scoreValue = 1000;
    public AudioClip sfx;
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    
    public enum CollectableType
    {
        DIAMOND, HINT
    }

    public CollectableType type = CollectableType.DIAMOND;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    public void DisableCollectable()
    {
        if (_audioSource != null && sfx != null)
        {
            _audioSource.PlayOneShot(sfx);
        }

        _spriteRenderer.enabled = false;
        _collider2D.enabled = false;
    }

    public void EnableCollectable()
    {
        _spriteRenderer.enabled = true;
        _collider2D.enabled = true;
    }

    public bool IsEnabled()
    {
        return _spriteRenderer.enabled;
    }
}
