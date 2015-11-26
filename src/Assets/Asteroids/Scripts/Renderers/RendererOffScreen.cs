using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS / Renderer Off Screen")]
	public class RendererOffScreen : RendererBehaviour
	{
		public override void OnBecameInvisible ()
		{
			transform.gameObject.SetActive (false);
		}
	}
}