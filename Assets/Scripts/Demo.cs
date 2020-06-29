using UnityEngine;
using Validation;

public class Demo : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    private void Awake()
    {
        gameObject.Require(out _spriteRenderer);
    }

    private SpriteRenderer _spriteRenderer;
}