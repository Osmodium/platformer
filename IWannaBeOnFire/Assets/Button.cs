using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Button : MonoBehaviour
{
    public bool on = false;
    public Sprite offSprite;
    public Sprite onSprite;
    private Sprite _currentSprite;
    private SpriteRenderer _spriteRenderer;
    
    public void Start()
    {
        _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>() as SpriteRenderer;
        _currentSprite = offSprite;
        if (on)
            _currentSprite = onSprite;
        
        _spriteRenderer.sprite = _currentSprite;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("PlayerBullet"))
        {
            on = !on;
            UpdateState();
        }
    }

    private void UpdateState()
    {
        _currentSprite = offSprite;
        if (on)
            _currentSprite = onSprite;

        _spriteRenderer.sprite = _currentSprite;
    }
}
