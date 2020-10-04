using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHint : MonoBehaviour
{
    public bool isEnabled = false;

    private SpriteRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.enabled = isEnabled;
    }

    public void SwitchHint(bool enabled)
    {
        isEnabled = enabled;
        _renderer.enabled = isEnabled;
    }
    
}
