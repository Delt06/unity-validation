using UnityEngine;
using Validation;


[RequireComponent(typeof(SpriteRenderer)), RequireComponentAnywhere(typeof(Camera)),
 RequireComponentAnywhere(typeof(MeshCollider))]
public class Demo : MonoBehaviour
{
	private void Awake()
	{
		this.Require(out _spriteRenderer);
		this.RequireAnywhere(out _camera);
	}

	private SpriteRenderer _spriteRenderer;
	private Camera _camera;
}