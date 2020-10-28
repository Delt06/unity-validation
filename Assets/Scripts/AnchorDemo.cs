using UnityEngine;
using Validation;

public sealed class AnchorDemo : MonoBehaviour
{
	private void Awake()
	{
		this.ResolveDependencies();
		Debug.Log(_animator);
	}

	[Dependency(Source.Anchor)] private readonly Animator _animator = default;
}