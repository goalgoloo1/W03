using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    public Sprite[] _sprite;
    SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(ESprite sprite)
    {
        _spriteRenderer.sprite = _sprite[(int)sprite];
    }

    public void SetSpriteFlipX(bool isTrue)
    {
        _spriteRenderer.flipX = isTrue ? true : false;
    }
}

public enum ESprite
{
    Idle,
    Jump,
    Die,
}