using UnityEngine;
using Validation;


public class Demo : MonoBehaviour
{ 
    private void Awake()
    {
        this.Require(out _spriteRenderer);
        this.RequireOnScene(out _camera);
    }

    private SpriteRenderer _spriteRenderer;
    private Camera _camera;
}