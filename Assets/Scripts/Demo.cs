using UnityEngine;
using Validation;

public class Demo : MonoBehaviour
{
	private void Update()
	{
		_camera.backgroundColor = Time.frameCount % 120 < 64 ? Color.white : Color.gray;
		var color = _spriteRenderer.color;
		color.a = Mathf.PingPong(Time.time, 1f);
		_spriteRenderer.color = color;
	}

	private void Start()
	{
		_meshFilter.sharedMesh = new Mesh();
	}

	private void Awake()
	{
		// called to populate all the fields
		this.ResolveDependencies();
	}

	// must be anywhere
	[Dependency(Source.Global)] private readonly Camera _camera = default;
	// must be attached to this GameObject
	[Dependency] private readonly SpriteRenderer _spriteRenderer = default;
	// must be attached to some of the object's children (including the object itself)
	[Dependency(Source.FromChildren)] private readonly MeshFilter _meshFilter = default; 
}